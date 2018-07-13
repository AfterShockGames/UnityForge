using System;
using Forgery.Errors;
using Forgery.Settings;

namespace Forgery.Hammer.Events
{
    /// <summary>
    ///     Base class used for type casting
    /// </summary>
    public abstract class Event { }

    /// <summary>
    ///     Hammer event class used for creating events
    /// </summary>
    /// <typeparam name="T">
    ///     Event Type
    /// </typeparam>
    public class Event<T> : Event
    {
        /// <summary>
        ///     Event delegate
        /// </summary>
        /// <param name="sender">The event sender or target</param>
        /// <param name="args">The event arguments based on a HammerEventArgs class</param>
        /// <returns>The event type or the result of the ForgeryEvent handler</returns>
        public delegate T ForgeryEventHandler(object sender, HammerEventArgs<T> args = null);

        /// <summary>
        ///     Event handler
        /// </summary>
        public event ForgeryEventHandler ForgeryEvent;

        /// <summary>
        ///     Reference to the event type
        /// </summary>
        public Type EventType = typeof(T);

        /// <summary>
        ///     Event unique identifier
        /// </summary>
        public string Name;

        /// <summary>
        ///     The event target
        /// </summary>
        private readonly object _target;

        /// <summary>
        ///     Constructing an event without target allows any type of target to be used.
        /// </summary>
        /// <param name="name">The event identifier</param>
        public Event(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Constructing an event with a target makes sure the event will only execute when its sender is the same as its target
        /// </summary>
        /// <param name="name">The event identifier</param>
        /// <param name="target">The event target</param>
        public Event(string name, object target)
        {
            Name = name;
            _target = target;
        }


        /// <summary>
        ///     Executes the event
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="args">The forgery event arguments</param>
        /// <returns>Forgery event results</returns>
        public T Execute(object sender, HammerEventArgs<T> args)
        {
            if (ForgeryEvent == null)
            {
                throw new ForgeryCritical(Language.ForgeryCriticalNoEventMethod + Name);
            }

            if (_target != null && _target != sender)
            {
                return default(T);
            }

            return ForgeryEvent(sender, args);
        }
    }
}
