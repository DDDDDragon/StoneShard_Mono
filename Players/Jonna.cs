using Microsoft.Xna.Framework;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono.Players
{
    public class Jonna : Player
    {
        public override string ID { get => "Jonna"; }

        public Jonna() : base() 
        {
            Body = Main.TextureManager[TexType.Entity, "human_female"];
            HeadOffset = new Vector2(0, 0);
            BodyOffset = new Vector2(0, 2);
        }
    }
}
