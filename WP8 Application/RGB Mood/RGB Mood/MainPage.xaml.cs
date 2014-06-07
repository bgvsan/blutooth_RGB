using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RGB_Mood.Resources;
using System.Windows.Media;
using Bluetooth;
using System.Diagnostics;

namespace RGB_Mood
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool connected = false;

        // Costruttore
        public MainPage()
        {
            InitializeComponent();
            //connect event change to send data to bluetooth
            this.colorPicker.ColorChanged += colorPicker_ColorChanged;
        }

        void colorPicker_ColorChanged(object sender, System.Windows.Media.Color color)
        {

            if (connected)
            {
                var result = Code.BT_Protocol.getCMDColor(color);
                Bluetooth.BTConnection btpd = new BTConnection();
                //Send data to BT module 
                btpd.Send_Data(result, 1, Code.Stuff._MACADDRESS);
            }
        }

      

        private async void Connection()
        {
            try
            {
                Bluetooth.BTConnection btpd = new BTConnection();
                await btpd.ConnectToDevice(Code.Stuff._MACADDRESS);
            }
            catch (Bluetooth.BluetoothDeviceException ex)
            {
                Debug.WriteLine(ex.ToString());
                connected = false;
            }
            connected = true;
        }


        private void App_bar_setting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Option.xaml", UriKind.Relative));
        }

        private void App_bar_connect_Click(object sender, EventArgs e)
        {
            Connection();
        }

        // Codice di esempio per la realizzazione di una ApplicationBar localizzata
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Imposta la barra delle applicazioni della pagina su una nuova istanza di ApplicationBar
        //    ApplicationBar = nuova ApplicationBar();

        //    // Crea un nuovo pulsante e imposta il valore del testo sulla stringa localizzata da AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crea una nuova voce di menu con la stringa localizzata da AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}