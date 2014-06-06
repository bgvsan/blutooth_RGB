using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RGB_Mood.Resources;

namespace RGB_Mood
{
    public partial class App : Application
    {
        /// <summary>
        /// Offre facile accesso al frame radice dell'applicazione Windows Phone.
        /// </summary>
        /// <returns>Nome radice dell'applicazione Windows Phone.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Costruttore dell'oggetto Application.
        /// </summary>
        public App()
        {
            // Gestore globale delle eccezioni non rilevate.
            UnhandledException += Application_UnhandledException;

            // Inizializzazione XAML standard
            InitializeComponent();

            // Inizializzazione specifica del telefono
            InitializePhoneApplication();

            // inizializzazione della visualizzazione della lingua
            InitializeLanguage();

            // Visualizza informazioni di profilatura delle immagini durante il debug.
            if (Debugger.IsAttached)
            {
                // Visualizza i contatori della frequenza dei fotogrammi corrente.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Visualizza le aree dell'applicazione che vengono ridisegnate in ogni fotogramma.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Abilitare la modalità di visualizzazione dell'analisi non di produzione,
                // che consente di visualizzare le aree di una pagina passate alla GPU con una sovrapposizione colorata.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Impedire che lo schermo si spenga mentre si trova nel debugger disabilitando
                // il rilevamento dell'inattività dell'applicazione.
                // Attenzione: utilizzare questa opzione solo in modalità di debug. L'applicazione che disabilita il rilevamento dell'inattività dell'utente continuerà ad essere eseguita
                // e a consumare energia quando l'utente non utilizza il telefono.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Codice da eseguire all'avvio dell'applicazione (ad esempio da Start)
        // Questo codice non verrà eseguito quando l'applicazione viene riattivata
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Codice da eseguire quando l'applicazione viene attivata (portata in primo piano)
        // Questo codice non verrà eseguito al primo avvio dell'applicazione
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Codice da eseguire quando l'applicazione viene disattivata (inviata in background)
        // Questo codice non verrà eseguito alla chiusura dell'applicazione
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Codice da eseguire alla chiusura dell'applicazione (ad esempio se l'utente fa clic su Indietro)
        // Questo codice non verrà eseguito quando l'applicazione viene disattivata
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Codice da eseguire se un'operazione di navigazione ha esito negativo
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // Un'operazione di navigazione ha avuto esito negativo; inserire un'interruzione nel debugger
                Debugger.Break();
            }
        }

        // Codice da eseguire in caso di eccezioni non gestite
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // Si è verificata un'eccezione non gestita; inserire un'interruzione nel debugger
                Debugger.Break();
            }
        }

        #region Inizializzazione dell'applicazione Windows Phone

        // Evitare la doppia inizializzazione
        private bool phoneApplicationInitialized = false;

        // Non aggiungere altro codice a questo metodo
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Creare il fotogramma ma non impostarlo ancora come RootVisual; in questo modo
            // la schermata iniziale rimane attiva finché non viene completata la preparazione al rendering dell'applicazione.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Gestisce gli errori di navigazione
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Gestisce le richieste di reset per cancellare il backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Accertarsi che l'inizializzazione non venga ripetuta
            phoneApplicationInitialized = true;
        }

        // Non aggiungere altro codice a questo metodo
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Impostare l'elemento visivo radice per consentire il rendering dell'applicazione
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Rimuovere il gestore in quanto non più necessario
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // Se l'applicazione ha ricevuto una navigazione 'reset', occorre controllare
            // la navigazione successiva per verificare se lo stack della pagina può essere ripristinato
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Annullare la registrazione dell'evento in modo che non venga chiamato nuovamente
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Cancellare solo lo stack per le navigazioni "nuove" (in avanti) e "aggiorna"
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // Per coerenza della IU, cancellare l'intero stack della pagina
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // non eseguire alcuna azione
            }
        }

        #endregion

        // Inizializzare il carattere dell'applicazione e la direzione flusso come definito nelle stringhe della risorsa localizzata.
        //
        // Per assicurare che il carattere dell'applicazione sia allineato con le lingue supportate e che
        // FlowDirection per ognuna di queste lingue segue la sua direzione classica, ResourceLanguage
        // e inizializzare ResourceFlowDirection in ogni file RESX affinché corrisponda a questi valori con
        // impostazioni cultura del file. Ad esempio:
        //
        // AppResources.es-ES.resx
        //    Il valore di ResourceLanguage deve essere "es-ES"
        //    Il valore di ResourceFlowDirection deve essere "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     Il valore di ResourceLanguage deve essere "ar-SA"
        //     Il valore di ResourceFlowDirection deve essere "RightToLeft"
        //
        // Per ulteriori informazioni sulla localizzazione delle applicazioni Windows Phone vedere http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Impostare il carattere affinché corrisponda alla lingua di visualizzazione definita da
                // Stringa di risorsa ResourceLanguage per ogni lingua supportata.
                //
                // Fallback al carattere della lingua neutra se la visualizzazione
                // lingua del telefono non è supportata.
                //
                // Se viene rilevato un errore del compilatore, ResourceLanguage manca da
                // file di risorse.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Impostare FlowDirection in tutti gli elementi nel nome radice basato
                // nella stringa di risorsa ResourceFlowDirection per ogni
                // lingua supportata.
                //
                // Se viene rilevato un errore del compilatore, ResourceFlowDirection manca da
                // file di risorse.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // Se viene rilevata un'eccezione, probabilmente è dovuta a
                // ResourceLangauge non impostato correttamente su un codice lingua supportato
                // o ResourceFlowDirection è impostato su un valore diverso da LeftToRight
                // o RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}