using System;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Managers
{
    public class TurnsManager : MonoBehaviour
    {
        public TurnsManager Instance { get; private set; }
        private void OnValidate() => Instance = this;

        public event Action TurnChanged;
        public bool IsPlayerTurn { get; private set;  } = true;

        [Button]
        public void NextTurn()
        {
            IsPlayerTurn = !IsPlayerTurn;
            if (IsPlayerTurn)
            {
                foreach (var character in GameObject.FindObjectsByType<Character>(FindObjectsSortMode.None)
                             .Where((c) => c is Soldier or Deer))
                {
                    character.ToggleActioned(false);
                }
            }
            else
            {
                foreach (var character in GameObject.FindObjectsByType<Character>(FindObjectsSortMode.None)
                             .Where((c) => c is Snowman))
                {
                    character.ToggleActioned(false);
                }
            }
            
            TurnChanged?.Invoke();
        }
    }
}
