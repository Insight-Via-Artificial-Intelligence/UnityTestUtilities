// Copyright (c) 2023 Insight Via Artificial Intelligence
// This file is licensed under the MIT License.
// License text available at https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities/blob/main/LICENSE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IVAI.TestableSample.Runtime
{
    public class SampleUnit : MonoBehaviour
    {
        [SerializeField]
        private int changeMeWithInspectorButton = 0;

        // How many seconds the unit has been enabled since start
        public int CountOfSecondsSinceActive { private set; get; } = 0;

        private float timer = 0f;

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            timer += Time.deltaTime;

            CountOfSecondsSinceActive = (int)timer;
        }

        // Any example has to have Hello World
        public string HelloWorld()
        {
            return "Hello World";
        }

        // A regular function to be called through the inspector
        public void SetChangeMe(int value)
        {
            changeMeWithInspectorButton = value;
        }
    }
}