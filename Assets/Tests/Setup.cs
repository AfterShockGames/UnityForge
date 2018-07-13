using UnityEngine;
using NUnit.Framework;
using Tests.Data;

namespace Tests
{
    [SetUpFixture]
    public class Setup
    {
        /// <summary>
        ///     Create and Setup the Forgery objects
        /// </summary>
        [OneTimeSetUp]
        public void SetupForgery()
        {
            GameObject forgeryInit = new GameObject("ForgeryInit");
            forgeryInit.AddComponent<TestInitalize>();

            Debug.Log(forgeryInit);
        }
    }
}
