using System;
using System.Collections.Generic;
using UnityEngine;
using WinterJam;
using WinterJam.Managers;
using WinterJam.Units.Characters;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WavesManager : MonoBehaviour
    {
        [field:SerializeField] public int WaveNumber { get; private set; }
        [field: SerializeField] public List<Snowman> ActiveSnowmen { get; private set; }
        private List<MapTile> _snowmanTiles;
        [SerializeField] private GameObject _snowmanPrefab;

        private void NewWave()
        {
            WaveNumber++;
            for (int i = 0; i < (int)(2 + WaveNumber - 0.5f); i++)
            {
                MapTile tile = _snowmanTiles[Random.Range(0, _snowmanTiles.Count-1)];
                
                    // print();
                if(tile)
                    Instantiate(_snowmanPrefab, tile.transform.position, Quaternion.identity).GetComponent<Snowman>().MoveOn(tile);
            }
        }

        private void Start()
        {
            _snowmanTiles = new List<MapTile>();
            for (int i = 0; i < MapManager.Instance.MapTilesTransform.childCount - 1; i++)
            {
                var tile = MapManager.Instance.MapTilesTransform.GetChild(i).GetComponent<MapTile>();
                if (tile && tile.CanSpawnSnowman)
                {
                    print(tile);
                    print(_snowmanTiles);
                    _snowmanTiles.Add(tile);
                    print(tile);
                }
            }
        
            NewWave();
        }
    }
}