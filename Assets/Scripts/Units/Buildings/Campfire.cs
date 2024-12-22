using System;
using WinterJam.Units.Characters;

namespace WinterJam.Units.Buildings
{
    public class Campfire : Unit, IInteractable
    {
        public void TryInteract(Character character)
        {
            if (character is Soldier soldier)
            {
                soldier.ChangeTorchState(true);
            }
        }

        private void Update()
        {
        }
    }
}