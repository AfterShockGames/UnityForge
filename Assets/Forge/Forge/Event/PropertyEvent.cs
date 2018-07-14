using System;

namespace Forge.Forge.Event
{
    /// <summary>
    ///     PropertyEvent attribute used by the TypeFactory to replace properties with a hammer dispatch event function
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyEvent : Attribute
    {
        /// <summary>
        ///     The Hammer event identifier
        /// </summary>
        public string EventIdentifier { get; private set; }
        /// <summary>
        ///     The event object which is passed to the event arguments
        /// </summary>
        public object EventObject { get; private set; }
        /// <summary>
        ///     The event default return value
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        ///     Set all necessary data for creating the dispatch event function 
        /// </summary>
        /// <param name="eventIdentifier">The Hammer event identifier</param>
        /// <param name="defaultValue">The default event value</param>
        /// <param name="eventObject">The event object which is passed to the event arguments</param>
        public PropertyEvent(string eventIdentifier, object eventObject, object defaultValue = null)
        {
            EventIdentifier = eventIdentifier;
            EventObject = eventObject;
            DefaultValue = defaultValue;
        }
    }
}
