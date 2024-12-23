using System;
using UnityEngine;
using WinterJam.Managers;
using WinterJam.Units.Buildings;

namespace WinterJam.Units.Characters {
/// Unit that can be played by player/bot
public abstract class Character : Unit {
    [field:SerializeField] public bool isActioned { get; private set; }
    [field: SerializeField] public int MaxMoves { get; private set; }
    [field: SerializeField] public int MovesLeft { get; private set; }
    [SerializeField] private bool _allowDiagonalInteractions = false;

    private IInteractable _foundInteractable;

    public void MoveTo(Vector2Int dir) {
        RotateToFaceMovementDirection(dir);

        MapTile tile = null;

        int gridPositionX = GridPosition.x + dir.x;
        int gridPositionY = GridPosition.y + dir.y;
        if (MapManager.Instance.IsPositionInsideMapBounds(gridPositionX, gridPositionY) == false) {
            Debug.Log($"{gridPositionX}, {gridPositionY}");
            return;
        }

        try {
            tile = MapManager.Instance.MapTilesMatrix[gridPositionX, gridPositionY];
        }
        catch (Exception e) {
            Debug.Log($"{gridPositionX}, {gridPositionX}");
            Console.WriteLine(e);
            throw;
        }

        if (tile == null) {
            return;
        }

        if (tile.Unit) {
            return;
        }

        if (MovesLeft > 0) {
            MoveOn(tile);
            MovesLeft--;
        }
        else {
            return;
        }
        if (MovesLeft <= 0)
            isActioned = true;
    }

    private void RotateToFaceMovementDirection(Vector2Int dir) {
        if (dir == Vector2Int.up) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (dir == Vector2Int.down) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (dir == Vector2Int.right) {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        if (dir == Vector2Int.left) {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private void MoveOn(MapTile tile) {
        if (_standingOnTile)
            _standingOnTile.Unit = null;

        tile.Unit = this;
        GridPosition = tile.GridPosition;
        _standingOnTile = tile;
        transform.localPosition = tile.transform.position;
        FindInteractableNextToTheCharacter();
    }

    public void Interact() {
        if (_foundInteractable != null) {
            bool success = _foundInteractable.TryInteract(this);
            if (this is Soldier) {
                if (_foundInteractable is House && success) {
                    Soldier soldier = (Soldier) this;
                    soldier.ChangeTorchState(false);
                }
            }
        }
    }

    public void ResetMoves() {
        MovesLeft = MaxMoves;
    }

    private void FindInteractableNextToTheCharacter() {
        if (this is not Soldier) {
            return;
        }

        MapTile[,] map = MapManager.Instance.MapTilesMatrix;

        Vector2Int[] allDirections = {
            new(1, 1),
            new(1, -1),
            new(-1, 1),
            new(-1, -1),
            new(0, 1),
            new(0, -1),
            new(1, 0),
            new(-1, 0)
        };

        Vector2Int[] nonDiagonalDirections = {
            new(0, 1),
            new(0, -1),
            new(1, 0),
            new(-1, 0)
        };

        Vector2Int[] directions = _allowDiagonalInteractions ? allDirections : nonDiagonalDirections;

        foreach (Vector2Int offset in directions) {
            int gridPositionX = GridPosition.x + offset.x;
            int gridPositionY = GridPosition.y + offset.y;

            if (MapManager.Instance.IsPositionInsideMapBounds(gridPositionX, gridPositionY) == false) {
                return;
            }

            MapTile tile = map[gridPositionX, gridPositionY];
            if (tile is null) {
                return;
            }

            if (tile && tile.Unit is IInteractable interactable) {
                print(interactable);
                _foundInteractable = interactable;
                break;
            }
        }

        return;


        // if (map[GridPosition.x + 1, GridPosition.y + 1].Unit &&  map[GridPosition.x + 1, GridPosition.y + 1].Unit is IInteractable interactable1)
        //     foundInteractable = interactable1;
        // else if (map[GridPosition.x + 1, GridPosition.y - 1].Unit && map[GridPosition.x + 1, GridPosition.y - 1].Unit is IInteractable interactable2)
        //     foundInteractable = interactable2;
        // else if (map[GridPosition.x - 1, GridPosition.y + 1].Unit&& map[GridPosition.x - 1, GridPosition.y + 1].Unit is IInteractable interactable3)
        //     foundInteractable = interactable3;
        // else if (map[GridPosition.x - 1, GridPosition.y - 1].Unit&& map[GridPosition.x - 1, GridPosition.y - 1].Unit is IInteractable interactable4)
        //     foundInteractable = interactable4;
        // else if (map[GridPosition.x, GridPosition.y + 1].Unit && map[GridPosition.x, GridPosition.y + 1].Unit is IInteractable interactable5)
        //     foundInteractable = interactable5;
        // else if (map[GridPosition.x, GridPosition.y - 1].Unit && map[GridPosition.x, GridPosition.y - 1].Unit is IInteractable interactable6)
        //     foundInteractable = interactable6;
        // else if (map[GridPosition.x + 1, GridPosition.y].Unit && map[GridPosition.x + 1, GridPosition.y].Unit is IInteractable interactable7)
        //     foundInteractable = interactable7;
        // else if (map[GridPosition.x - 1, GridPosition.y].Unit && map[GridPosition.x - 1, GridPosition.y].Unit is IInteractable interactable8)
        //     foundInteractable = interactable8;
    }
}
}