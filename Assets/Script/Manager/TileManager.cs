using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //Tile
    private Tile[,] tiles = new Tile[4,4];
    private int[,] originValue;
    [SerializeField] private Transform bg;

    //TileNum
    [SerializeField] private Transform tileNumBG;
    private TileNum[,] tilesNum = new TileNum[4,4];

    //TileNumSO
    public List<TileNumSO> listTileNumSO = new();

    [SerializeField] private SlideAllTilesNum slideAllTilesNum;
    [SerializeField] private NumTileSpawner numTileSpawner;
    private TileMove tileMove = new();

    private bool isMoving = false;
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
            StartCoroutine(Move(Vector2.right));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move(Vector2.left));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(Move(Vector2.up));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Move(Vector2.down));
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
        numTileSpawner.Spawn(tiles,tilesNum);
        SetOriginTiles();
    }

    private IEnumerator Move(Vector2 dir)
    {
        isMoving = true;
        tileMove.Move(tiles,tilesNum,dir);


        slideAllTilesNum.Slide();
        yield return new WaitUntil(() => slideAllTilesNum.isDone);
        isMoving = false;

        if(IsChanged())
        {
            ResetMerge();
            Spawn();
        }
    }

    private bool IsChanged()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (tiles[x, y].value != originValue[x, y])
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void SetOriginTiles()
    {
        originValue = new int[4, 4];
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                originValue[x, y] = tiles[x, y].value;
            }
        }
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
