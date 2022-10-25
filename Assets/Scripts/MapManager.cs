using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase autoTile;
    [SerializeField] private Transform mapElementsObj;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject doorOff;
    [SerializeField] private GameObject doorOn;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject lampOff;
    [SerializeField] private GameObject lampOn;
    [SerializeField] private GameObject torch;
    [SerializeField] private GameObject pressurePlate;
    [SerializeField] private GameObject switchButton;
    [SerializeField] private GameObject victoryChest;
    private string filePath;
    private int levelIndex;

    private void Awake()
    {
        SetLevelIndex(2);
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
        tilemapDatas.mapElements = new List<MapElements>();

        foreach (Vector3Int tilePos in tilemap.cellBounds.allPositionsWithin)
            if (tilemap.HasTile(tilePos))
                tilemapDatas.tilesPos.Add(tilePos);
        foreach (Transform mapElementObj in mapElementsObj) {
            MapElements tempMapElements = new MapElements();
            tempMapElements.id = GetMapElementFromTag(mapElementObj.gameObject.tag);
            tempMapElements.pos = mapElementObj.localPosition;
            tempMapElements.idLink = GetLinkOfMapElements(mapElementsObj, tempMapElements.id, mapElementObj);
            tilemapDatas.mapElements.Add(tempMapElements);
        }

        string json = JsonUtility.ToJson(tilemapDatas);
        File.WriteAllText(filePath, json);
        Debug.Log("Save in : " + filePath);
    }

    private MapElement GetMapElementFromTag(string tag)
    {
        switch (tag) {
            case "Box":
                return MapElement.box;
            case "DoorOff":
                return MapElement.doorOff;
            case "DoorOn":
                return MapElement.doorOn;
            case "Key":
                return MapElement.key;
            case "LampOff":
                return MapElement.lampOff;
            case "LampOn":
                return MapElement.lampOn;
            case "Torch":
                return MapElement.torch;
            case "PressurePlate":
                return MapElement.pressurePlate;
            case "SwitchButton":
                return MapElement.switchButton;
            case "VictoryChest":
                return MapElement.victoryChest;
            default:
                return MapElement.error;
        }
    }

    // Link interactableObj (Key, PressurePlate, SwitchButton) with ActivableObj
    private List<int> GetLinkOfMapElements(Transform mapElementsObj, MapElement objId, Transform mapElementObj)
    {
        List<int> listMapElements = new List<int>();
        if (objId == MapElement.key) {
            int instanceId = mapElementObj.GetComponent<Key>().door.transform.GetInstanceID();
            SearchForLinkTwoMapElement(mapElementsObj, listMapElements, instanceId);
        } else if (objId == MapElement.pressurePlate) {
            foreach (ActivableObj activableObj in mapElementObj.GetComponent<PressurePlate>().activablesObj) {
                int instanceId = activableObj.transform.GetInstanceID();
                SearchForLinkTwoMapElement(mapElementsObj, listMapElements, instanceId);
            }
        } else if (objId == MapElement.switchButton) {
            foreach (ActivableObj activableObj in mapElementObj.GetComponent<SwitchButton>().activablesObj) {
                int instanceId = activableObj.transform.GetInstanceID();
                SearchForLinkTwoMapElement(mapElementsObj, listMapElements, instanceId);
            }
        }
        return listMapElements;
    }

    private void SearchForLinkTwoMapElement(Transform mapElementsObj, List<int> listMapElements, int instanceId)
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
        foreach (Vector3Int tilePos in tilemapsDatas.tilesPos)
            tilemap.SetTile(tilePos, autoTile);
        foreach (MapElements mapElements in tilemapsDatas.mapElements)
            CreateMapElement(mapElements, objs);
        foreach (MapElements mapElements in tilemapsDatas.mapElements) {
            SetLinkOfMapElements(objs, mapElements, count);
            count++;
        }
        Debug.Log("Load from " + filePath);
    }

    private void CreateMapElement(MapElements mapElements, List<Transform> objs)
    {
        switch (mapElements.id) {
            case MapElement.box:
                InstantiateObj(mapElements, objs, box);
                break;
            case MapElement.doorOff:
                InstantiateObj(mapElements, objs, doorOff);
                break;
            case MapElement.doorOn:
                InstantiateObj(mapElements, objs, doorOn);
                break;
            case MapElement.key:
                InstantiateObj(mapElements, objs, key);
                break;
            case MapElement.lampOff:
                InstantiateObj(mapElements, objs, lampOff);
                break;
            case MapElement.lampOn:
                InstantiateObj(mapElements, objs, lampOn);
                break;
            case MapElement.torch:
                InstantiateObj(mapElements, objs, torch);
                break;
            case MapElement.pressurePlate:
                InstantiateObj(mapElements, objs, pressurePlate);
                break;
            case MapElement.switchButton:
                InstantiateObj(mapElements, objs, switchButton);
                break;
            case MapElement.victoryChest:
                InstantiateObj(mapElements, objs, victoryChest);
                break;
        }
    }

    private void InstantiateObj(MapElements mapElements, List<Transform> objs, GameObject obj)
    {
        Transform tempObj;
        tempObj = Instantiate(obj, mapElementsObj).transform;
        tempObj.localPosition = mapElements.pos;
        objs.Add(tempObj);
    }

    private void SetLinkOfMapElements(List<Transform> objs, MapElements mapElements, int count)
    {
        switch (mapElements.id) {
            case MapElement.key:
                objs[count].GetComponent<Key>().door = objs[mapElements.idLink[0]].GetComponent<Door>();
                break;
            case MapElement.pressurePlate:
                objs[count].GetComponent<PressurePlate>().activablesObj = new List<ActivableObj>();
                foreach (int idLink in mapElements.idLink)
                    objs[count].GetComponent<PressurePlate>().activablesObj.Add(objs[idLink].GetComponent<ActivableObj>());
                break;
            case MapElement.switchButton:
                objs[count].GetComponent<SwitchButton>().activablesObj = new List<ActivableObj>();
                foreach (int idLink in mapElements.idLink)
                    objs[count].GetComponent<SwitchButton>().activablesObj.Add(objs[idLink].GetComponent<ActivableObj>());
                break;
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
    public List<Vector3Int> tilesPos;
    public List<MapElements> mapElements;
}

[System.Serializable]
public class MapElements
{
    public MapElement id;
    public Vector3 pos;
    public List<int> idLink;
}

public enum MapElement {
    error,          // 0
    box,            // 1
    doorOff,        // 2
    doorOn,         // 3
    key,            // 4
    lampOff,        // 5
    lampOn,         // 6
    torch,          // 7
    pressurePlate,  // 8
    switchButton,   // 9
    victoryChest,   // 10
};

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
120
82
146
148
56
*/
