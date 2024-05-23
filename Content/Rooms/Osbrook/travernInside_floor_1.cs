using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Tiles;
using StoneShard_Mono.Content.Tiles.InRoom;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono.Content.Rooms.Osbrook
{
    public class tavernInside_floor_1 : Room
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Player = ContentInstance<Jonna>.Instance;

            RegisterEntity(Player, new(6, 11));

            Player.LastTilePos = new(6, 11);

            Player.TargetTilePos = new(6, 11);

            RegisterEntity(ContentInstance<OpaqueFore>.NewOpaqueFore("Osbrook\\tavernInside_floor_1\\opaqueFore1"), new(1, 5));

            RegisterEntity(ContentInstance<TransparentFore>.NewTransparentFore("Osbrook\\tavernInside_floor_1\\transparentFore1"), new(1, 10));

            RegisterEntity(ContentInstance<TransparentFore>.NewTransparentFore("Osbrook\\tavernInside_floor_1\\transparentFore2"), new(8, 5));

            RegisterEntity(ContentInstance<Door>.NewDoor("Osbrook\\tavernInside_floor_1\\upfloor", new(13, 3), new(8, -8)), new(12, 1));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 4), new(1, 7));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 5, drawOffset: new(0, 12)), new(6, 7));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 5), new(1, 10));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 6), new(6, 10));
        }

        public override void SetStaticDefaults()
        {
            RoomName = "tavernInside_floor_1";

            BackGround = Main.TextureManager[TexType.Tile, "Osbrook\\tavernInside_floor_1\\background"];

            inHouse = true;

            TileMap = new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
        }
    }
}
