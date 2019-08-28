using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the visuals of the arena and the node system.
/// </summary>
public class TileController : MonoBehaviour
{
    //Fields
    [SerializeField]
    private GameObject tileAnchorObj;
    [SerializeField]
    private GameObject refObj;
    [SerializeField]
    private GameObject pathEnemyObj;

    private PlayerController pC;
    private int arenaWidth = 28;
    private int arenaHeight = 22;
    private GameObject[,] tileObjArray;
    private Tile[,] tileArray;

    //Properties
    private List<List<Tile>> listOfLists;
    private List<Tile> spawnTiles1;
    private List<Tile> spawnTiles2;
    private List<Tile> targetTiles1;
    private List<Tile> targetTiles2;
    private _Enemy pathEnemy;

    public void Init(PlayerController pC)
    {
        this.pC = pC;

        tileObjArray = new GameObject[arenaWidth, arenaHeight];
        tileArray = new Tile[arenaWidth, arenaHeight];


        spawnTiles1 = new List<Tile>();
        targetTiles1 = new List<Tile>();
        spawnTiles2 = new List<Tile>();
        targetTiles2 = new List<Tile>();

        listOfLists = new List<List<Tile>>();
        listOfLists.Add(spawnTiles1);
        listOfLists.Add(targetTiles1);
        listOfLists.Add(spawnTiles2);
        listOfLists.Add(targetTiles2);

        refObj.SetActive(false);

        SetupMainTiles();
        SetupSpecialTiles();
    }

    // Update is called once per frame
    private void Update()
    {
        if (pC == null)
            return;
    }
    public List<Tile> GetTilesAroundPoint(Vector2 point)
    {
        var tiles = new List<Tile>();
        //Debug.Log(point.x + "_" + point.y);

        int minX = Mathf.FloorToInt(point.x - 0.5f);
        int minY = Mathf.FloorToInt(point.y - 0.5f);

        int maxX = Mathf.FloorToInt(point.x + 0.5f);
        int maxY = Mathf.FloorToInt(point.y + 0.5f);

        //clockwise
        tiles.Add(GetTileAt(minX, minY)); // 0, 0 - SW
        tiles.Add(GetTileAt(minX, maxY)); // 0, 1 - NW
        tiles.Add(GetTileAt(maxX, maxY)); // 1, 1 - NE
        tiles.Add(GetTileAt(maxX, minY)); // 1, 0 - SE

        return tiles;
    }
    private void SetupSpecialTiles()
    {
        GameObject sT1 = GameObject.FindGameObjectWithTag("SpawnTiles1");
        GameObject tT1 = GameObject.FindGameObjectWithTag("TargetTiles1");
        GameObject sT2 = GameObject.FindGameObjectWithTag("SpawnTiles2");
        GameObject tT2 = GameObject.FindGameObjectWithTag("TargetTiles2");

        for (int i = 0; i < sT1.transform.childCount; i++)
        {
            sT1.transform.GetChild(i).GetComponentInChildren<SpriteRenderer>().color = Color.green;

            Tile tile = sT1.transform.GetChild(i).GetComponent<Tile>();

            spawnTiles1.Add(tile);

            tile.Init(pC);

            tile.MyTileType = TileType.ENEMY;
        }
        for (int i = 0; i < tT1.transform.childCount; i++)
        {
            tT1.transform.GetChild(i).GetComponentInChildren<SpriteRenderer>().color = Color.red;

            Tile tile = tT1.transform.GetChild(i).GetComponent<Tile>();

            targetTiles1.Add(tile);

            tile.Init(pC);

            tile.MyTileType = TileType.ENEMY;
        }
        for (int i = 0; i < sT2.transform.childCount; i++)
        {
            sT2.transform.GetChild(i).GetComponentInChildren<SpriteRenderer>().color = Color.green;

            Tile tile = sT2.transform.GetChild(i).GetComponent<Tile>();

            spawnTiles2.Add(tile);

            tile.Init(pC);

            tile.MyTileType = TileType.ENEMY;
        }
        for (int i = 0; i < tT2.transform.childCount; i++)
        {
            tT2.transform.GetChild(i).GetComponentInChildren<SpriteRenderer>().color = Color.red;

            Tile tile = tT2.transform.GetChild(i).GetComponent<Tile>();

            targetTiles2.Add(tile);

            tile.Init(pC);

            tile.MyTileType = TileType.ENEMY;
        }
    }
    private void SetupMainTiles()
    {
        for (int X = 0; X < arenaWidth; X++)
        {
            for (int Y = 0; Y < arenaHeight; Y++)
            {
                tileObjArray[X, Y] = Instantiate(pC.prefabController.TilePrefab, new Vector3(X, Y, 0), Quaternion.identity, tileAnchorObj.transform);
                tileArray[X, Y] = tileObjArray[X, Y].GetComponent<Tile>();

                tileArray[X, Y].Init(pC);

                //? Comment out this to show nodes visually 
                tileArray[X, Y].spriteObj.SetActive(false);
            }
        }
    }
    public Tile GetSpawnTile(int waveNumber)
    {
        try
        {
            //Even
            if(waveNumber % 2 == 0)
            {
                int rnd = UnityEngine.Random.Range(0, spawnTiles2.Count);

                return spawnTiles2[rnd];

            }
            //Odd
            if (waveNumber % 2 != 0)
            {
                int rnd = UnityEngine.Random.Range(0, spawnTiles1.Count);

                return spawnTiles1[rnd];
            }
            else
            {
                throw new System.Exception();
            }
        }
        catch
        {
            Debug.LogError("GetSpawnTile");
            return null;
        }
    }
    public Tile GetTargetTile(int waveNumber)
    {
        try
        {
            //Even
            if (waveNumber % 2 == 0)
            {
                int rnd = UnityEngine.Random.Range(0, targetTiles2.Count);

                return targetTiles2[rnd];

            }
            //Odd
            if (waveNumber % 2 != 0)
            {
                int rnd = UnityEngine.Random.Range(0, targetTiles1.Count);

                return targetTiles1[rnd];
            }
            else
            {
                throw new System.Exception();
            }
        }
        catch
        {
            Debug.LogError("GetTargetNode");
            return null;
        }
    }
    //Check main node array
    public Tile GetTileAt(int X, int Y)
    {
        try
        {
            return tileArray[X, Y];
        }
        catch
        {
            return GetSpecialTile(X, Y);
        }
    }
    //If there is no tile at the coords check spawn and target tiles
    // ? Method should be kept as light as possible
    // todo refractor as much as possible
    private Tile GetSpecialTile(float X, float Y)
    {
        foreach (var list in listOfLists)
        {
            //If X matches - vertical 
            //If Y matches - horizontal
            if(list[0].X == X || list[0].Y == Y)
            {
                foreach (var node in list)
                {
                    if (node.X == X && node.Y == Y)
                    {
                        ///Debug.Log("GetSpecialNode");
                        return node;
                    }
                }
            }
        }

        return null;
    }
    public bool IsPathBlocked()
    {
        if(pathEnemy == null)
        {
            pathEnemy = pathEnemyObj.GetComponentInChildren<_Enemy>();
            pathEnemy.Init(pC, null, null);
        }

        var x = QPath.QPath.FindPath<Tile>(pC, pathEnemy, spawnTiles1[0], targetTiles1[0]);
        var y = QPath.QPath.FindPath<Tile>(pC, pathEnemy, spawnTiles2[0], targetTiles2[0]);

        if(x.Length == 0 || y.Length == 0)
        {
            Debug.Log("Path blocked");
        }

        return false;
    }
}
