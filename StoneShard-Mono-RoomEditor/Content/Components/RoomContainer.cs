using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono_RoomEditor.Content.Rooms;
using StoneShard_Mono_RoomEditor.Extensions;
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

        public RoomContainer(RoomData roomData)
        {
            Room = new Room();
            Room.SetDefaults();
            Room.LoadData(roomData);
        }

        public override int Width => (int)Room.RealSize.X;

        public override int Height => (int)Room.RealSize.Y;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            spriteBatch.Change(sortMode: SpriteSortMode.BackToFront);
            Room.Draw(spriteBatch, gameTime);
            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Room.Position = Position;
            Room.Update(gameTime);
        }

        public void LoadData(RoomData roomData) => Room.LoadData(roomData);
    }
}
