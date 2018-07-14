using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using Tests.Data;
using Forge.Settings;
using Forge.Anvil.Registry;

namespace Tests {

    /// <summary>
    ///     Tests all Forge startup functions
    /// </summary>
    public class ForgeInitializeTest
    {
        [UnityTest]
        public IEnumerator ForgeStartupTest()
        {
            yield return null;
            Assert.NotNull(GameObject.Find(InternalData.ANVIL_REGISTER)); // Checks if the Anvil object is created.
            Assert.NotNull(GameObject.Find(InternalData.HAMMER)); // Checks if the Hammer object is created.
            Assert.NotNull(GameObject.Find(InternalData.ANVIL_REGISTRY)); // Checks if the anvil registry is created.
        }

        [UnityTest]
        public IEnumerator ForgeCreateRegistryTest()
        {
            Register.GetRegister.CreateRegistry<ITestObject>(TestSettings.TEST_REGISTRY);
            yield return null;

            IRegistry testRegistry = Register.GetRegister.GetRegistry(TestSettings.TEST_REGISTRY);

            Assert.NotNull(testRegistry);
            
        }
    }
}