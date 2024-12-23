using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Character _soldier ;
        [SerializeField] private Character _deer;

        [SerializeField] private Character _selectedCharacter;

        private void Start()
        {
            _selectedCharacter = _soldier;
        }

        private void Update()
        {
            MoveCharacter();

            if (Input.GetKeyDown(KeyCode.Q)) {
                SwitchCharacter();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _selectedCharacter.Interact();
            }
        }

        private void SwitchCharacter() {
            if (_selectedCharacter == _soldier) {
                _selectedCharacter = _deer;
            }
            else if (_selectedCharacter == _deer) {
                _selectedCharacter = _soldier;
            }
        }

        private void MoveCharacter()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                _selectedCharacter.MoveTo(Vector2Int.up);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                _selectedCharacter.MoveTo(Vector2Int.down);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _selectedCharacter.MoveTo(Vector2Int.left);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                _selectedCharacter.MoveTo(Vector2Int.right);
        }

        // private void CheckClick()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        //         if (Physics.Raycast(ray, out RaycastHit hit))
        //         {
        //             if (hit.collider.TryGetComponent<MapTile>(out MapTile mapTile))
        //             {
        //                 if(mapTile.Unit)
        //                     ProcessUnit(mapTile.Unit);
        //                 else if(!mapTile.Unit && _selectedCharacter && !_selectedCharacter.isActioned)
        //                     _selectedCharacter.MoveOn(mapTile);
        //             }
        //         }
        //     }
        // }
        //
        // private void ProcessUnit(Unit unit)
        // {
        //     switch (unit)
        //     {
        //         case House:
        //             break;
        //         case Character character:
        //         {
        //             if (!character.isActioned)
        //             {
        //                 _selectedCharacter = character;
        //             }
        //             break;
        //         }
        //     }
        // }
    }
}