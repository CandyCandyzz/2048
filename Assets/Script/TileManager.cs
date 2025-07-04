using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Tile
    private Tile[,] tiles = new Tile[4,4];
    [SerializeField] private Transform bg;

    //TileNum
    [SerializeField] private Transform tileNumBG;
    [SerializeField] private GameObject prefabsTileNum;
    private TileNum[,] tilesNum = new TileNum[4,4];

    //TileNumSO
    public List<TileNumSO> listTileNumSO = new();

    private bool isMoving = false;
    private bool isChange = false;
    private IEnumerator Start()
    {
        GetTile();
        yield return null;
        Spawn();
        Spawn();
    }

    private void Update()
    {
        if (isMoving) { return; }
        if(Input.GetKeyDown(KeyCode.D))
        {
            isMoving = true;
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            isMoving = true;
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            isMoving = true;
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isMoving = true;
            MoveDown();
        }
    }

    private void GetTile()
    {
        int index = 0;
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (global::System.Int32 y = 0; y < tiles.GetLength(1); y++)
            {
                Tile tile = bg.GetChild(index).GetComponent<Tile>();
                tile.SetPos(x, y);
                tiles[x,y] = tile;
                index++;
            }
        }
    }

    public void Spawn()
    {
        List<Tile> tileEmpty = new();

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (tiles[x, y].isEmpty == true)
                {
                    tileEmpty.Add(tiles[x, y]);
                }
            }
        }
        if (tileEmpty.Count == 0) { return; }
        int index = Random.Range(0, tileEmpty.Count);

        tileEmpty[index].value = 2;
        tileEmpty[index].isEmpty = false;

        TileNum tileNum = Instantiate(prefabsTileNum, tileNumBG).GetComponent<TileNum>();
        tileNum.SetPos(tileEmpty[index]);
        tilesNum[tileNum.row, tileNum.col] = tileNum;
    }

    private void MoveRight()
    {
        isChange = false;
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            List<Tile> row = new();
            List<Tile> tilesMerge = new();
            for (int y = tiles.GetLength(1) - 1; y >= 0; y--)
            {
                row.Add(tiles[x, y]);
                if (tiles[x, y].value != 0)
                {
                    tilesMerge.Add(tiles[x, y]);
                }
            }
            Merge(tilesMerge);
            MoveValues(row);
        }
        if (!isChange)
        {
            isMoving = false;
            return;
        }
        StartCoroutine(AnimateAllTilesThenSpawn());
        ResetMerge();
    }
    private void MoveLeft()
    {
        isChange = false;
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            List<Tile> row = new();
            List<Tile> tilesMerge = new();
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                row.Add(tiles[x, y]);
                if (tiles[x, y].value != 0)
                {
                    tilesMerge.Add(tiles[x, y]);
                }
            }
            Merge(tilesMerge);
            MoveValues(row);
        }
        if (!isChange)
        {
            isMoving = false;
            return;
        }
        StartCoroutine(AnimateAllTilesThenSpawn());
        ResetMerge();
    }
    private void MoveUp()
    {
        isChange = false;
        for (int y = 0; y < tiles.GetLength(0); y++)
        {
            List<Tile> col = new();
            List<Tile> tilesMerge = new();
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                col.Add(tiles[x, y]);
                if (tiles[x, y].value != 0)
                {
                    tilesMerge.Add(tiles[x, y]);
                }
            }
            Merge(tilesMerge);
            MoveValues(col);
        }
        if (!isChange)
        {
            isMoving = false;
            return;
        }
        StartCoroutine(AnimateAllTilesThenSpawn());
        ResetMerge();
    }
    private void MoveDown()
    {
        isChange = false;
        for (int y = 0; y < tiles.GetLength(0); y++)
        {
            List<Tile> col = new();
            List<Tile> tilesMerge = new();
            for (int x = tiles.GetLength(1) - 1; x >= 0; x--)
            {
                col.Add(tiles[x, y]);
                if (tiles[x, y].value != 0)
                {
                    tilesMerge.Add(tiles[x, y]);
                }
            }
            Merge(tilesMerge);
            MoveValues(col);
        }
        if (!isChange) 
        { 
            isMoving = false;
            return; 
        }
        StartCoroutine(AnimateAllTilesThenSpawn());
        ResetMerge();
    }

    private void Merge(List<Tile> tiles)
    {
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            Tile current = tiles[i];
            Tile next = tiles[i+1];
            if (current.value == next.value && current.value != 0 && !current.isMerged && !next.isMerged && next.value != 0)
            {
                current.value *= 2;
                current.isMerged = true;
                current.isEmpty = false;

                next.value = 0;
                next.isEmpty = true;

                int rowFrom = next.row;
                int colFrom = next.col;

                int rowTo = current.row;
                int colTo = current.col;

                TileNum tileNumNext = tilesNum[rowFrom, colFrom];
                tileNumNext.isDestroyed = true;
                tileNumNext.target = tilesNum[rowTo, colTo].transform;

                tilesNum[rowFrom, colFrom] = null;
                isChange = true;
            }
        }
    }

    private void MoveValues(List<Tile> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            for (int j = i + 1; j < tiles.Count; j++)
            {
                Tile current = tiles[i];
                Tile next = tiles[j];

                if (current.value == 0 && next.value != 0)
                {
                    current.value += tiles[j].value;
                    next.value = 0;

                    int rowFrom = next.row;
                    int colFrom = next.col;

                    int rowTo = current.row;
                    int colTo = current.col;

                    current.isEmpty = false;
                    next.isEmpty = true;

                    TileNum tileNum = tilesNum[rowFrom, colFrom];
                    tilesNum[rowFrom, colFrom] = null;
                    tilesNum[rowTo,colTo] = tileNum;
                    tileNum.target = current.transform;
                    tileNum.SetPosInArray(current);
                    isChange = true;
                }   
            }
        }
    }

    private bool AllTilesDone()
    {
        for (int i = 0; i < tileNumBG.childCount; i++)
        {
            TileNum tile = tileNumBG.GetChild(i).GetComponent<TileNum>();
            if (tile.isMoving) return false;
        }
        return true;
    }
    private IEnumerator AnimateAllTilesThenSpawn()
    {
        for (int i = 0; i < tileNumBG.childCount; i++)
        {
            tileNumBG.GetChild(i).GetComponent<TileNum>().Move();
        }
        yield return new WaitUntil(() => AllTilesDone());
        isMoving = false;
        Spawn();
    }
    private void ResetMerge()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tiles[x,y].isMerged = false;
            }
        }
    }

    public bool IsAnyTileEmpty()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if(tiles[x,y].isEmpty)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsAnyTileCanMerge()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (y < 3 && tiles[x,y].value == tiles[x,y+1].value) { return true; }
                if (x < 3 && tiles[x, y].value == tiles[x+1,y].value) { return true; }
            }
        }
        return false;
    }
}
