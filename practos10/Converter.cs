using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ca_csIS
{
    static class Converter
    {
        public static string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DataFiles\\";

        public static T doDeserialize<T>(string fName)
        {
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            string json = File.ReadAllText(dataPath + fName);
            T elements = JsonConvert.DeserializeObject<T>(json);
            return elements;
        }

        public static void doSerialize<T>(T elements, string fName)
        {
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            string json = JsonConvert.SerializeObject(elements);
            File.WriteAllText(dataPath + fName, json);
        }
    }
}
