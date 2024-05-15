using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Tiles
{
    public class Table : Tile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            TileSize = new(2, 1);
        }
    }
}
