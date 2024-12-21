using System;
using UnityEngine;
using UnityEngine.Serialization;
using WinterJam.Managers;

namespace WinterJam
{
    /// Base class of stuff that can stand on tiles (characters, campfire, houses)
    public abstract class Unit : MonoBehaviour
    {
        [FormerlySerializedAs("Position")] [field: SerializeField] public Vector2Int GridPosition;
        [field: SerializeField] public float CheckDistance = 0.4f;

        private void Start()
        {
            FixMapTilePos();
        }

        [ContextMenu("Fix map tile unit")]
        private void FixMapTilePos()
        {
            foreach (Transform tile in MapManager.Instance.MapTilesTransform)
            {
                if (Vector3.Distance(transform.position, tile.position) <= CheckDistance)
                {
                    var mapTile =  tile.GetComponent<MapTile>();
                    mapTile.Unit = this;
                    GridPosition = mapTile.GridPosition;
                    transform.position = new (mapTile.GridPosition.x, 0, mapTile.GridPosition.y);
                }
            }
        }
    }
}