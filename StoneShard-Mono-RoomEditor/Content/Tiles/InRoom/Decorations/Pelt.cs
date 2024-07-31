using StoneShard_Mono_RoomEditor.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono_RoomEditor.Content.Tiles.InRoom.Decorations
{
    public class Pelt : Decoration
    {
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
