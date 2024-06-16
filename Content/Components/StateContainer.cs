using StoneShard_Mono.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Content.Components
{
    public class StateContainer : Container
    {
        public StateContainer(Vector2 size = default, EventHandler click = null, EventHandler<GameTime> updating = null,
            EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> draw = null)
        {
            States = new();
            _width = (int)size.X;
            _height = (int)size.Y;
            OnClick += click != null ? click : (sender, args) => { };
            Drawing += draw != null ? draw : (sender, SpriteBatch) => { };
            Updating += updating != null ? updating : (sender, SpriteBatch) => { };
            var state = new SizeContainer(size);
            state.id = "state";
            RegisterChild(state);
        }

        public Dictionary<string, List<Component>> States;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Drawing;

        public event EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)> Hovering;

        public event EventHandler<GameTime> Updating;

        public string CurrentState = "";

        public void SwitchToState(string state)
        {
            SelectChildById<SizeContainer>("state").Children = States[state];
            CurrentState = state;
        }

        public bool RegisterState(string state, params Component[] components)
        {
            if (States.ContainsKey(state))
                return false;
            else
                States.Add(state, new());
            foreach (var component in components)
            {
                component.Parent = SelectChildById<SizeContainer>("state");
                States[state].Add(component);
            }
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init || !Visible) return;
            base.Draw(spriteBatch, gameTime);
            Drawing?.Invoke(this, (spriteBatch, gameTime));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Updating?.Invoke(this, gameTime);
        }
    }
}
