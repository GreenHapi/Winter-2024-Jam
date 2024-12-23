using System;
using PrimeTween;
using UnityEngine;
using WinterJam.Units.Buildings;
using WinterJam.Units.Characters;

namespace WinterJam.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Character _selectedCharacter;

        private void Awake()
        {
            _selectedCharacter = Soldier.Instance;
        }

        private void Update()
        {
            MoveCharacter();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_selectedCharacter == Soldier.Instance) 
                    _selectedCharacter = Deer.Instance;
                else if(_selectedCharacter == Deer.Instance)
                    _selectedCharacter = Soldier.Instance;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                _selectedCharacter.Interact();
            }
        }
        
        private void MoveCharacter()
        {
            if (Input.GetKeyDown(KeyCode.W)) 
                _selectedCharacter.MoveTo(Vector2Int.up);
            if (Input.GetKeyDown(KeyCode.S)) 
                _selectedCharacter.MoveTo(Vector2Int.down);
            if (Input.GetKeyDown(KeyCode.A)) 
                _selectedCharacter.MoveTo(Vector2Int.left);
            if (Input.GetKeyDown(KeyCode.D)) 
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