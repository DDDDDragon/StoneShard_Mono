using StoneShard_Mono.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Content.Tiles.InRoom.Decorations
{
    public class Pelt : Decoration
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.TileID[GetType().Name] = 10000;
        }

        public override void SetDefaults()
        {
            SubID = 1;

            TileSize = new(3, 2);
        }

        public override void SetSubTexture(int subID)
        {
            Texture = Main.TextureManager[TexType.Tile, $"General\\Decorations\\pelt_{SubID}"];
        }
    }
}
