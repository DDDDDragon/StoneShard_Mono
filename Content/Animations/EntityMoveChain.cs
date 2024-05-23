using Microsoft.Xna.Framework;
using StoneShard_Mono.Content.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Content.Animations
{
    public class EntityMoveChain : AnimationChain
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_animationIndex == Animations.Count)
            {
                MaxTime = 0;
                Target.IsMove = false;
                return;
            }

            if (!Animations[_animationIndex].Initialized)
            {
                if (Target.CurrentPath.Count > 0) Target.CurrentPath.RemoveAt(0);

                Target.LastTilePos = (Animations[_animationIndex] as EntityMoveAnimation).StartPos;
                Target.TargetTilePos = (Animations[_animationIndex] as EntityMoveAnimation).EndPos;
            }

            Animations[_animationIndex].Update(gameTime);

            if (Animations[_animationIndex].MaxTime == 0 && Main.PlayerTurn)
            {
                if (ShouldBreak)
                {
                    MaxTime = 0;
                    Target.IsMove = false;
                    return;
                }

                _animationIndex++;
            }

            Target.IsMove = true;
        }
    }
}
