using FontStashSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StoneShard_Mono.Managers
{
    public class FontManager : AssetManager<FontSystem>
    {
        public Dictionary<string, FontSystem> Fonts = new();

        public override void LoadOne(string dir, Dictionary<string, FontSystem> dictronary)
        {
            var path = Path.Combine(Main.GamePath, "Content", dir);
            if (Directory.Exists(path))
            {
                Directory.GetFiles(path, "*.ttf").ToList().ForEach(file => {
                    var font = new FontSystem();
                    font.AddFont(File.ReadAllBytes(file));
                    dictronary.Add(Path.GetFileNameWithoutExtension(file), font);
                });
            }
        }

        public SpriteFontBase this[string index, float size]
        {
            get => Fonts[index].GetFont(size);
        }
    }
}
