using System;
using System.Threading.Tasks;
using UnityEngine;
using WinterJam.Managers;
using WinterJam.Units.Buildings;

namespace WinterJam.Units.Characters
{
    public class Snowman : Character
    {
        [SerializeField] private House _targetHouse;

        private void Awake()
        {
            foreach (var house in FindObjectsByType<House>(FindObjectsSortMode.None))
            {
                if (!_targetHouse)
                    _targetHouse = house;

                if (Vector2Int.Distance(GridPosition, house.GridPosition) <
                    Vector2Int.Distance(_targetHouse.GridPosition, house.GridPosition))
                {
                    _targetHouse = house;
                }
            }
        }


        private void OnEnable() => TurnsManager.Instance.TurnChanged += MoveToTarget;

        private void OnDisable() => TurnsManager.Instance.TurnChanged -= MoveToTarget;

        private async void MoveToTarget()
        {
            if (TurnsManager.Instance.IsPlayerTurn) return;

            for (int i = 0; i < MaxMoves; i++)
            {
                destroyCancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(300);
                destroyCancellationToken.ThrowIfCancellationRequested();
                MoveTo(CheckFreeDirection());
            }

            TurnsManager.Instance.EnemyFinishedItsMovement();
        }

        private Vector2Int CheckFreeDirection()
        {
            var map = MapManager.Instance.MapTilesMatrix;
            Vector2Int bestDir = Vector2Int.zero;

            Vector2Int[] directions =
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right,
            };

            foreach (var offset in directions)
            {
                var tile = map[GridPosition.x + offset.x, GridPosition.y + offset.y];

                if (tile && !tile.Unit)
                {
                    bestDir = Vector2Int.Distance(
                                  new(GridPosition.x + offset.x, GridPosition.y + offset.y), _targetHouse.GridPosition)
                            < Vector2Int.Distance(
                                  new(GridPosition.x + bestDir.x, GridPosition.y + bestDir.y),
                                  _targetHouse.GridPosition)
                        ? offset
                        : bestDir;
                }
            }

            return bestDir;
        }
    }
}