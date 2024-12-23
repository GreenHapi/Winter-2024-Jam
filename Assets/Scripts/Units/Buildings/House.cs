using NaughtyAttributes;
using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Units.Buildings {
public class House : Unit, IInteractable {
    [SerializeField] private GameObject _frozen;
    [SerializeField] private GameObject _melted;

    [SerializeField] private bool _isFrozen;

    private void Start() {
        if (_isFrozen) {
            Freeze();
        }
        else {
            Melt();
        }
    }

    public bool TryInteract(Character character) {
        if (character == null) {
            return false;
        }

        if (character is Soldier soldier) {
            if (soldier.IsTorchLit && _isFrozen) {
                Melt();
                return true;
            }
        }

        if (character is Snowman snowman) {
            if (!_isFrozen) {
                Freeze();
                return true;
            }
        }

        return false;
    }

    [Button]
    private void Melt() {
        _frozen.SetActive(false);
        _melted.SetActive(true);
        _isFrozen = false;
    }

    [Button]
    private void Freeze() {
        _frozen.SetActive(true);
        _melted.SetActive(false);
        _isFrozen = true;
    }
}
}