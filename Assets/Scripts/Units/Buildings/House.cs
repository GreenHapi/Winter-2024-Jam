using NaughtyAttributes;
using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Units.Buildings
{
    public class House : Unit, IInteractable
    {
        [SerializeField] private GameObject _frozen;
        [SerializeField] private GameObject _melted;

        [SerializeField] private bool _isFrozen;
        [SerializeField] private AudioSource _meltAudioSource;
        [SerializeField] private AudioSource _freezeAudioSource;

        private void Start()
        {
            if (_isFrozen)
            {
                Freeze(false);
            }
            else
            {
                Melt(false);
            }
        }

        public bool TryInteract(Character character)
        {
            if (character == null)
            {
                return false;
            }

            if (character is Soldier soldier)
            {
                if (soldier.IsTorchLit && _isFrozen)
                {
                    Melt();
                    return true;
                }
            }

            if (character is Snowman snowman)
            {
                if (!_isFrozen)
                {
                    Freeze();
                    return true;
                }
            }

            return false;
        }

        [Button]
        private void Melt(bool playSound = true)
        {
            _frozen.SetActive(false);
            _melted.SetActive(true);
            if (playSound)
            {
                _meltAudioSource.Play();
            }

            _isFrozen = false;
        }

        [Button]
        private void Freeze(bool playSound = true)
        {
            _frozen.SetActive(true);
            _melted.SetActive(false);
            if (playSound)
            {
                _freezeAudioSource.Play();
            }

            _isFrozen = true;
        }
    }
}