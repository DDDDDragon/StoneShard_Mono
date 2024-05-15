using StoneShard_Mono.UIComponents;
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
        public Animation(Entity target = null, int maxTime = 1, string tag = "") 
        {
            Target = target;
            MaxTime = maxTime;
            Time = 0;
            Tag = tag;
        }

        public string Tag;

        public static Animation Empty => new Animation(null, 0);

        public int MaxTime;

        public int Time;

        public virtual Entity Target { get; set; }

        private bool _initialized;

        public bool Initialized => _initialized;

        public virtual void SetDefaults()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (MaxTime != 0 && Time == MaxTime) End();
            if(!_initialized)
            {
                SetDefaults();
                _initialized = true;
            }
        }

        public virtual void End()
        {
            MaxTime = 0;
        }
    }
}
