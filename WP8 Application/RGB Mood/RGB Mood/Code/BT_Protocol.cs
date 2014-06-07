using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace RGB_Mood.Code
{
    public static class BT_Protocol
    {
        /*SEND AND receive messages from color picker to bluetooth stack*/

        //START CMD 0X01
        //R 0X00
        //G 0X00
        //B 0X00
        //STOP CMD 0xFF

        const int _START_CMD = 0x01;
        const int _STOP_CMD = 0XFF;
       

        public static byte[] getCMDColor(Color color)
        {

            byte[] data_to_send = new byte[] { _START_CMD, color.R,color.G,color.B, _STOP_CMD };
            return data_to_send;
        }

        public async Task<bool> sendColor(Color color)
        {
            try
            {
                var result = getCMDColor(color);
                Bluetooth.BTConnection btpd = new Bluetooth.BTConnection();
                //Send data to BT module 
                var response = await btpd.Send_Data(result, 1, Code.Stuff._MACADDRESS);
               {
                    if (response.Equals(0X01))
                    {
                        return true;
                    }
               }

               return false;
            }

            catch(Bluetooth.BluetoothDeviceException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error : " + ex.ToString());
                return false;
            }

             
            
        }
       
    }
}
