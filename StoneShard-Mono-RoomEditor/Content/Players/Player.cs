using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StoneShard_Mono_RoomEditor.Managers;
using StoneShard_Mono_RoomEditor.Content.Components;
using StoneShard_Mono_RoomEditor.Extensions;
using StoneShard_Mono_RoomEditor.Content.Rooms;

namespace StoneShard_Mono_RoomEditor.Content.Players
{
    public abstract class Player : Entity
    {
        public int Direction = 1;

        public void GoToRoom(Room room, Vector2 pos)
        {
            CurrentRoom = room;

            room.LocalPlayer = this;

            room.RegisterEntity(this, pos);
        }

        public override void SetDefaults()
        {
            Timer = new Timer();

            HeadNormal = Main.TextureManager[TexType.Entity, $"Player\\{ID}\\head_normal"];

            Shadow = Main.TextureManager[TexType.Entity, "shadow_small"];

            _offsets = new Vector2[] { new(-1, -1), new(-1, 0), new(0, 1), new(1, 0), new(1, -1), new(1, 0), new(0, 1), new(-1, 0) };

            Rotation = 0;

            base.SetDefaults();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Shadow, Position + new Vector2((Width - Shadow.Width) / 2 + 6, Height + 2), null, Color.Black * 0.4f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1 - TilePosition.Y / 1000 + 0.0003f);

            var drawPos = Position + DrawOffset;

            spriteBatch.Draw(Body, drawPos + BodyOffset + Rectangle.Size.ToVector2() / 2 + new Vector2(6 * -Direction, 6), new Rectangle(0, Height * (int)Timer[2], Width, Height), Color.White,
                Rotation, Rectangle.Size.ToVector2() / 2, new Vector2(-Direction, 1f), SpriteEffects.None, 1 - TilePosition.Y / 1000 + 0.0002f);

            var headRect = new Rectangle(0, (HeadNormal.Height) / 4 * (int)Timer[1], HeadNormal.Width, HeadNormal.Height / 4);

            spriteBatch.Draw(HeadNormal, drawPos + HeadOffset + Rectangle.Size.ToVector2() / 2 + new Vector2(6 * -Direction, 6), headRect, Color.White,
                Rotation, Rectangle.Size.ToVector2() / 2, new Vector2(-Direction, 1f), SpriteEffects.None, 1 - TilePosition.Y / 1000 + 0.0001f);
        }

        public Texture2D HeadNormal;

        public Texture2D Body;

        public Texture2D Shadow;

        public Vector2 HeadOffset;

        public Vector2 BodyOffset;

        private Vector2[] _offsets;

        public override Vector2 Position
        {
            get => (TilePosition + CurrentRoom.TilePostion) * Main.TileSize + new Vector2(0, Main.TileSize / 2 - Height);
        }

        public override int Width => Body.Width;

        public override int Height => Body.Height / 3;

        public override Rectangle Rectangle => new((int)Position.X, (int)Position.Y, Width + 12, Height + 12);

        public Timer Timer;
    }
}
