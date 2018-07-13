namespace Forgery.Anvil
{
    /// <summary>
    ///     Mod Interface. Holds basic functions for a mod
    /// </summary>
    public interface IModBase
    {
        /// <summary>
        ///     Fired before load.
        ///     This should be fired before any game instantiation
        /// </summary>
        void PreInit();

        /// <summary>
        ///     Fired during load.
        ///     This should be fired during game loading.
        /// </summary>
        void Load();

        /// <summary>
        ///     Fired after load.
        ///     This should be fired after everything is loaded.
        /// </summary>
        void PostInit();
    }
}
