using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StoneShard_Mono_RoomEditor.Extensions
{
    public static class SpriteBatchExt
    {
        internal static Texture2D pixel
        {
            get
            {
                var tex = new Texture2D(Main.Instance.GraphicsDevice, 1, 1);
                tex.SetData(new Color[] { Color.White });
                return tex;
            }
        }

        public static void Rebegin(this SpriteBatch spriteBatch, SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
        {
            spriteBatch.End();
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        public static void DrawLine(this SpriteBatch batch, Line line, Color color)
        {
            float radian = line.ToVector2().GetRadian();
            if (pixel is not null)
                batch.Draw(pixel, line.Start, null, color, radian, Vector2.Zero, new Vector2(Vector2.Distance(line.Start, line.End), 1f), SpriteEffects.None, 0);
        }

        public static void DrawLine(this SpriteBatch batch, Vector2 start, Vector2 end, Color color)
        {
            batch.DrawLine(new Line(start, end), color);
        }

        public static void DrawRectangle(this SpriteBatch batch, Rectangle rect, Color color)
        {
            batch.Draw(pixel, rect, color);
        }

        public static void DrawRectangle(this SpriteBatch batch, Rectangle rect, Color color, float rotation = 0f, Vector2 origin = default, float scale = 1, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1)
        {
            batch.Draw(pixel, rect, null, color, rotation, origin, effects, layerDepth);
        }
    }

    public struct Line
    {
        public Vector2 Start;
        public Vector2 End;
        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
        public Vector2 ToVector2()
        {
            return End - Start;
        }
    }
}
