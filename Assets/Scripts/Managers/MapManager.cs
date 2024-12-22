using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterJam.Managers
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }
        [field:SerializeField] public Transform MapTilesTransform { get; private set; }
        [field:SerializeField] public MapTile[,] MapTilesMatrix { get; private set; }

        private void OnValidate()
        {
            Instance = this;
        }

        public void Awake()
        {
            float mapSize = Mathf.Sqrt(MapTilesTransform.childCount);
            MapTilesMatrix = new MapTile[(int)mapSize, (int)mapSize];
            
            for (int x = 0; x < mapSize-1; x++)
            {
                for (int y = 0; y < mapSize-1; y++)
                {
                    MapTilesMatrix[x, y] = MapTilesTransform.Find($"MapTile ({x}, {y})").GetComponent<MapTile>();
                }
            }
        }
    }
}