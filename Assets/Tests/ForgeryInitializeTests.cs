using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using Tests.Data;
using Forgery.Settings;
using Forgery.Anvil.Registry;

namespace Tests {

    /// <summary>
    ///     Tests all forgery startup functions
    /// </summary>
    public class ForgeryInitializeTest
    {
        [UnityTest]
        public IEnumerator ForgeryStartupTest()
        {
            yield return null;
            Assert.NotNull(GameObject.Find(InternalData.ANVIL_REGISTER)); // Checks if the Anvil object is created.
            Assert.NotNull(GameObject.Find(InternalData.HAMMER)); // Checks if the Hammer object is created.
            Assert.NotNull(GameObject.Find(InternalData.ANVIL_REGISTRY)); // Checks if the anvil registry is created.
        }

        [UnityTest]
        public IEnumerator ForgeryCreateRegistryTest()
        {
            //Register.GetRegister.CreateRegistry<>();
            yield return null;
        }
    }
}