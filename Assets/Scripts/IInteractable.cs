using WinterJam.Units.Characters;

namespace WinterJam
{
    /// Is unit can be interacted with other units?
    public interface IInteractable
    {
        public void TryInteract(Character character);
    }
}