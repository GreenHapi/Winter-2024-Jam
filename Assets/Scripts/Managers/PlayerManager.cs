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
        
        private void Update()
        {
            CheckClick();
        }

        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent<MapTile>(out MapTile mapTile))
                    {
                        
                        if(mapTile.Unit)
                            ProcessUnit(mapTile.Unit);
                        else if(!mapTile.Unit && _selectedCharacter && !_selectedCharacter.isActioned)
                            _selectedCharacter.MoveOn(mapTile);
                    }
                }
            }
        }

        private void ProcessUnit(Unit unit)
        {
            switch (unit)
            {
                case House:
                    break;
                case Character character:
                {
                    if (!character.isActioned)
                    {
                        _selectedCharacter = character;
                    }
                    break;
                }
            }
        }
    }
}