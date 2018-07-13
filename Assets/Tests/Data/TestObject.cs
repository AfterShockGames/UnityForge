using UnityEngine;

namespace Tests.Data
{
    public class TestObject : MonoBehaviour, ITestObject
    {
        public bool TestFunc()
        {
            return true;
        }
    }
}
