using StoneShard_Mono.UIComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneShard_Mono.Players;

namespace StoneShard_Mono.Animations
{
    public class EntityMoveAnimation : Animation
    {
        public EntityMoveAnimation(Vector2 end, Entity target = null, string tag = "") : base(target, 15, tag)
        {
            _end = end;

            _maxRotation = ((float)Main.Random.NextDouble() - 0.5f) / 3;
            _maxRotation += _maxRotation > 0 ? 0.05f : -0.05f;
        }

        private Vector2 _start;

        private Vector2 _end;

        private float _maxRotation;

        private float _maxOffset;

        public Vector2 StartPos => _start;

        public Vector2 EndPos => _end;

        public override void SetDefaults()
        {
            _start = Target.TilePosition;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Time++;

            if (Vector2.Distance(Target.TilePosition, _end) >= 0.001f)
            {
                var dir = (_end - _start).X;

                Target.Direction = dir > 0 ? 1 : (dir == 0 ? Target.Direction : -1);
                Target.IsMove = true;

                if (Math.Abs(Target.Rotation - _maxRotation) >= 0.001f)
                    Target.Rotation += _maxRotation / 2;

                if (_maxOffset < 6)
                {
                    Target.DrawOffset += new Vector2(0, -1.5f);
                    _maxOffset += 1.5f;
                }

                Target.TilePosition += (_end - _start) / 8;
            }
            else
            {
                if (Target.Rotation > 0)
                    Target.Rotation -= _maxRotation / 3;
                else
                    Target.Rotation = 0;

                if (_maxOffset > 0)
                {
                    Target.DrawOffset += new Vector2(0, 1);
                    _maxOffset -= 1;
                }
                Target.TilePosition = _end;
            }
        }

        public override void End()
        {
            base.End();

            Target.IsMove = false;
        }
    }
}
