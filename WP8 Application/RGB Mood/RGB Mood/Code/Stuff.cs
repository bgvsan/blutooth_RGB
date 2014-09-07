using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;

namespace RGB_Mood.Code
{


    class Stuff
    {
        public static string _MACADDRESS = "";
    }

    class File
    {
        public async Task<string> loadMacAddress()
        {
            string ret = "";
            Code.File file = new Code.File();
            ret  = await file.ReadFile();
            return ret;
        }

        public async Task WriteToFile(string value)
        {
            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(value.ToCharArray());

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;



            var file = await local.CreateFileAsync("DataFile.txt", CreationCollisionOption.ReplaceExisting);
            // Create a new folder name DataFolder.
            //var dataFolder = await local.CreateFolderAsync("DataFolder",
            //CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            //var file = await dataFolder.CreateFileAsync("DataFile.txt",
            //CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        public async Task<string> ReadFile()
        {
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            string ret = "";
            if (local != null)
            {

                try
                {
                    var file = await local.OpenStreamForReadAsync("DataFile.txt");

                    // Get the DataFolder folder.
                    // var dataFolder = await local.GetFolderAsync("DataFolder");

                    // Get the file.

                    // var file = await dataFolder.OpenStreamForReadAsync("DataFile.txt");

                    // Read the data.
                    using (StreamReader streamReader = new StreamReader(file))
                    {
                        ret = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine("error file read : " + ex.ToString());
                }
            }
            return ret;
        }



    }
}
