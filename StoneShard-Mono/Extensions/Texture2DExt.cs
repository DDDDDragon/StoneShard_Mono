using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace StoneShard_Mono.Extensions
{
    public static class Texture2DExt
    {
        public static Vector2 GetSize(this Texture2D tex)
        {
            if(tex == null) return new Vector2(0, 0);
            return new(tex.Width, tex.Height);
        }
    }
}
