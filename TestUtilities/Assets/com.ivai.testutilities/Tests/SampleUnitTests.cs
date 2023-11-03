// Copyright (c) 2023 Insight Via Artificial Intelligence
// This file is licensed under the MIT License.
// License text available at https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities/blob/main/LICENSE

using IVAI.EditorUtilities.Testing;
using IVAI.TestableSample.Runtime;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace IVAI.TestableSample.Tests
{
    public class SampleUnitTests
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
            // Reset time scale
            Time.timeScale = 1.0f;

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

        [Test]
        public void CreatePrefab()
        {
            SampleUnit secondUnit = TestAssetLoader.CreatePrefab<SampleUnit>("SampleUnit", "", testObjects);

            Assert.IsNotNull(secondUnit);
        }

        // Unity test marked methods allows us to write tests like unity coroutines:
        // https://docs.unity3d.com/Manual/Coroutines.html
        // Useful if what we want to test happens over time
        [UnityTest]
        public IEnumerator TestRealtimeWait()
        {
            // Increase time scale to speed up test
            Time.timeScale = 10f;

            // Since our time scale is 10, 2 seconds becomes 0.2 seconds
            // This is import as a 100 tests which takes 2 seconds is over 3 minutes
            // We can use wait realtime if we want to ignore timescale
            yield return new WaitForSeconds(2f);

            // The count of seconds just converts waited time to int
            Assert.AreEqual(2, sampleUnit.CountOfSecondsSinceActive);
        }

        // Use test function to check whether two floats are close enough to be considered equal
        [Test]
        public void MostlyEqual()
        {
            float expected = 10f;

            sampleUnit.transform.position = Vector3.one * expected;

            Assert.IsTrue(TestAssetLoader.MostlyEqual(expected, sampleUnit.transform.position.z, 0.1f));
        }

        [Test]
        public void MostlyNotEqual()
        {
            float firstNumber = 10f;
            float secondNumber = 10.1f;

            Assert.IsFalse(TestAssetLoader.MostlyEqual(firstNumber, secondNumber));
        }

        // Use test function to check whether two floats are close enough to be considered equal
        [Test]
        public void MostlyEqualVector3()
        {
            Vector3 expected = Vector3.one * 10f;

            Vector3 mostlyEqual = Vector3.one * 10.01f;

            Assert.IsTrue(TestAssetLoader.MostlyEqual(expected, mostlyEqual, 0.1f));
        }

        [Test]
        public void MostlyNotEqualVector3()
        {
            Vector3 expected = Vector3.one * 10f;

            Vector3 mostlyEqual = Vector3.one * 10.1f;

            Assert.IsFalse(TestAssetLoader.MostlyEqual(expected, mostlyEqual, 0.1f));
        }
    }
}