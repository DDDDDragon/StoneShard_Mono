using Microsoft.Xna.Framework;

namespace StoneShard_Mono.Content.Animations
{
    public class Animation : GameContent
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

        public override void Update(GameTime gameTime)
        {
            if (MaxTime != 0 && Time == MaxTime) End();
            if (!_initialized)
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
