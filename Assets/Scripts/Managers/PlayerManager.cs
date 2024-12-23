using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Character _soldier ;
        [SerializeField] private Character _deer;

        public Character SelectedCharacter { get; private set; }

         private void Start()
        {
            SelectedCharacter = _soldier;
        }

        private void Update()
        {
            MoveCharacter();

            if (Input.GetKeyDown(KeyCode.Q)) {
                SwitchCharacter();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SelectedCharacter.Interact();
            }
        }

        private void SwitchCharacter() {
            if (SelectedCharacter == _soldier) {
                SelectedCharacter = _deer;
            }
            else if (SelectedCharacter == _deer) {
                SelectedCharacter = _soldier;
            }
        }

        private void MoveCharacter()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                MoveUp();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                MoveDown();
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                MoveRight();
            }
        }

        public void MoveRight() {
            Debug.Log("MoveRight");
            SelectedCharacter.MoveTo(Vector2Int.right);
        }

        public void MoveLeft() {
            Debug.Log("MoveLeft");
            SelectedCharacter.MoveTo(Vector2Int.left);
        }

        public void MoveDown() {
            Debug.Log("MoveDown");
            SelectedCharacter.MoveTo(Vector2Int.down);
        }

        public void MoveUp() {
            Debug.Log("MoveUp");
            SelectedCharacter.MoveTo(Vector2Int.up);
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