using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Linq;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase autoTile;
    [SerializeField] private Transform square;
    [SerializeField] private MouseLight mouseLight;
    [SerializeField] private List<MapObj> mapObjs;
    private string filePath;
    private int levelIndex;
    private List<Color> colors = new List<Color>() {
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow,
    };

    private void Awake()
    {
        SetLevelIndex(PlayerPrefs.GetInt("LevelId", 1));
        Load();
    }

    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.LeftControl)) {
    //         if (Input.GetKeyDown(KeyCode.A))
    //             Save();
    //         if (Input.GetKeyDown(KeyCode.L))
    //             Load();
    //         if (Input.GetKeyDown(KeyCode.R))
    //             FindObjectOfType<SceneLoader>().LoadNewScene("Game");
    //     }
    // }

    private void Save()
    {
        TilemapDatas tilemapDatas = new TilemapDatas();
        tilemapDatas.tilesPos = new List<Vector2Int>();
        tilemapDatas.mapElements = new List<MapElements>();

        foreach (Vector2Int tilePos in tilemap.cellBounds.allPositionsWithin)
            if (tilemap.HasTile((Vector3Int)tilePos))
                tilemapDatas.tilesPos.Add(tilePos);
        tilemapDatas.squarePos = square.position;
        tilemapDatas.mouseLightPos = mouseLight.transform.position;
        List<Transform> tempMapElementsObj = new List<Transform>();
        foreach (Transform child in transform)
            tempMapElementsObj.Add(child);
        tempMapElementsObj = tempMapElementsObj.OrderBy(obj => obj.position.x).ToList();
        foreach (Transform mapElementObj in tempMapElementsObj) {
            MapElements tempMapElements = new MapElements();
            tempMapElements.id = GetMapElementFromTag(mapElementObj.gameObject.tag);
            tempMapElements.pos = new Vector2(Mathf.Round(mapElementObj.localPosition.x * 2f) * 0.5f, Mathf.Round(mapElementObj.localPosition.y * 2f) * 0.5f);
            tempMapElements.rev = mapElementObj.localScale.x == -1;
            tempMapElements.rot = mapElementObj.eulerAngles.z;
            tempMapElements.idLink = GetLinkOfMapElements(tempMapElementsObj, tempMapElements.id, mapElementObj);
            tilemapDatas.mapElements.Add(tempMapElements);
        }

        string json = JsonUtility.ToJson(tilemapDatas);
        File.WriteAllText(filePath, json);
        Debug.Log("Save in : " + filePath);
    }

    private int GetMapElementFromTag(string tag)
    {
        for (int i = 0; i < mapObjs.Count; i++)
            if (mapObjs[i].tag == tag)
                return i;
        return 0;
    }

    private List<int> GetLinkOfMapElements(List<Transform> mapElementsObj, int objId, Transform mapElementObj)
    {
        List<int> listMapElements = new List<int>();
        if (mapObjs[objId].tag == "PressurePlate" || mapObjs[objId].tag == "SwitchButton") {
            foreach (ActivableObj activableObj in mapElementObj.GetComponent<Powering>().activablesObj) {
                int instanceId = activableObj.transform.GetInstanceID();
                SearchForLinkTwoMapElement(mapElementsObj, listMapElements, instanceId);
            }
        } else if (mapObjs[objId].tag == "Key") {
            int instanceId = mapElementObj.GetComponent<Key>().door.transform.GetInstanceID();
            SearchForLinkTwoMapElement(mapElementsObj, listMapElements, instanceId);
        }
        return listMapElements;
    }

    private void SearchForLinkTwoMapElement(List<Transform> mapElementsObj, List<int> listMapElements, int instanceId)
    {
        int idLink = 0;
        foreach (Transform tempMapElementObj in mapElementsObj) {
            if (tempMapElementObj.GetInstanceID() == instanceId) {
                listMapElements.Add(idLink);
                break;
            }
            idLink++;
        }
    }

    private void Load()
    {
        string json = File.ReadAllText(filePath);
        TilemapDatas tilemapsDatas = JsonUtility.FromJson<TilemapDatas>(json);
        List<Transform> objs = new List<Transform>();
        int count = 0;
        foreach (Vector2Int tilePos in tilemapsDatas.tilesPos)
            tilemap.SetTile((Vector3Int)tilePos, autoTile);
        foreach (MapElements mapElements in tilemapsDatas.mapElements)
            InstantiateObj(mapElements, objs, mapObjs[mapElements.id].go);
        foreach (MapElements mapElements in tilemapsDatas.mapElements) {
            SetLinkOfMapElements(objs, mapElements, count);
            count++;
        }
        if (PlayerPrefs.GetInt("CheckPoint", 0) == 0) {
            square.position = tilemapsDatas.squarePos;
            mouseLight.SetPos(tilemapsDatas.mouseLightPos);
        } else {
            Vector2 checkPointPos = GameObject.FindWithTag("CheckPoint").transform.position;
            square.position = checkPointPos;
            checkPointPos.y += 1f;
            mouseLight.SetPos(checkPointPos);
        }
        Debug.Log("Load from " + filePath);
    }

    private void InstantiateObj(MapElements mapElements, List<Transform> objs, GameObject obj)
    {
        Transform tempObj;
        tempObj = Instantiate(obj, transform).transform;
        tempObj.localPosition = mapElements.pos;
        if (mapElements.rev)
            tempObj.localScale = new Vector2(-1f, 1f);
        tempObj.Rotate(0f, 0f, mapElements.rot);
        objs.Add(tempObj);
    }

    private void SetLinkOfMapElements(List<Transform> objs, MapElements mapElements, int count)
    {
        if (mapObjs[mapElements.id].tag == "PressurePlate" || mapObjs[mapElements.id].tag == "SwitchButton") {
            objs[count].GetComponent<Powering>().activablesObj = new List<ActivableObj>();
            foreach (int idLink in mapElements.idLink)
                objs[count].GetComponent<Powering>().activablesObj.Add(objs[idLink].GetComponent<ActivableObj>());
        } else if (mapObjs[mapElements.id].tag == "Key") {
            Key tempKey = objs[count].GetComponent<Key>();
            Door tempDoor = objs[mapElements.idLink[0]].GetComponent<Door>();
            tempKey.door = tempDoor;
            int colorIndex = Random.Range(0, colors.Count);
            Color tempColor = colors[colorIndex];
            colors.RemoveAt(colorIndex);
            tempKey.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tempColor;
            tempDoor.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tempColor;
            tempDoor.transform.GetChild(1).GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    public void SetLevelIndex(int levelIndex)
    {
        this.levelIndex = levelIndex;
        filePath = Application.dataPath + "/JsonData/LevelsData/Level_" + levelIndex + ".json";
    }
}

[System.Serializable]
public class TilemapDatas
{
    public List<Vector2Int> tilesPos;
    public Vector2 squarePos;
    public Vector2 mouseLightPos;
    public List<MapElements> mapElements;
}

[System.Serializable]
public class MapElements
{
    public int id;
    public Vector2 pos;         // Position
    public float rot;           // Rotation
    public bool rev;            // Reversed
    public List<int> idLink;    // Link for power
}

[System.Serializable]
public class MapObj
{
    public string tag;
    public GameObject go;
}

/* Tiles use for autotiling :
0
1
2
3
4
5
7
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
120
82
146
148
56
65
11
*/
