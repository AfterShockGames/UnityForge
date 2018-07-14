using System;

namespace Forge.Hammer.Events
{
    /// <summary>
    ///     Args for within an eventList
    /// </summary>
    /// <typeparam name="T">The event args type</typeparam>
    public class EventListArgs<T> : EventArgs
    {
        /// <summary>
        ///     The item / result from this event
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        ///     Event list contructor
        /// </summary>
        /// <param name="item">The item to be passed from the event</param>
        public EventListArgs(T item)
        {
            Item = item;
        }
    }
}