using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneShard_Mono.Content;
using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Content.Tiles;
using StoneShard_Mono.Content.Tiles.InRoom;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono
{
    public static class ContentInstance<T> where T : GameContent
    {
        public static T Instance { get; internal set; }

        private static List<T> _instances = new List<T>();

        public static IReadOnlyList<T> Instances { get => _instances.AsReadOnly(); private set => _instances = (List<T>)value; }

        public static bool OneInstance()
        {
            return Instance is Player || Instance is Room;
        }

        static ContentInstance()
        {
            Instance = Activator.CreateInstance<T>();

            Instance.SetStaticDefaults();

            Instance.SetDefaults();
        }

        public static T NewInstance()
        {
            if (OneInstance() && Instance != null)
                return Instance;

            Instance = Activator.CreateInstance<T>();

            Instance.SetDefaults();

            _instances.Add(Instance);

            return Instance;
        }

        public static Entity NewEntity(Vector2 drawOffset = default)
        {
            var instance = NewInstance();
            if (instance is Entity entity)
            {
                entity.DrawOffset += drawOffset;
                return entity;
            }
            else return null;
        }

        public static OpaqueFore NewOpaqueFore(string texID)
        {
            var instance = NewEntity();
            if (instance is OpaqueFore ofore)
            {
                ofore.Texture = Main.TextureManager[TexType.Tile, texID];
                return ofore;
            }
            else return null;
        }

        public static TransparentFore NewTransparentFore(string texID)
        {
            var instance = NewEntity();
            if (instance is TransparentFore tfore)
            {
                tfore.Texture = Main.TextureManager[TexType.Tile, texID];
                return tfore;
            }
            else return null;
        }

        public static T NewTile(int subID = 1, Vector2 drawOffset = default)
        {
            var instance = NewEntity(drawOffset);
            if (instance is Tile tile)
            {
                tile.SubID = subID;
                return tile as T;
            }
            else return null;
        }

        public static Door NewDoor(string texID, Vector2 realPos, Vector2 drawOffset = default)
        {
            var instance = NewEntity(drawOffset);
            if(instance is Door door)
            {
                door.Texture = Main.TextureManager[TexType.Tile, texID];
                door.realPos = realPos;
                return door;
            }
            else return null;
        }
    }
}
