using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;
using static ReadJson;
using static UnityEditor.Experimental.GraphView.GraphView;


public enum MapOrientation
{
    Orthogonal,
    Isometric,
    Staggered,
    Hexagonal,
}

public enum StaggerAxis
{
    X,
    Y,
}
public enum StaggerIndex
{
    Even,
    Odd,
}
public class ReadJson : MonoBehaviour
{
    public MapJsonStructor mapJsonStructor;
    public Tile[] tileList;
    public Tilemap mainTileMap, obstacleLayerTileMap;
    public Dictionary<uint, byte> obstacleIndexInfomationDic;
    private TilemapCollider2D currentTilemapCollider2d;

    private MapOrientation m_Orientation = MapOrientation.Staggered;
    private StaggerAxis m_StaggerAxis = StaggerAxis.X;
    private StaggerIndex m_StaggerIndex = StaggerIndex.Odd;
    private void Awake()
    {
        //tileList = new Tile[1000];
        //for(int i=0; i<tileList.Length; i++)
        //{
        //    tileList[i] = (Tile) Resources.Load("Assets/Cainos/Pixel Art Top Down - Basic/Tile Palette/TP Wall/TX Tileset Wall_25.asset");
        //}
        mapJsonStructor = new MapJsonStructor();
        mapJsonStructor.layers = new layer[] { };
        obstacleIndexInfomationDic = new Dictionary<uint, byte>();
        //obstacleIndexInfomationDic.Add(8, 1);
        //obstacleIndexInfomationDic.Add(7, 2);
     
        m_Orientation = MapOrientation.Staggered;
        m_StaggerAxis = StaggerAxis.X;
        m_StaggerIndex = StaggerIndex.Odd;

    }
    // Start is called before the first frame update
    void Start()
    {

        readTextFile("Assets/MapJson/Map1.json");
        
    }

  

    void readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadToEnd();
          //  Debug.Log(inp_ln);
            // Do Something with the input. 
            LoadWorldMapFromJsonData(inp_ln);
            StartCoroutine(RefreshCollider(obstacleLayerTileMap));
        }

        inp_stm.Close();
    }
    IEnumerator RefreshCollider(Tilemap tilemap)
    {
        if (currentTilemapCollider2d)
        {
            Destroy(currentTilemapCollider2d);
        }
        yield return new WaitForEndOfFrame(); // need this!
                                              // Update the tilemap collider by disabling and enabling the TilemapCollider2D component. This is the only solution I found to refresh it during runtime.
        currentTilemapCollider2d = tilemap.gameObject.AddComponent<TilemapCollider2D>();
        yield return new WaitForEndOfFrame();
        AstarPath.active.Scan();
    }
    [System.Serializable]
    public class MapJsonStructor
    {
        public int compressionlevel;
        public int width;
        public int height;
        public bool infinite;
        public layer[] layers;
        public int nextlayerid;
        public int nextobjectid;
        public string orientation;
        public string renderorder;
        public string staggeraxis;
        public string staggerindex;
        public string tiledversion;
        public int tilewidth;
        public int tileheight;
        public tileset[] tilesets;
        public string type;
        public string version;
    }
    [System.Serializable]
    public class tileset
    {
        public int firstgid;
        public string source;
    }

    [System.Serializable]
    public class layer
    {
        public int[] data;
        public int width;
        public int height;
        public int id;
        public string name;
        public int opacity;
        public string type;
        public bool visible;
        public int x;
        public int y;
    }

        
    public void LoadWorldMapFromJsonData(string mapDataJson)
    {
        mainTileMap.ClearAllTiles();
        obstacleLayerTileMap.ClearAllTiles();
        mapJsonStructor = JsonUtility.FromJson<MapJsonStructor>(mapDataJson);
        string json = JsonUtility.ToJson(mapJsonStructor.layers[0]);
        Debug.Log(json);
        layer layer = mapJsonStructor.layers[0];
        if (!string.IsNullOrEmpty(mapJsonStructor.orientation) && mapJsonStructor.orientation != "null")
        {
            m_Orientation = (MapOrientation)Enum.Parse(typeof(MapOrientation), mapJsonStructor.orientation, true);
        }
        if (!string.IsNullOrEmpty(mapJsonStructor.staggerindex) && mapJsonStructor.staggerindex != "null")
        {
            m_StaggerIndex = (StaggerIndex)Enum.Parse(typeof(StaggerIndex), mapJsonStructor.staggerindex, true);
        }
        if (!string.IsNullOrEmpty(mapJsonStructor.staggeraxis) && mapJsonStructor.staggeraxis != "null")
        {
            m_StaggerAxis = (StaggerAxis)Enum.Parse(typeof(StaggerAxis), mapJsonStructor.staggeraxis, true);
        }
        Debug.Log(mapJsonStructor.layers[0]);

        int mapSize = layer.width * layer.height;
        for (int i = 0; i < mapSize; i++)
        {
            uint utId = (uint)layer.data[i];
      
            if (utId != 0)
            {
                //bebause the data is 16 but sprite split is 15, sub 1 to match with my Unity sprite config
                //customTileBases.sprite = mapIndexListSprite[utId];
                Vector3Int int3 = TiledIndexToGridCell(i, 0, -layer.height, layer.width);
                if (tileList[utId] == null)
                {
                    tileList[utId] = tileList[1];
                }
                if (!obstacleIndexInfomationDic.ContainsKey(utId))
                {
                  
                    mainTileMap.SetTile(int3, tileList[utId]);
                    Debug.LogError("data1:" + utId);
                }
                else
                {
                    obstacleLayerTileMap.SetTile(int3, tileList[utId]);
                    Debug.LogError("data2:" + utId);
                }
            }
        }
        //try to get min minX, minY, maxX, maxY
        float minX;
        float maxX;
        float maxY;
        float minY;
        if (m_Orientation == MapOrientation.Staggered)
        {
            minX = 0;
            maxX = (((float)mapJsonStructor.tilewidth / 100f) * mapJsonStructor.width) / 2;
            maxY = ((mapJsonStructor.tileheight / 100f) * mapJsonStructor.height);
            // minY = -((mapJsonStructor.tileheight / 100f) * mapJsonStructor.height);
            minY = 0;
        }
        else
        {
            minX = -2;
            maxY = ((((float)layer.height * 128f) / 96f) / 2f) + 2;
            minY = maxY * -1;
            maxX = (((float)layer.width * 256f) / 96f) + 1;
        }

        if (m_StaggerAxis == StaggerAxis.Y)
        {
            maxX *= 2;
            maxY /= 2;
            minY /= 2;
        }

        //Debug.Log("minx" + minX + "miny:" + minY + "maxX:" + maxX + "maxY:" + maxY);
        //because we slide up the Y to positive value so we need calculater minY -> maxY, maxy*2

        //float fixMinY = minY + Mathf.Abs(minY);
        //Debug.Log("minY:" + minY + "fix:" +fixMinY);
        //float fixMaxY = maxY + (Mathf.Abs(maxY)* 2);
        //Debug.Log("maxY:" + maxY + "fix:" + fixMaxY);
        //new flow
        maxY = layer.height;
        maxX = layer.width;
      
    }

    private Vector3Int TiledIndexToGridCell(int index, int offset_x, int offset_y, int stride)
    {
        int x = index % stride;
        int y = index / stride;
        x += offset_x;
        y += offset_y;
        var pos3 = TiledCellToGridCell(x, y);
        pos3.y = -pos3.y;

        return pos3;
    }

    private Vector3Int TiledCellToGridCell(int x, int y)
    {
        if (m_Orientation == MapOrientation.Isometric)
        {
            return new Vector3Int(-y, x, 0);
        }
        else if (m_Orientation == MapOrientation.Staggered)
        {
            var isStaggerX = m_StaggerAxis == StaggerAxis.X;
            var isStaggerOdd = m_StaggerIndex == StaggerIndex.Odd;

            if (isStaggerX)
            {
                var pos = new Vector3Int(x - y, x + y, 0);

                if (isStaggerOdd)
                {
                    pos.x -= (x + 1) / 2;
                    pos.y -= x / 2;
                }
                else
                {
                    pos.x -= x / 2;
                    pos.y -= (x + 1) / 2;
                }

                return pos;
            }
            else
            {
                var pos = new Vector3Int(x, y + x, 0);

                if (isStaggerOdd)
                {
                    var stagger = y / 2;
                    pos.x -= stagger;
                    pos.y -= stagger;
                }
                else
                {
                    var stagger = (y + 1) / 2;
                    pos.x -= stagger;
                    pos.y -= stagger;
                }

                return pos;
            }
        }
        else if (m_Orientation == MapOrientation.Hexagonal)
        {
            var isStaggerX = m_StaggerAxis == StaggerAxis.X;
            var isStaggerOdd = m_StaggerIndex == StaggerIndex.Odd;

            if (isStaggerX)
            {
                // Flat top hex
                if (isStaggerOdd)
                {
                    return new Vector3Int(-y, -x - 1, 0);
                }
                else
                {
                    return new Vector3Int(-y, -x, 0);
                }
            }
            else
            {
                // Pointy top hex
                if (isStaggerOdd)
                {
                    return new Vector3Int(x, y, 0);
                }
                else
                {
                    return new Vector3Int(x, y + 1, 0);
                }
            }
        }

        // Simple maps (like orthongal do not transform indices into other spaces)
        return new Vector3Int(x, y, 0);
    }
}
