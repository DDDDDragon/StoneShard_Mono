using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Components
{
    public abstract class Container : Component
    {
        internal List<Component> Children = new List<Component>();

        internal int _width;

        internal int _height;

        public override int Height => _height;

        public override int Width => _width;

        public void RegisterChild(Component component)
        {
            if (SelectChildById(component.id) != null && component.id != "")
                return;
            component.Parent = this;
            Children.Add(component);
        }

        public bool childMiddle = true;

        public override void Update(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Position = child.RelativePosition + Position;
                child.Update(gameTime);
            }
            for(int i = 0; i < Children.Count; i++)
            {
                if (Children[i].shouldCollect)
                {
                    Children.RemoveAt(i);
                    i--;
                }
            }
            base.Update(gameTime);
            if (!_init) _init = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_init) return;
            base.Draw(spriteBatch, gameTime);
            foreach (var component in Children)
                component.Draw(spriteBatch, gameTime);
        }

        public void RegisterChildAt(int index, Component component, bool behind = false)
        {
            if (SelectChildById(component.id) != null)
                return;
            if (behind)
                Children.Insert(Children.Count - index, component);
            else 
                Children.Insert(index, component);
        }

        public bool ContainsChild(Predicate<Component> match)
        {
            return Children.Exists(match);
        }

        public Component SelectChild(Func<Component, bool> match)
        {
            return Children.FirstOrDefault(match, null);
        }

        public T SelectChild<T>(Func<Component, bool> match) where T : Component
        {
            if (Children.FirstOrDefault(match) is not T) return null;
            return Children.FirstOrDefault(match, null) as T;
        }

        public T SelectChildById<T>(string id) where T : Component
        {
            if (id == "") return null;
            if (Children.FirstOrDefault(c => c.id == id, null) is not T) return null;
            else return Children.FirstOrDefault(c => c.id == id, null) as T;
        }

        public Component SelectChildById(string id)
        {
            if (id == "") return null;
            return Children.FirstOrDefault(c => c.id == id, null);
        }

        public override float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                foreach (var components in Children)
                    components.Alpha = value;
            }
        }
    }
}
