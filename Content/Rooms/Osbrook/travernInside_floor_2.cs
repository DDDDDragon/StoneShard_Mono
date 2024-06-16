using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Tiles;
using StoneShard_Mono.Content.Tiles.InRoom;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono.Content.Rooms.Osbrook
{
    public class tavernInside_floor_2 : Room
    {
        public override void SetDefaults()
        {
            base.SetDefaults(); 

            BackGround = Main.TextureManager[TexType.Tile, "Osbrook\\tavernInside_floor_2\\background"];

            inHouse = true;

            TileMap = new int[,]
            {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1 },
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

            RegisterEntity(ContentInstance<Door>.NewDoor<tavernInside_floor_1>("Osbrook\\tavernInside_floor_2\\downfloor", new(10, 2)), new(11, 2));
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.RoomID[GetType().Name] = 2;
        }

        public override void Enter(Room lastRoom)
        {
            switch(lastRoom.Name)
            {
                case "tavernInside_floor_1" :

                    Player = ContentInstance<Jonna>.Instance;

                    ContentInstance<Jonna>.Instance.GoToRoom(this, new(10, 2));

                    break;
            }
        }
    }
}
