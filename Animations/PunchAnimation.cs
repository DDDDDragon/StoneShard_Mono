using StoneShard_Mono.Components;
using StoneShard_Mono.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace StoneShard_Mono.Animations
{
    public class PunchAnimation : Animation
    {
        public PunchAnimation(Component target, float maxTime, float strength, string tag = "") : base(target, maxTime, tag)
        {
            _strength = strength;

            _direction = ((float)Main.Random.NextDouble() * 6.2831855f).GetAngle();

            _startPosition = target.Position + new Vector2(target.Width, target.Height) / 2;

            _distanceFalloff = -1f;

            _vibrationCyclesPerSecond = 1f;

            _framesToLast = (int)maxTime;
        }

        public float Remap(float fromValue, float fromMin, float fromMax, float toMin, float toMax, bool clamped = true)
        {
            return MathHelper.Lerp(toMin, toMax, GetLerpValue(fromMin, fromMax, fromValue, clamped));
        }

        public float GetLerpValue(float from, float to, float t, bool clamped = false)
        {
            if (clamped)
            {
                if (from < to)
                {
                    if (t < from)
                    {
                        return 0f;
                    }
                    if (t > to)
                    {
                        return 1f;
                    }
                }
                else
                {
                    if (t < to)
                    {
                        return 1f;
                    }
                    if (t > from)
                    {
                        return 0f;
                    }
                }
            }
            return (t - from) / (to - from);
        }

        public override void Update(GameTime gameTime)
        {
            float scaleFactor = (float)Math.Cos((double)(_framesLasted / 60f * _vibrationCyclesPerSecond * 6.2831855f));
            float scaleFactor2 = Remap(_framesLasted, 0f, _framesToLast, 1f, 0f, true);
            float scaleFactor3 = Remap(Vector2.Distance(_startPosition, Target.Position + new Vector2(Target.Width, Target.Height) / 2 + Target.DrawOffset), 0f, _distanceFalloff, 1f, 0f, true);
            if (_distanceFalloff == -1f)
                scaleFactor3 = 1f;
            Target.DrawOffset += _direction * scaleFactor * _strength * scaleFactor2 * scaleFactor3;
            _framesLasted++;
            if (_framesLasted >= _framesToLast)
                End();
        }
        private int _framesToLast;
        private Vector2 _startPosition;
        private Vector2 _direction;
        private float _distanceFalloff;
        private float _strength;
        private float _vibrationCyclesPerSecond;
        private int _framesLasted;
    }
}
