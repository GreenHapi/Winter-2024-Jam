using UnityEngine;

namespace WinterJam.Units.Characters {
public class Soldier : Character {
    [field: SerializeField] public bool IsTorchLit { get; private set; }
    [SerializeField] private GameObject _torchFire;

    private void Awake() {
        _torchFire.SetActive(false);
    }

    public void ChangeTorchState(bool lighten) {
        IsTorchLit = lighten;

        _torchFire.SetActive(lighten);
    }
}
}