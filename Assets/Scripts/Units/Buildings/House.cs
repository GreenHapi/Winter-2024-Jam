using NaughtyAttributes;
using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Units.Buildings {
public class House : Unit, IInteractable {
    [SerializeField] private GameObject _frozen;
    [SerializeField] private GameObject _melted;

    public void TryInteract(Character character) { }

    [Button]
    private void Melt() {
        _frozen.SetActive(false);
        _melted.SetActive(true);
    }

    [Button]
    private void Freeze() {
        _frozen.SetActive(true);
        _melted.SetActive(false);
    }
}
}