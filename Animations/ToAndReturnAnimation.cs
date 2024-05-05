using StoneShard_Mono.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Animations
{
    public class ToAndReturnAnimation : DirectMoveAnimation
    {
        public ToAndReturnAnimation(Component target, float maxTime, Vector2 dir, int speed, int times, string tag = "")
            : base(target, maxTime, dir, speed, tag) { Times = times; }

        public int Times;

        public override void End()
        {
            if(Times > 0)
                Target.CurrentAnimation = new ToAndReturnAnimation(Target, MaxTime, -MoveDirection, MoveSpeed, Times - 1, Tag);
            base.End();
        }
    }
}
