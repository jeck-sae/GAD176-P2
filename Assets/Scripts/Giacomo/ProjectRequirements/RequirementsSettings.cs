using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{

    [CreateAssetMenu(menuName = "Requirements/Settings"), Serializable]
    public class RequirementsSettings : ScriptableObject
    {
        public Vector2 force;
        public Vector2 area;
    }
}