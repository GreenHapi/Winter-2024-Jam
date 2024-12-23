using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WinterJam;
using WinterJam.Managers;
using WinterJam.Units.Characters;
using Random = UnityEngine.Random;

namespace Managers {
public class GameManager : MonoBehaviour 
{
    [SerializeField] private int _newGameSceneIndex = 1;
    
    [field:SerializeField] public int WaveNumber { get; private set; }
    [field:SerializeField] public List<Snowman> ActiveSnowmen { get; private set; }
    private MapTile[] _snowmanTiles;
    private GameObject _snowmanPrefab;

    private void NewWave()
    {
        WaveNumber++;
        for (int i = 0; i < (int)(2 + WaveNumber - 0.5f); i++)
        {
            MapTile tile = _snowmanTiles[Random.Range(0, _snowmanTiles.Length - 1)];
            Instantiate(_snowmanPrefab, tile.transform.position, Quaternion.identity).GetComponent<Snowman>().MoveOn(tile);
        }
    }
    

    public void NewGame() 
    {
        SceneManager.LoadScene(_newGameSceneIndex);
        _snowmanTiles = new MapTile[MapManager.Instance.MapTilesTransform.childCount];
        for (int i = 0; i < MapManager.Instance.MapTilesTransform.childCount; i++)
        {
            _snowmanTiles[i] = MapManager.Instance.MapTilesTransform.GetChild(i).GetComponent<MapTile>();
        }
        
        NewWave();
    }

    public static void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
}