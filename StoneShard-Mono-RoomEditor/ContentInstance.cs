﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneShard_Mono_RoomEditor.Content.Players;
using StoneShard_Mono_RoomEditor.Content.Rooms;
using StoneShard_Mono_RoomEditor.Managers;
using StoneShard_Mono_RoomEditor.Content.Tiles;
using StoneShard_Mono_RoomEditor.Content;
using StoneShard_Mono_RoomEditor.Content.Tiles.InRoom;

namespace StoneShard_Mono_RoomEditor
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
                entity.DrawOffset = drawOffset;
                return entity;
            }
            else return null;
        }

        public static T NewTile(int subID = 1, Vector2 drawOffset = default, string texID = "")
        {
            var instance = NewEntity(drawOffset);
            if (instance is Tile tile)
            {
                tile.SubID = subID;
                if (texID != "")
                {
                    tile.Texture = Main.TextureManager[TexType.Tile, texID];
                    tile.TexturePath = texID;
                }
                return tile as T;
            }
            else return null;
        }

        public static Door NewDoor<R>(string texID, Vector2 realPos, Vector2 drawOffset = default) where R : Room
        {
            var instance = NewEntity(drawOffset);
            if(instance is Door door)
            {
                door.Texture = Main.TextureManager[TexType.Tile, texID];
                door.TexturePath = texID;
                return door;
            }
            else return null;
        }
    }
}
