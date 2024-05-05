using StoneShard_Mono.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Animations
{
    public class Animation
    {
        public Animation(Component target, float maxTime, string tag = "") 
        {
            Target = target;
            MaxTime = maxTime;
            Time = 0;
            Tag = tag;
        }

        public string Tag;

        public static Animation Empty => new Animation(null, 0);

        public float MaxTime;

        public float Time;

        public Component Target;

        public EventHandler<Component> DoAnimation;

        public virtual void Update(GameTime gameTime)
        {
            if (MaxTime != 0)
            {
                DoAnimation?.Invoke(this, Target);

                Time += 0.05f;

                if (Time >= MaxTime) End();
            }
        }

        public virtual void End()
        {
            MaxTime = 0;
        }
    }
}
