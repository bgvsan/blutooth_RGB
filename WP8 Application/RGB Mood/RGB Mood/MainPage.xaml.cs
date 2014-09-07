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
        private Bluetooth.BTConnection btpd;

        // Costruttore
        public MainPage()
        {
            InitializeComponent();
            //connect event change to send data to bluetooth
            this.colorPicker.ColorChanged += colorPicker_ColorChanged;

        }

        private async void init()
        {
            Code.File file = new Code.File();
            Code.Stuff._MACADDRESS = await file.loadMacAddress();
            if (Code.Stuff._MACADDRESS == "")
            {
                MessageBox.Show("No MacAddress set Please go to Option");
            }
        }

        async void colorPicker_ColorChanged(object sender, System.Windows.Media.Color color)
        {
            try
            {
                if (!connected)
                {
                    Connection();
                }
                var commandColor = Code.BT_Protocol.getCMDColor(color);
                string tempstring = "";
                foreach (byte s in commandColor)
                {
                    tempstring += s.ToString() + " ";
                }
                Debug.WriteLine(tempstring);
                //Send data to BT module 

                await btpd.Send_Data(commandColor, 0, Code.Stuff._MACADDRESS);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());
            }

        }



        private async void Connection()
        {
                try
                {
                    btpd = new BTConnection();
                    await btpd.ConnectToDevice(Code.Stuff._MACADDRESS);
                }
                   
                catch (Bluetooth.BluetoothDeviceException ex)
                {
                    MessageBox.Show(ex.Message);
                    Debug.WriteLine(ex.ToString());
                    connected = false;
                }
                catch (Exception exxx)
                {

                    Debug.WriteLine(exxx.ToString());
                }
                connected = true;
            
            
        }


        private void App_bar_setting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Option.xaml", UriKind.Relative));
        }

        private void App_bar_connect_Click(object sender, EventArgs e)
        {
            init();
            if (Code.Stuff._MACADDRESS != "")
            {
                Connection();
            }
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