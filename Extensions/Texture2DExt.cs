using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StoneShard_Mono.Extensions
{
    public static class Texture2DExt
    {
        public static Vector2 GetSize(this Texture2D tex)
        {
            return new(tex.Width, tex.Height);
        }
    }
}
