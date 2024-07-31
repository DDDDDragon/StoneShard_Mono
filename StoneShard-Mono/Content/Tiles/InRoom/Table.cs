using Microsoft.Xna.Framework;
using StoneShard_Mono.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Content.Tiles.InRoom
{
    public class Table : Tile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.TileID[GetType().Name] = 2;
        }

        public override void SetDefaults()
        {
            SubID = 1;

            DrawOffset = new(-Main.TileSize, - Main.TileSize);

            TileSize = new(2, 1);
        }

        public override void SetSubTexture(int subID)
        {
            Texture = Main.TextureManager[TexType.Tile, $"General\\table_{SubID}"];
        }
    }
}
