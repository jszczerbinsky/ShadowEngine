using ShadowBuild.Exceptions;
using System.Collections.Generic;

namespace ShadowBuild.Events
{
    /// <summary>
    /// Custom in-game events class.
    /// </summary>
    public sealed class Event
    {
        /// <value>Event delegate.</value>
        public delegate void eventDelegateVoid();

        /// <value>Voids called when event happens.</value>
        public eventDelegateVoid eventDelegates;

        /// <value>Gets event name.</value>
        public string name { get; private set; }

        private static List<Event> events = new List<Event>();

        private Event(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Calls the event.
        /// </summary>
        public void call()
        {
            eventDelegates();
        }

        /// <summary>
        /// Finds event by name.
        /// </summary>
        /// <param name="name">event name</param>
        public static Event Get(string name)
        {
            foreach (Event ev in events)
            {
                if (ev.name.Equals(name)) return ev;
            }
            throw new EventException("There is no event \"" + name + "\"in event list");
        }

        /// <summary>
        /// Sets up new custom event.
        /// </summary>
        /// <param name="name">event name</param>
        public static void Setup(string name)
        {
            try
            {
                Get(name);
            }
            catch (EventException)
            {
                events.Add(new Event(name));
                return;
            }
            throw new EventException("Event name \"" + name + "\" is already in use");
        }

        /// <summary>
        /// Sets up new custom event.
        /// </summary>
        /// <param name="name">event name</param>
        /// <param name="delegates">Voids called when event happens.</param>
        public static void Setup(string name, eventDelegateVoid delegates)
        {
            try
            {
                Get(name);
            }
            catch (EventException)
            {
                Event e = new Event(name);
                e.eventDelegates = delegates;
                events.Add(e);
                return;
            }
            throw new EventException("Event name \"" + name + "\" is already in use");
        }
    }
}
