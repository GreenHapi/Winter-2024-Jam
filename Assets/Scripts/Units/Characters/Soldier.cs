using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace WinterJam.Units.Characters
{
    public class Soldier : Character
    {
        [SerializeField] private bool _isTorchLighten;
        [SerializeField] private GameObject _torchFire;

        public static Soldier Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ChangeTorchState(bool lighten)
        {
            _isTorchLighten = lighten;
        }
    }
}