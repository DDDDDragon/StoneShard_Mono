using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono_RoomEditor.Content.Components;
using StoneShard_Mono_RoomEditor.Content.Scenes;
using StoneShard_Mono_RoomEditor.Extensions;

namespace StoneShard_Mono_RoomEditor.Content
{
    public class MainScene : Scene
    {
        public MainScene()
        {
            var screen = new SizeContainer(1920, 1200);

            var sideContainer = new SizeContainer(360, Main.GameHeight);
            sideContainer.BackgroundColor = Color.White;
            sideContainer.BorderWidth.Set(0, 0, 2, 0);

            var mainContainer = new SizeContainer(1560, Main.GameHeight);
            mainContainer.RelativePosition = new(360, 0);
            mainContainer.BackgroundColor = Color.White;

            var roomViewContainer = new SizeContainer(1560, 900);

            var roomView = new ZoomableContainer(1500, 840);
            roomView.RelativePosition = new(30, 30);
            roomView.BorderWidth.Set(1);
            roomView.Initialize();

            var rect = new SizeContainer(1500, 840);
            rect.BorderWidth.Set(1);
            roomView.RegisterChild(rect);

            var room = new RoomContainer();
            roomView.RegisterChild(room);

            roomViewContainer.RegisterChild(roomView);
            mainContainer.RegisterChild(roomViewContainer);

            screen.RegisterChild(sideContainer);
            screen.RegisterChild(mainContainer);

            Components.Add(screen);

            //c = new Camera();
            //c.DoInitialize(1920, 1200);
            //c.Transform = Matrix.Identity;
            //c.Position = new(0, 0);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, transformMatrix: );
            base.Draw(spriteBatch, gameTime);
            //spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone);
        }

        public override void Update(GameTime gameTime)
        {
            //c.DoUpdate(gameTime);
            base.Update(gameTime);
        }
    }
}
