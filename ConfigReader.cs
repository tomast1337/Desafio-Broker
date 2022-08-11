using Newtonsoft.Json;

namespace Program
{
    class ConfigReader
    {
        // Data from the config.json
        static public Dictionary<string, string> config = new Dictionary<string, string>();
        // Flag indicating if the config file was read
        static bool configRead = false;

        // Reads the config.json file and returns a dictionary with the configuration
        public static Dictionary<string, string> ReadConfig()
        {
            if (configRead)
                return config;
            var necessaryKeys = new List<string> { "PrimaryDomain",
                                                   "PrimaryPort",
                                                   "UsernameEmail",
                                                   "UsernamePassword",
                                                   "DestinationEmail" };
            
            var configFile = File.ReadAllText("config.json");
            
            var configJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(configFile);
            
            foreach (var key in necessaryKeys)
            {
                if (!configJson.ContainsKey(key))
                    throw new Exception($"The config.json file is missing the key {key}");
                ConfigReader.config.Add(key, configJson[key]);
            }
            
            configRead = true;
            
            return config;
        }
    }

}