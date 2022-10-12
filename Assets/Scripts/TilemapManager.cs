using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase autoTile;
    private string filePath;
    private int levelIndex;

    private void Awake()
    {
        SetLevelIndex(1);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetKeyDown(KeyCode.A))
                Save();
            if (Input.GetKeyDown(KeyCode.L))
                Load();
        }
    }

    private void Save()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        TilemapDatas tilemapDatas = new TilemapDatas();
        tilemapDatas.tilesPos = new List<Vector3Int>();

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                    tilemapDatas.tilesPos.Add(new Vector3Int(x, y, 0));
            }
        }
        string json = JsonUtility.ToJson(tilemapDatas);
        File.WriteAllText(filePath, json);
        Debug.Log("Save in : " + filePath);
    }

    private void Load()
    {
        string json = File.ReadAllText(filePath);
        TilemapDatas tilemapsDatas = JsonUtility.FromJson<TilemapDatas>(json);
        foreach (Vector3Int tilePos in tilemapsDatas.tilesPos)
            tilemap.SetTile(tilePos, autoTile);
        Debug.Log("Load from " + filePath);
    }

    public void SetLevelIndex(int levelIndex)
    {
        this.levelIndex = levelIndex;
        filePath = Application.dataPath + "/LevelsData/Level_" + levelIndex + ".json";
    }
}

[System.Serializable]
public class TilemapDatas
{
    public List<Vector3Int> tilesPos;
}
