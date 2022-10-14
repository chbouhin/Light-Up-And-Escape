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
        TilemapDatas tilemapDatas = new TilemapDatas();
        tilemapDatas.tilesPos = new List<Vector3Int>();

        foreach (Vector3Int tilePos in tilemap.cellBounds.allPositionsWithin)
            if (tilemap.HasTile(tilePos))
                tilemapDatas.tilesPos.Add(tilePos);
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

/* Tiles use for autotiling :
0
1
2
3
4
5
6
7
8
9
17
145
154
67
44
30
80
63
105
138
18
104
40
144
38
60
16
13
122
35
58
78
85
142
124
118
140
33
127
*/
