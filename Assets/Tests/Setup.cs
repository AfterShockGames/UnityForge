using UnityEngine;
using NUnit.Framework;
using Tests.Data;

namespace Tests
{
    [SetUpFixture]
    public class Setup
    {
        /// <summary>
        ///     Create and Setup the Forge objects
        /// </summary>
        [OneTimeSetUp]
        public void SetupForge()
        {
            GameObject ForgeInit = new GameObject("ForgeInit");
            ForgeInit.AddComponent<TestInitalize>();
        }
    }
}
