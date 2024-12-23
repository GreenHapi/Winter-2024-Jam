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
            var houses = FindObjectsByType<House>(FindObjectsSortMode.None);
            if (houses.Length == 0)
            {
                Debug.LogWarning("No houses found on the map.");
                return;
            }

            foreach (var house in houses)
            {
                if (_targetHouse == null)
                {
                    _targetHouse = house;
                }

                if (Vector2Int.Distance(GridPosition, house.GridPosition) <
                    Vector2Int.Distance(GridPosition, _targetHouse.GridPosition))
                {
                    _targetHouse = house;
                }
            }
        }

        private void OnEnable() => TurnsManager.Instance.TurnChanged += MoveToTarget;

        private void OnDisable() => TurnsManager.Instance.TurnChanged -= MoveToTarget;

        private async void MoveToTarget()
        {
            if (TurnsManager.Instance.IsPlayerTurn || destroyCancellationToken.IsCancellationRequested)
                return;

            for (int i = 0; i < MaxMoves; i++)
            {
                destroyCancellationToken.ThrowIfCancellationRequested();

                Vector2Int direction = CheckFreeDirection();
                if (direction == Vector2Int.zero)
                    break;

                MoveTo(direction);

                
                FoundInteractable?.TryInteract(this);
                
                await Task.Delay(1000);
                destroyCancellationToken.ThrowIfCancellationRequested();
            }

            TurnsManager.Instance.EnemyFinishedItsMovement();
        }

        private Vector2Int CheckFreeDirection()
        {
            if (MapManager.Instance == null || MapManager.Instance.MapTilesMatrix == null)
            {
                Debug.LogError("MapManager or MapTilesMatrix is not initialized.");
                return Vector2Int.zero;
            }

            var map = MapManager.Instance.MapTilesMatrix;
            Vector2Int bestDir = Vector2Int.zero;
            float shortestDistance = float.MaxValue;

            Vector2Int[] directions =
            {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right,
            };

            foreach (var offset in directions)
            {
                int gridPositionX = GridPosition.x + offset.x;
                int gridPositionY = GridPosition.y + offset.y;
                if (MapManager.Instance.IsPositionInsideMapBounds(gridPositionX, gridPositionY) == false)
                {
                    Debug.Log("Out of map bounds.");
                    continue;
                }

                if (tile != null && tile.Unit == null) // Ensure the tile exists and is unoccupied
                {
                    float distance = Vector2Int.Distance(nextPos, _targetHouse.GridPosition);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        bestDir = offset;
                    }
                }
            }

            return bestDir;
        }
    }
}
