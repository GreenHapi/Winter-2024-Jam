using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using WinterJam.Units.Characters;

namespace WinterJam.Managers
{
    public class TurnsManager : MonoBehaviour
    {
        public static TurnsManager Instance { get; private set; }
        private void Awake() => Instance = this;

        public event Action TurnChanged;

        private int _enemiesCount;
        private IEnumerable<Character> _enemies;
        [field: SerializeField] public bool IsPlayerTurn { get; private set; } = true;

        private void Start()
        {
            IEnumerable<Character> characters = GetAllSnowmen();
            _enemiesCount = characters.Count();
        }

        [Button]
        public void NextTurn()
        {
            if (IsPlayerTurn)
            {
                Debug.Log("Player Turn");
                foreach (var character in FindObjectsByType<Character>(FindObjectsSortMode.None)
                             .Where(c => c is Soldier or Deer))
                {
                    character.ResetMoves();
                }

                IsPlayerTurn = false;
                _enemies = GetAllSnowmen().ToList();
                _enemiesCount = _enemies.Count();
            }
            else
            {
                Debug.Log("Enemy Turn");
                foreach (var character in _enemies)
                {
                    character.ResetMoves();
                }

                IsPlayerTurn = true;
            }

            TurnChanged?.Invoke();
        }

        private static IEnumerable<Character> GetAllSnowmen()
        {
            IEnumerable<Character> characters = FindObjectsByType<Character>(FindObjectsSortMode.None)
                .Where(c => c is Snowman);
            return characters;
        }

        public void EnemyFinishedItsMovement()
        {
            _enemiesCount--;
            if (_enemiesCount == 0)
            {
                NextTurn();
                IsPlayerTurn = true;
            }
        }
    }
}