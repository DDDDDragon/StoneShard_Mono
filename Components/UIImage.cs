using StoneShard_Mono.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StoneShard_Mono.Components
{
    public class UIImage : Component
    {
        public UIImage(string texID, int scale = 1, Vector2 postion = default, float rotation = 0, TexType type = TexType.UI,
            bool horizontalMiddle = false, bool verticalMiddle = false, int frame = 1, int frameMaxTime = 1,
            EventHandler changeFrame = null)
        {
            _texture = Main.TextureManager[type, texID, scale];
            Scale = scale;
            Position = postion == default ? new(0, 0) : postion;
            Rotation = rotation;
            HorizontalMiddle = horizontalMiddle;
            VerticalMiddle = verticalMiddle;
            Frame = frame;
            FrameMaxTime = frameMaxTime;
            FrameTimer = new();
            ChangeFrame = changeFrame == null ? (sender, args) =>
            {
                FrameTimer[0]++;
                if (FrameTimer[0] == FrameMaxTime)
                {
                    CurrentFrame++;
                    FrameTimer[0] = 0;
                }
                if (CurrentFrame > Frame - 1) CurrentFrame = 0;
            } : changeFrame;
        }

        public override int Height => base.Height / Frame;

        public int Frame;

        public int CurrentFrame;

        public Point FramePos => new(0, CurrentFrame * (int)Size.Y);

        public Timer FrameTimer;

        public int FrameMaxTime;

        public EventHandler ChangeFrame;

        public Vector2 Size => new(Width, Height);

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            var destination = new Rectangle(Position.ToPoint() + (Size / 2).ToPoint() + DrawOffset.ToPoint(), Size.ToPoint());
            spriteBatch.Draw(_texture, destination, new(FramePos, Size.ToPoint()), Color.White * Alpha, Rotation, Size / 2, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ChangeFrame?.Invoke(this, EventArgs.Empty);
        }
    }
}
