using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono_RoomEditor.Extensions;
using System;

namespace StoneShard_Mono_RoomEditor.Content.Components
{
    public class ZoomableContainer : SizeContainer
    {
        public Matrix View;

        public Matrix Projection;

        public Matrix Transform => View * Projection;

        public float ZoomScale;

        public Vector2 CameraPosition;

        public ZoomableContainer(int width, int height) 
        { 
            _width = width;
            _height = height;
        }

        public ZoomableContainer(Vector2 size)
        {
            _width = (int)size.X;
            _height = (int)size.Y;
        }

        public void Initialize()
        {
            CameraPosition = new(0, 0);
            ZoomScale = 1f;
            Projection = Matrix.Identity;
            View = Matrix.Identity;
        }

        public void MoveCamera(Vector2 vec)
        {
            CameraPosition += vec;
        }

        public void ZoomCamera(float scale)
        {
            ZoomScale *= scale;
        }

        public Vector2 ConvertScreenToWorld(Vector2 location)
        {
            Vector3 t = new Vector3(location, 0);
            t = Main.Graphics.GraphicsDevice.Viewport.Unproject(t, Projection, View, Matrix.Identity);
            return new Vector2(t.X, t.Y);
        }

        public Vector2 ConvertWorldToScreen(Vector2 location)
        {
            Vector3 t = new Vector3(location, 0);
            t = Main.Graphics.GraphicsDevice.Viewport.Project(t, Projection, View, Matrix.Identity);
            return new Vector2(t.X, t.Y);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init || !Visible) return;
            if (BackgroundColor != default)
                spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, Width, Height), BackgroundColor * _alpha);

            spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, BorderWidth.X, _height), BorderColor);
            spriteBatch.DrawRectangle(new((int)Position.X, (int)Position.Y, _width, BorderWidth.Y), BorderColor);
            spriteBatch.DrawRectangle(new(_width - BorderWidth.Z + (int)Position.X, (int)Position.Y, BorderWidth.Z, Height), BorderColor);
            spriteBatch.DrawRectangle(new((int)Position.X, _height - BorderWidth.W + (int)Position.Y, _width, BorderWidth.W), BorderColor);

            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, 
                transformMatrix: Transform);
            spriteBatch.GraphicsDevice.Viewport = new((int)Position.X, (int)Position.Y, _width, _height);

            foreach (var component in Children)
                component.Draw(spriteBatch, gameTime);

            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone);
            spriteBatch.GraphicsDevice.Viewport = new(0, 0, 1920, 1200);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                MoveCamera(new(-2, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                MoveCamera(new(2, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                MoveCamera(new(0, -2));
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                MoveCamera(new(0, 2)); var deltaWheel = _currentMouse.ScrollWheelValue - _previousMouse.ScrollWheelValue;
            if (deltaWheel > 0)
                ZoomCamera(1.25f);
            if (deltaWheel < 0)
                ZoomCamera(0.8f);

            View = Matrix.CreateTranslation(new(-Size / 2, 0)) *
                Matrix.CreateScale(ZoomScale) *
                Matrix.CreateTranslation(new(Size / 2, 0));

            foreach (var child in Children)
            {
                child.Position = child.RelativePosition;
                child.Update(gameTime);
            }

            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].shouldCollect)
                {
                    Children.RemoveAt(i);
                    i--;
                }
            }

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRect = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRect.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed && CanClick)
                {
                    Clicked = true;
                }
            }

            if (!_init) _init = true;

            if (HorizontalMiddle) RelativePosition.X = (Parent.Width - Width) / 2;
            if (VerticalMiddle) RelativePosition.Y = (Parent.Height - Height) / 2;

            switch (Anchor)
            {
                case Anchor.Left:
                    RelativePosition.X = 0;
                    break;
                case Anchor.Right:
                    RelativePosition.X = Parent.Width - Width;
                    break;
                case Anchor.Top:
                    RelativePosition.Y = 0;
                    break;
                case Anchor.Bottom:
                    RelativePosition.Y = Parent.Height - Height;
                    break;
                case Anchor.None:
                    break;
            }

            DrawOffset = new(0, 0);

            if (!_init) _init = true;
        }
    }
}
