// Copyright (c) 2023 Insight Via Artificial Intelligence
// This file is licensed under the MIT License.
// License text available at https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities/blob/main/LICENSE

using System.Collections;
using System.Collections.Generic;

using IVAI.EditorUtilities.Testing;
using IVAI.TestableSample.Runtime;

using NUnit.Framework;

using UnityEngine;

namespace IVAI.TestableSample.Tests
{

    public class GetAssetFromOtherFolder
    {
        // A list of all objects created during testing.
        private List<GameObject> testObjects = new List<GameObject>();

        private SampleUnit sampleUnit = null;

        private string prefabFolder = string.Empty;

        [SetUp]
        public void SetUp()
        {
            //Store old version of prefab name
            prefabFolder = TestAssetLoader.PrefabFolder;

            // Change prefab folder
            TestAssetLoader.PrefabFolder = "DifferentPrefabFolder";
            
            // The variant is in a different folder structure
            sampleUnit = TestAssetLoader.CreatePrefabThatHasScript<SampleUnit>("SampleUnit Variant", "", testObjects);
        }

        [TearDown]
        public void TearDown()
        {
            // Revert name for the next test
            TestAssetLoader.PrefabFolder = prefabFolder;

            // Remove reference to unit
            sampleUnit = null;

            // Destroys all created objects
            TestAssetLoader.CleanUpObjects(testObjects);
        }

        [Test]
        public void CreatedUnitFromOtherFolder()
        {
            // This is a trivial test for the unit,
            // but it shows you can get a prefab for a different folder
            Assert.IsNotNull(sampleUnit);
        }
    }
}