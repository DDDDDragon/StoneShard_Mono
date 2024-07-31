using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoneShard_Mono_RoomEditor.Managers
{
    public class LocalizationManager : AssetManager<Dictionary<string, string>>
    {
        public Dictionary<string, Dictionary<string, string>> Localizations = new();

        public override void LoadOne(string dir, Dictionary<string, Dictionary<string, string>> dictronary)
        {
            var path = Path.Combine(Main.GamePath, "Content", dir);
            if(Directory.Exists(path))
            {
                Directory.GetFiles(path, "*.json").ToList().ForEach(file =>
                {
                    var localization = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                        File.ReadAllText(file)
                    );
                    dictronary.Add(Path.GetFileNameWithoutExtension(file), localization);
                });
            }
        }

        public string this[string language, string key]
        {
            get {
                if (!Localizations.ContainsKey(language) || !Localizations[language].ContainsKey(key))
                    return "Text not found";
                else return Localizations[language][key];
            }
        }
    }
}
