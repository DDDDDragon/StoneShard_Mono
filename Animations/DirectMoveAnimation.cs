using StoneShard_Mono.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Animations
{
    public class DirectMoveAnimation : Animation
    {
        public DirectMoveAnimation(Component target, float maxTime, Vector2 dir, int speed, string tag = "") : base(target, maxTime, tag)
        {
            MoveDirection = dir;

            MoveSpeed = speed;

            DoAnimation += (object sender, Component target) =>
            {
                target.RelativePosition += MoveDirection * MoveSpeed;
            };
        }

        public Vector2 MoveDirection;

        public int MoveSpeed;

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds % 10 <= 1)
                base.Update(gameTime);
        }
    }
}
