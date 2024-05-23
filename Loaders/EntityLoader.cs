using System;
using System.Linq;
using System.Reflection;
using StoneShard_Mono.Content;

namespace StoneShard_Mono.Loaders
{
    public static class EntityLoader
    {
        public static int EntityCount { get; private set; }

        private static Assembly _gameAssembly => Assembly.GetExecutingAssembly();

        public static void Load()
        {
            
        }
    }
}
