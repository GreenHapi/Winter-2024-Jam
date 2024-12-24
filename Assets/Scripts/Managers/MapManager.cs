using NaughtyAttributes;
using UnityEngine;

namespace WinterJam.Managers {
public class MapManager : MonoBehaviour {
    public static MapManager Instance { get; private set; }
    [field: SerializeField] public Transform MapTilesTransform { get; private set; }
    [field: SerializeField] public MapTile[,] MapTilesMatrix { get; private set; }
    public float MapSize { get; private set; }

    private void OnValidate() {
        Instance = this;
    }


    public void Awake() {
        Instance = this;
        MapSize = Mathf.Sqrt(MapTilesTransform.childCount);
        MapTilesMatrix = new MapTile[(int) MapSize, (int) MapSize];

        for (int x = 0; x < MapSize; x++) {
            for (int y = 0; y < MapSize; y++) {
                MapTilesMatrix[x, y] = MapTilesTransform.Find($"MapTile ({x}, {y})").GetComponent<MapTile>();
            }
        }

        AssignUnitsToTiles();
    }

    [Button]
    private void AssignUnitsToTiles() {
        Unit[] allUnits =
            GameObject.FindObjectsByType<Unit>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var obstacle in allUnits) {
            obstacle.FixMapTilePos();
        }
    }

    public bool IsPositionInsideMapBounds(int gridPositionX, int gridPositionY) {
        bool isPositionInsideMapBounds = (gridPositionX < MapSize && gridPositionX >= 0) &&
                                         (gridPositionY < MapSize && gridPositionY >= 0);
        if (!isPositionInsideMapBounds) {
            // Debug.Log($"Position x:{gridPositionX}, y: {gridPositionY} is not inside map bounds.");
        }

        return isPositionInsideMapBounds;
    }
}
}