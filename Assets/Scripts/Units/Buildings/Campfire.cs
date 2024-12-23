using WinterJam.Units.Characters;

namespace WinterJam.Units.Buildings {
public class Campfire : Unit, IInteractable {
    public bool TryInteract(Character character) {
        if (character is Soldier soldier) {
            soldier.ChangeTorchState(true);
            return true;
        }

        return false;
    }

    private void Update() { }
}
}