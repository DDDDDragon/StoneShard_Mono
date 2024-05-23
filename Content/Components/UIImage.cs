using StoneShard_Mono.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using StoneShard_Mono.Extensions;

namespace StoneShard_Mono.Content.Components
{
    public class UIImage : Component
    {
        public UIImage(string texID, int scale = 2, Vector2 relativePos = default, float rotation = 0, TexType type = TexType.UI,
            bool horizontalMiddle = false, bool verticalMiddle = false, int frame = 1, int frameMaxTime = 1,
            EventHandler changeFrame = null, bool repeat = true, BlendState drawMode = null)
        {
            _texture = Main.TextureManager[type, texID, scale];
            Scale = scale;
            RelativePosition = relativePos == default ? new(0, 0) : relativePos;
            Rotation = rotation;
            HorizontalMiddle = horizontalMiddle;
            VerticalMiddle = verticalMiddle;
            Frame = frame;
            FrameMaxTime = frameMaxTime;
            FrameTimer = new();
            ChangeFrame = changeFrame == null ? (sender, args) =>
            {
                if (!Visible) return;
                FrameTimer[0]++;
                if (FrameTimer[0] == FrameMaxTime)
                {
                    CurrentFrame++;
                    FrameTimer[0] = 0;
                }
                if (CurrentFrame > Frame - 1) CurrentFrame = repeat ? 0 : Frame - 1;
            }
            : changeFrame;
            DrawMode = drawMode == null ? BlendState.AlphaBlend : drawMode;
            Repeat = repeat;
        }

        public override int Height => base.Height / Frame;

        public int Frame;

        public int CurrentFrame;

        public Point FramePos => new(0, CurrentFrame * (int)Size.Y);

        public Timer FrameTimer;

        public int FrameMaxTime;

        public EventHandler ChangeFrame;

        public BlendState DrawMode;

        public bool Repeat;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init || !Visible) return;
            spriteBatch.Rebegin(blendState: DrawMode, samplerState: SamplerState.PointClamp);
            var destination = new Rectangle(Position.ToPoint() + (Size / 2 * DrawScale + DrawOffset).ToPoint(), (Size * DrawScale).ToPoint());
            spriteBatch.Draw(_texture, destination, new(FramePos, Size.ToPoint()), Color.White * Alpha, Rotation, Size / 2, SpriteEffects.None, 0);
            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ChangeFrame?.Invoke(this, EventArgs.Empty);
        }
    }
}
