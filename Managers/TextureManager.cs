using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace StoneShard_Mono.Managers
{
    public class TextureManager : AssetManager<Texture2D>
    {
        public Dictionary<string, Texture2D> Entity = new();

        public Dictionary<string, Texture2D> UI = new();

        public override void LoadOne(string dir, Dictionary<string, Texture2D> dictronary)
        {
            var path = Path.Combine(Main.GamePath, "Content\\Textures", dir);
            if (Directory.Exists(path))
            {
                Directory.GetFiles(path, "*.png", SearchOption.AllDirectories).ToList().ForEach(file =>
                    dictronary.Add(file.Replace($"{path}\\", "").Replace(".png", ""), 
                    Texture2D.FromFile(Main.Instance.GraphicsDevice, file)));
            }
        }

        public Texture2D this[TexType type, string ID, int scale = 1]
        {
            get
            {
                Texture2D src;
                Texture2D ret;
                Color[] sourceDatas;
                Color[,] returnDatas;
                switch (type)
                {
                    case TexType.Entity:
                        src = Entity[ID];
                        break;
                    case TexType.UI:
                        src = UI[ID];
                        break;
                    default: return null;
                }
                ret = new Texture2D(Main.Instance.GraphicsDevice, src.Width * scale, src.Height * scale);
                sourceDatas = new Color[src.Width * src.Height];
                returnDatas = new Color[ret.Width , ret.Height];
                src.GetData(sourceDatas);
                
                for(int i = 0; i < src.Width; i++)
                {
                    for(int j = 0; j < src.Height; j++)
                    {
                        if (sourceDatas[j * src.Width + i].A == 0) continue;
                        for(int k = 0; k < scale * scale; k++)
                            returnDatas[i * scale + k % scale, j * scale + k / scale] = sourceDatas[j * src.Width + i];
                    }
                }
                Color[] retData = new Color[returnDatas.Length];
                for (int i = 0; i < retData.Length; i++)
                    retData[i] = returnDatas[i % ret.Width, i / ret.Width];
                ret.SetData(retData);
                return ret;
            }
        }

    }

    public enum TexType : int
    {
        Entity,
        UI
    }
}
