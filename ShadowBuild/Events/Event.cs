using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new EventNotFoundException("Cannot find event named " + name + " inside event list");
        }
        public static void Setup(string name)
        {
            try
            {
                Get(name);
            }
            catch (EventNotFoundException)
            {
                events.Add(new Event(name));
                return;
            }
            throw new EventNameIsAlreadyUsedException("Event name \"" + name + "\" is already used by another event");
        }
        public static void Setup(string name, eventDelegateVoid delegates)
        {
            try
            {
                Get(name);
            }
            catch (EventNotFoundException)
            {
                Event e = new Event(name);
                e.eventDelegates = delegates;
                events.Add(e);
                return;
            }
            throw new EventNameIsAlreadyUsedException("Event name \"" + name + "\" is already used by another event");
        }
    }
}
