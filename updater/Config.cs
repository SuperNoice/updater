using System.Collections.Generic;
using System.IO;

namespace updater
{
    class Config
    {
        public string Version { get; set; }
        public string UpdateInfoLink { get; set; }
        public string UpdateFileLink { get; set; }

        private Config() { }

        public static Config Parse(string filePath)
        {
            var config = new Config();

            foreach (string line in File.ReadLines(filePath))
            {
                string[] keyVal = line.Trim().Split('=');
                var option = new KeyValuePair<string, string>(keyVal[0], keyVal[1]);
                switch (option.Key)
                {
                    case "version":
                        config.Version = option.Value;
                        break;
                    case "updateInfoLink":
                        config.UpdateInfoLink = option.Value;
                        break;
                    case "updateFileLink=":
                        config.UpdateFileLink = option.Value;
                        break;
                    default:
                        break;
                }
            }

            return config;
        }

        public void Save(string filePath)
        {
            StreamWriter configFile = File.AppendText(filePath);
            configFile.WriteLine($"version={Version}");
            configFile.WriteLine($"updateInfoLink={UpdateInfoLink}");
            configFile.WriteLine($"updateFileLink={UpdateFileLink}");
            configFile.Flush();
            configFile.Close();
        }

        public static void CreateClearConfigFile(string filePath)
        {
            StreamWriter configFile = File.AppendText(filePath);
            configFile.WriteLine($"version=");
            configFile.WriteLine($"updateInfoLink=");
            configFile.WriteLine($"updateFileLink=");
            configFile.Flush();
            configFile.Close();
        }
    }
}
