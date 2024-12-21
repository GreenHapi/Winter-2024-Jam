using UnityEngine;

namespace WinterJam.Units.Characters
{
    /// Unit that can be played by player/bot
    public abstract class Character : Unit
    {
        [field:SerializeField] public bool isActioned { get; private set; }
        [field:SerializeField] public int MaxMoveDistance { get; private set; }

        public void MoveOn(MapTile tile)
        {
            
            print((int)Vector2.Distance(tile.GridPosition, GridPosition));
            if ((int)Vector2.Distance(tile.GridPosition, new(transform.localPosition.x, transform.position.z)) <= MaxMoveDistance)
            {
                tile.Unit = this;
                transform.localPosition = tile.transform.position;
                isActioned = true;
            }
            else
            {
                print("Can't move there!");
            }
        }
    }
}