using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono_RoomEditor.Content.Rooms;
using StoneShard_Mono_RoomEditor.Extensions;

namespace StoneShard_Mono_RoomEditor.Content.Tiles.InRoom
{
    public class Door : Tile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            TileSize = new(1, 1);

            UseAdditive = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, sortMode: SpriteSortMode.BackToFront, blendState: BlendState.Additive);
            base.Draw(spriteBatch, gameTime);
            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, sortMode: SpriteSortMode.BackToFront);
        }
    }
}
