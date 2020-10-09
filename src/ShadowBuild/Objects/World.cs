using System;
using System.Collections.Generic;
using ShadowBuild.Exceptions;

namespace ShadowBuild.Objects
{
    public sealed class World
    {
        public static readonly World Default = new World("default");
        public static List<World> All { get; private set; } = new List<World> { World.Default };
        public List<RenderableObject> Objects { get; internal set; } = new List<RenderableObject>();
        public static World ActualWorld = World.Default;

        public string Name { get; private set; }

        public World(string Name)
        {
            this.Name = Name;
        }
        public static void Setup(World world)
        {
            foreach(World w in All)
                if(w.Name == world.Name)
                    throw new WorldNameException("There is another world with name " + world.Name);
            All.Add(world); 
        }
        private static World FindWithoutException(string name)
        {
            foreach (World w in All)
                if (w.Name == name)
                    return w;
            return null;
        }
        public static World Find(string name)
        {
            World w= FindWithoutException(name);
            if (w == null)
                throw new WorldNameException("There is no world named "+name);
            return w;
        }
    }
}
