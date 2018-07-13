using System;

namespace Forgery.Hammer.Events
{
    /// <summary>
    ///     Forgery event arguments which hold any relevant event info
    /// </summary>
    /// <typeparam name="T">The argument type</typeparam>
    public class HammerEventArgs<T> : EventArgs
    {
        public Type ArgumentType = typeof(T);

        public T EventItem { get; set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="item">The argument / item to pass</param>
        public HammerEventArgs(T item)
        {
            EventItem = item;
        }
    }
}
