using System;
using System.Collections.Generic;

namespace Forge.Hammer.Events
{
    /// <summary>
    ///     A list containing multiple events.
    /// </summary>
    /// <typeparam name="T">The event return type</typeparam>
    public class EventList<T> : List<T>
    {
        /// <summary>
        ///     OnAdd Event
        /// </summary>
        public event EventHandler OnAdd;
        /// <summary>
        ///     OnRemove Event
        /// </summary>
        public event EventHandler OnRemove;

        /// <summary>
        ///     Adds an item to the list and executes the OnAdd event.
        /// </summary>
        /// <param name="item">The item to add</param>
        public new void Add(T item)
        {
            if (OnAdd != null)
            {
                EventListArgs<T> args = new EventListArgs<T>(item);
                OnAdd(this, args);
            }

            base.Add(item);
        }

        /// <summary>
        ///     Removes an item from the list and fires the OnRemove event
        /// </summary>
        /// <param name="item">The item to remove</param>
        public new void Remove(T item)
        {
            if (OnRemove != null)
            {
                EventListArgs<T> args = new EventListArgs<T>(item);
                OnRemove(this, args);
            }

            base.Remove(item);
        }
    }
}