﻿#pragma checksum "\\vmware-host\Shared Folders\Dropbox\GitHub\blutooth_RGB\WP8 Application\RGB Mood\RGB Mood\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FACEAE359B36084A2DE2D6596E1B5562"
//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.34011
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace RGB_Mood {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton App_bar_setting;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton App_bar_connect;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal Coding4Fun.Toolkit.Controls.ColorPicker colorPicker;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/RGB%20Mood;component/MainPage.xaml", System.UriKind.Relative));
            this.App_bar_setting = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("App_bar_setting")));
            this.App_bar_connect = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("App_bar_connect")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.colorPicker = ((Coding4Fun.Toolkit.Controls.ColorPicker)(this.FindName("colorPicker")));
        }
    }
}

