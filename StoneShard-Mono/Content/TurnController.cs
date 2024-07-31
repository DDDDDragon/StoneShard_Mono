using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StoneShard_Mono.Content
{
    public class TurnController : GameContent
    {
        public bool PlayerTurn = true;

        public void EndPlayerTurn()
        {

            PlayerTurn = false;
        }

        public void EndNPCTurn()
        {

            PlayerTurn = true;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }
    }
}
