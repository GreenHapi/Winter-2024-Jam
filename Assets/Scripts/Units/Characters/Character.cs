using UnityEngine;
using WinterJam.Managers;

namespace WinterJam.Units.Characters
{
    /// Unit that can be played by player/bot
    public abstract class Character : Unit
    {
        [field:SerializeField] public bool isActioned { get; private set; }
        [field:SerializeField] public int MaxMoveDistance { get; private set; }
        
        [field:SerializeField] private IInteractable foundInteractable;

        public void MoveOn(MapTile tile)
        {
            if ((int)Vector2.Distance(tile.GridPosition, new(transform.localPosition.x, transform.position.z)) <= MaxMoveDistance)
            {
                tile.Unit = this;
                GridPosition = tile.GridPosition;
                transform.localPosition = tile.transform.position;
                isActioned = true;
                FindForInteractsNearby();
            }
            else
            {
                print("Can't move there!");
            }
        }

        public void Interact()
        {
            if(isActioned) return;
            
            foundInteractable.TryInteract(this);
        }
        
        public void ToggleActioned(bool actioned)
        {
            isActioned = actioned;
        }

        public IInteractable FindForInteractsNearby()
        {
            if (this is not Soldier) return null;
            
            var map = MapManager.Instance.MapTilesMatrix;

            if (map[GridPosition.x + 1, GridPosition.y + 1].Unit &&  map[GridPosition.x + 1, GridPosition.y + 1].Unit is IInteractable interactable1)
                foundInteractable = interactable1;
            else if (map[GridPosition.x + 1, GridPosition.y - 1].Unit && map[GridPosition.x + 1, GridPosition.y - 1].Unit is IInteractable interactable2)
                foundInteractable = interactable2;
            else if (map[GridPosition.x - 1, GridPosition.y + 1].Unit&& map[GridPosition.x - 1, GridPosition.y + 1].Unit is IInteractable interactable3)
                foundInteractable = interactable3;
            else if (map[GridPosition.x - 1, GridPosition.y - 1].Unit&& map[GridPosition.x - 1, GridPosition.y - 1].Unit is IInteractable interactable4)
                foundInteractable = interactable4;
            else if (map[GridPosition.x, GridPosition.y + 1].Unit && map[GridPosition.x, GridPosition.y + 1].Unit is IInteractable interactable5)
                foundInteractable = interactable5;
            else if (map[GridPosition.x, GridPosition.y - 1].Unit && map[GridPosition.x, GridPosition.y - 1].Unit is IInteractable interactable6)
                foundInteractable = interactable6;
            else if (map[GridPosition.x + 1, GridPosition.y].Unit && map[GridPosition.x + 1, GridPosition.y].Unit is IInteractable interactable7)
                foundInteractable = interactable7;
            else if (map[GridPosition.x - 1, GridPosition.y].Unit && map[GridPosition.x - 1, GridPosition.y].Unit is IInteractable interactable8)
                foundInteractable = interactable8;

            return foundInteractable;
        }
    }
}