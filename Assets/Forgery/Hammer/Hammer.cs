using System.Collections;
using System.Collections.Generic;
using Forgery.Errors;
using Forgery.Hammer.Events;
using Forgery.Settings;
using Forgery.Utils;
using UnityEngine;
using Event = Forgery.Hammer.Events.Event;

namespace Forgery.Hammer
{ 
    /// <summary>
    ///     Forgery Hammer class.
    ///     Hammer is used for Event creating and managing
    /// </summary>
    public class Hammer : MonoBehaviour
    {
        /// <summary>
        ///     Gets or created the Hammer gameObject
        /// </summary>
        public static Hammer GetHammer
        {
            get
            {
                return _hammer ?? InstantiateHammer;
            }
        }

        /// <summary>
        ///     Creates the hammer gameObject
        /// </summary>
        public static Hammer InstantiateHammer
        {
            get
            {
                GameObject go = new GameObject(InternalData.HAMMER);
                _hammer = go.AddComponent<Hammer>();

                return _hammer;
            }
        }

        /// <summary>
        ///     A Dictionary which contains all events.
        ///     The key is a unique identifier.
        /// </summary>
        private readonly Dictionary<string, EventList<Event>> _eventList = new Dictionary<string, EventList<Event>>();

        private static Hammer _hammer;

        /// <summary>
        ///     Registers an event and adds it to the list.
        /// </summary>
        /// <param name="eventIdentifier">The event identifier</param>
        /// <param name="hammerEvent">The event object</param>
        public void RegisterEvent(string eventIdentifier, Event hammerEvent)
        {
            if (!_eventList.ContainsKey(eventIdentifier))
            {
                _eventList.Add(eventIdentifier, new EventList<Event>());
            }

            _eventList[eventIdentifier].Add(hammerEvent);
        }

        /// <summary>
        ///     Registers an event for x seconds.
        ///     Usefull for time based powerups.
        /// </summary>
        /// <param name="eventIdentifier">The event identifier</param>
        /// <param name="hammerEvent">The event object</param>
        /// <param name="time">The amount of seconds to register this event for</param>
        public void RegisterEventForSeconds(string eventIdentifier, Event hammerEvent, float time)
        {
            RegisterEvent(eventIdentifier, hammerEvent);

            StartCoroutine(DestroyAfterSeconds(time, hammerEvent, eventIdentifier));
        }

        /// <summary>
        ///     Destroys an event
        /// </summary>
        /// <typeparam name="T">The event object Type</typeparam>
        /// <param name="eventIdentifier">The event identifier</param>
        /// <param name="eEvent">The event to remove</param>
        public void DestroyEvent<T>(string eventIdentifier, Event<T> eEvent)
        {
            if (_eventList.ContainsKey(eventIdentifier))
            {
                _eventList[eventIdentifier].Remove(eEvent);
            }
        }

        /// <summary>
        ///     Fires all events on the eventIdentifier.
        ///     This does not allow any forgeryEventArgs to be sent.
        /// </summary>
        /// <param name="eventIdentifier">The event identifier</param>
        /// <param name="sender">The event sender object. Should be same as the target.</param>
        public void DispatchEvents(string eventIdentifier, object sender)
        {
            DispatchEvents<object>(eventIdentifier, sender, null);
        }

        /// <summary>
        ///     Fires all events on the eventIdentifier
        /// </summary>
        /// <typeparam name="T">The forgeryEventArgs type and eventType</typeparam>
        /// <param name="eventIdentifier">The eventIdentifier</param>
        /// <param name="sender">The sender object</param>
        /// <param name="args">
        ///     The forgeryEventArgs. 
        ///     These will be passed to the eventHandler and used during execution.
        /// </param>
        /// <returns>
        ///     The event results.
        ///     <example>
        ///         For example if you register an event to calculate movement speed and fire all movement speed event those events can increase or decrease the final movement speed number.
        ///         Below is a code example of creating and registering an event for movementSpeed
        ///         <code>
        ///             //We first create a property which uses the Hammer event System to calculate it's value.
        ///             public float MoveSpeed
        ///             {
        ///                 get
        ///                 {
        ///                     return Hammer.DispatchEvents<float>("MOVEMENTSPEED", object, new HammerEventArgs<float>(moveSpeed));
        ///                 }
        ///             }
        /// 
        ///             private moveSpeed = 5;
        /// 
        ///             //Now we register an event to add 5 movement speed for 5 seconds.
        ///             Event<float> speedEvent = new Event<float>("EVENT_IDENTIFIER", object); //Identifier is used to remove the speed Event. The object should be the same as the object used in the DispactEvents function.
        ///             speedEvent.ForgeryEvent += (obj, args) => args.EventItem + 5; //We create the handler function which adds 5 to the ForgeryEventArguments.
        /// 
        ///             AftershockState.Hammer.RegisterEventForSeconds("MOVEMENTSPEED", speedEvent, 5); //Registers the newly created speed event for 5 seconds
        ///         </code>
        ///         After using this code the event results will result in having +5 added to it for 5 seconds.
        ///     </example>
        /// </returns>
        public T DispatchEvents<T>(string eventIdentifier, object sender, HammerEventArgs<T> args)
        {
            foreach (T result in DispatchEvent(eventIdentifier, sender, args))
            {
                args.EventItem = result;
            }

            return args.EventItem;
        }
        
        /// <summary>
        ///     Enumarable for dispatching all events on a eventIdentifier
        /// </summary>
        /// <typeparam name="T">The ForgeryEventArgument Type</typeparam>
        /// <param name="eventIdentifier">The eventIdentifier</param>
        /// <param name="sender">The event sender object</param>
        /// <param name="args">The ForgeryEventArguments</param>
        /// <returns>
        ///     <see cref="DispatchEvents"/>
        /// </returns>
        public IEnumerable DispatchEvent<T>(string eventIdentifier, object sender, HammerEventArgs<T> args)
        {
            if (!_eventList.ContainsKey(eventIdentifier))
            {
                yield break;
            }

            foreach (Event @event in _eventList[eventIdentifier])
            {
                if (@event.GetType() != typeof(Event<T>))
                {
                    throw new ForgeryCritical(Language.ForgeryCriticalInvalidEventTypeInList + eventIdentifier);
                }

                Event<T> iEvent = (Event<T>) @event;

                if (iEvent.EventType == args.ArgumentType)
                {
                    yield return iEvent.Execute(sender, args);
                }
                else
                {
                    throw new ForgeryCritical(Language.ForgeryCriticalArgumentTypeDoesNotEqualEventType);
                }
            }
        }

        /// <summary>
        ///     Destroys the event after x seconds
        /// </summary>
        /// <param name="time">The seconds to wait for</param>
        /// <param name="hammerEvent">The event to destroy</param>
        /// <param name="eventIdentifier">The event Identifier</param>
        /// <returns>WaitForSeconds</returns>
        private IEnumerator DestroyAfterSeconds(float time, Event hammerEvent, string eventIdentifier)
        {
            yield return new WaitForSeconds(time);

            _eventList[eventIdentifier].Remove(hammerEvent);
        }

        /// <summary>
        ///     TODO::Create function
        /// </summary>
        private void RegisterAllEvents() { }

        /// <summary>
        ///     TODO::Create function
        /// </summary>
        private void DestroyAllEvents() { }
    }
}