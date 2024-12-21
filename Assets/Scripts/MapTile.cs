using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;

namespace WinterJam
{
    public class MapTile : MonoBehaviour
    {
        [field: SerializeField] public Unit Unit;
        [FormerlySerializedAs("MaxPosition")] [field: SerializeField] public Vector2Int GridPosition;

        private void OnValidate()
        {
            GridPosition = new ((int)transform.position.x, (int)transform.position.z);
            name = $"MapTile ({GridPosition.x}, {GridPosition.y})";
        }
    }
}