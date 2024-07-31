using Microsoft.Xna.Framework;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono.Content.Players
{
    public class Jonna : Player
    {
        public override string ID { get => "Jonna"; }

        public override void SetDefaults()
        {
            Body = Main.TextureManager[TexType.Entity, "human_female"];

            base.SetDefaults();
            
            HeadOffset = new Vector2(0, 0);
            BodyOffset = new Vector2(0, 2);
        }
    }
}
