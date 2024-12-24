using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using WinterJam.Managers;
using WinterJam.Units.Buildings;
using Random = UnityEngine.Random;

namespace WinterJam.Units.Characters
{
    public class Snowman : Character
    {
        [SerializeField] private House _targetHouse;
        [SerializeField] private NavMeshAgent _agent;

        private void Awake()
        {
            _targetHouse = FindObjectsByType<House>(FindObjectsSortMode.None).OrderBy(x => Random.value).First();
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(_targetHouse.transform.position);
        }

        private void OnEnable() => TurnsManager.Instance.TurnChanged += MoveToTarget;

        private void OnDisable() => TurnsManager.Instance.TurnChanged -= MoveToTarget;


        private void Update()
        {
            if (TurnsManager.Instance.IsPlayerTurn || destroyCancellationToken.IsCancellationRequested)
                return;
            StartCoroutine(MoveAgentBySteps());
        }
        

        IEnumerator MoveAgentBySteps()
        {
            // Calculate the direction to the target
            Vector3 direction = _targetHouse.transform.position - transform.position;
        
            // Keep moving the agent until it's close enough to the target
            while (direction.magnitude > 1)
            {
                for (int i = 0; i < MaxMoves; i++)
                {
                    // Normalize direction so it can be used as a unit vector
                    direction.Normalize();

                    // Move the agent by one unit (teleport)
                    _agent.Warp(transform.position + direction);

                    // Wait for the specified delay before the next step
                    yield return new WaitForSeconds(0.5f);
                
                    // Recalculate the direction to the target after each step
                    direction = _targetHouse.transform.position - transform.position;

                    // If the agent is close enough, break out of the loop
                    if (direction.magnitude <= 1)
                    {
                        break;
                    }
                }

                // Wait for a cooldown or pause after moving the specified number of steps
                yield return new WaitForSeconds(0.5f);
            }

            // When the agent is within range of the target, teleport directly to the target
            _agent.Warp(_targetHouse.transform.position);
            Debug.Log("Agent reached the target!");
        }
        

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

                var tile = map[gridPositionX, gridPositionY];

                if (tile != null && tile.Unit == null) // Ensure the tile exists and is unoccupied
                {
                    float distance = Vector2Int.Distance(new(gridPositionX, gridPositionY), _targetHouse.GridPosition);
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