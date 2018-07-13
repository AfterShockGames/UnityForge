using System;

namespace Forgery.Anvil
{
    /// <summary>
    ///     Base MOD attribute, using this attribute will autoload your class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Mod : Attribute
    {
        /// <summary>
        ///     The public modId this mod is referenced by
        /// </summary>
        public string ModId;

        /// <summary>
        ///     Mod constructor.
        /// </summary>
        /// <param name="modId">Your mod ID</param>
        public Mod(string modId)
        {
            ModId = modId;
        }
    }
}