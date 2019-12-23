using ShadowBuild.Exceptions;
using System.Collections.Generic;

namespace ShadowBuild.Events
{
    public sealed class Event
    {
        public delegate void eventDelegateVoid();
        public eventDelegateVoid eventDelegates;
        public string name { get; private set; }

        private static List<Event> events = new List<Event>();

        public Event(string name)
        {
            this.name = name;
        }
        public void call()
        {
            eventDelegates();
        }

        public static Event Get(string name)
        {
            foreach (Event ev in events)
            {
                if (ev.name.Equals(name)) return ev;
            }
            throw new EventException("There is no event \""+name+"\"in event list");
        }
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
