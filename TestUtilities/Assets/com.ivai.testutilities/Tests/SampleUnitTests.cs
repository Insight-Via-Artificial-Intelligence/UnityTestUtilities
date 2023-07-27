using IVAI.TestableSample.Runtime;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IVAI.TestableSample.Tests
{
    public class SampleUnitTests : MonoBehaviour
    {
        // A list of all objects created during testing.
        private List<GameObject> testObjects = new List<GameObject>();

        // Will be created as a default sample unit for testsing
        private SampleUnit sampleUnit = null;

        [SetUp]
        public void SetUp()
        {
            // Create the sample unit and add it to the list of test objects
            sampleUnit = TestAssetLoader.CreateComponentOfType<SampleUnit>(testObjects, "sampleUnit");
        }

        [TearDown]
        public void TearDown()
        {
            // Remove reference to unit
            sampleUnit = null;

            // Destroys all created objects
            TestAssetLoader.CleanUpObjects(testObjects);
        }

        [Test]
        public void HelloWorldTest()
        {
            string helloWorld = sampleUnit.HelloWorld();

            Assert.AreEqual("Hello World", helloWorld);
        }

    }
}