using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace StoneShard_Mono.Content.Animations
{
    public class AnimationChain : Animation
    {
        public AnimationChain(Entity target = null, string tag = "", params Animation[] animations) : base(target, int.MaxValue, tag)
        {
            _animationIndex = 0;

            Animations = animations.ToList();

            foreach (var anim in animations)
            {
                anim.Target = target;
            }
        }

        public override Entity Target
        {
            get => base.Target;
            set
            {
                base.Target = value;
                if (value == null) return;
                foreach (var i in Animations)
                    i.Target = value;
            }
        }

        public bool ShouldBreak;

        public List<Animation> Animations;

        protected int _animationIndex;

        public void RegisterAnimation(Animation animation)
        {
            animation.Target = Target;
            Animations.Add(animation);
        }
    }
}
