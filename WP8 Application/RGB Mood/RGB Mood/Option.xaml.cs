using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;

namespace RGB_Mood
{
    public partial class Option : PhoneApplicationPage
    {


        public Option()
        {
            InitializeComponent();
            loadMacAddress();
        }


        private async void loadMacAddress()
        {
            Code.File file = new Code.File();
            var res = await file.ReadFile();
            this.txtMacAddress.Text = res.ToString();
            Code.Stuff._MACADDRESS = this.txtMacAddress.Text;
        }

        private async void App_bar_save_Click(object sender, EventArgs e)
        {
            try
            {
                Code.File file = new Code.File();
                await file.WriteToFile(this.txtMacAddress.Text);
                Code.Stuff._MACADDRESS = this.txtMacAddress.Text;
                MessageBox.Show("Mac Address Saved");
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error save Mac Address");
                System.Diagnostics.Debug.WriteLine("Error Save File : " + ex.ToString());
            }

        }
    }
}