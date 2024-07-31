using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono_RoomEditor.Content.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono_RoomEditor.Content.Components
{
    public class RoomContainer : SizeContainer
    {
        public Room Room;

        public RoomContainer()
        {
            Room = Room.Empty;
        }

        public RoomContainer(Room room)
        {
            Room = room;
        }

        public override int Width => (int)Room.RealSize.X;

        public override int Height => (int)Room.RealSize.Y;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            Room.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Room.Update(gameTime);
        }
    }
}
