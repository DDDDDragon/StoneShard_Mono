using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Tiles;
using StoneShard_Mono.Content.Tiles.InRoom;
using StoneShard_Mono.Content.Tiles.InRoom.Decorations;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono.Content.Rooms.Osbrook
{
    public class tavernInside_floor_1 : Room
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            BackgroundPath = "Osbrook\\tavernInside_floor_1\\background";
            Background = Main.TextureManager[TexType.Tile, BackgroundPath];

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

            RegisterEntity(ContentInstance<OpaqueFore>.NewTile(texID: "Osbrook\\tavernInside_floor_1\\opaqueFore1"), new(1, 5));

            RegisterEntity(ContentInstance<TransparentFore>.NewTile(texID: "Osbrook\\tavernInside_floor_1\\transparentFore1"), new(1, 10));

            RegisterEntity(ContentInstance<TransparentFore>.NewTile(texID: "Osbrook\\tavernInside_floor_1\\transparentFore2"), new(8, 5));

            RegisterEntity(ContentInstance<Door>.NewDoor<tavernInside_floor_2>("Osbrook\\tavernInside_floor_1\\upfloor", new(13, 3), new(8, -8)), new(12, 1));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 4), new(1, 7));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 5, drawOffset: new(0, 12)), new(6, 7));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 5), new(1, 10));

            RegisterEntity(ContentInstance<Table>.NewTile(subID: 6), new(6, 10));

            RegisterEntity(ContentInstance<Pelt>.NewTile(subID: 1, drawOffset: new(6, -10)), new(4, 8));

            RegisterEntity(ContentInstance<Pelt>.NewTile(subID: 4, drawOffset: new(16, -28)), new(4, 3));
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void Enter(Room lastRoom)
        {
            switch (lastRoom.Name)
            {
                case "tavernInside_floor_2":

                    LocalPlayer = ContentInstance<Jonna>.Instance;

                    ContentInstance<Jonna>.Instance.GoToRoom(this, new(13, 4));

                    break;
                default:
                    LocalPlayer = ContentInstance<Jonna>.Instance;

                    ContentInstance<Jonna>.Instance.GoToRoom(this, new(6, 11));

                    break;
            }

            base.Enter(lastRoom);
        }
    }
}
