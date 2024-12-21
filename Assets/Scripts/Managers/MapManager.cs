using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterJam.Managers
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }
        [field:SerializeField] public Transform MapTilesTransform { get; private set; }

        private void OnValidate()
        {
            Instance = this;
        }

    }
}