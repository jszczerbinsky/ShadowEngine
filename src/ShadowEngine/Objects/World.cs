using ShadowEngine.Exceptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowEngine.Objects
{

    /// <summary>
    /// World class.
    /// With this class you can manage game worlds 
    /// Only one world can be rendered at once
    /// Objects will not affect another objects from different world
    /// </summary>
    public sealed class World
    {
        /// <value>Gets default world</value>
        public static readonly World Default = new World("default");

        /// <value>Gets all worlds</value>
        public static Collection<World> All { get; private set; } = new Collection<World> { World.Default };

        /// <value>Gets all renderable objects in this world</value>
        public Collection<RenderableObject> Objects { get; internal set; } = new Collection<RenderableObject>();

        /// <value>Actual rendered world</value>
        public static World ActualWorld = World.Default;

        /// <value>World name</value>
        public string Name { get; private set; }

        public World(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Sets up world
        /// </summary>
        public static void Setup(World world)
        {
            foreach (World w in All)
                if (w.Name == world.Name)
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

        /// <summary>
        /// Finds world by name. World has to be set up first.
        /// </summary>
        public static World Find(string name)
        {
            World w = FindWithoutException(name);
            if (w == null)
                throw new WorldNameException("There is no world named " + name);
            return w;
        }
    }
}
