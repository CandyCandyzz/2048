using System.Collections.Generic;
using UnityEngine;

public class TileMove
{
    private TileMerge tileMerge = new();
    private TileCollapse tileCollapse = new();

    Tile[,] tiles;
    TileNum[,] tilesNum;

    public void Move(Tile[,] tiles, TileNum[,] tilesNum, Vector2 dir)
    {
        this.tiles = tiles;
        this.tilesNum = tilesNum;

        if (dir == Vector2.right)
        {
            MoveRight();
            return;
        }
        if (dir == Vector2.left)
        {
            MoveLeft();
            return;
        }
        if (dir == Vector2.up)
        {
            MoveUp();
            return;
        }
        if (dir == Vector2.down)
        {
            MoveDown();
            return;
        }
    }
    private void MoveRight()
    {
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
            tileMerge.Merge(tilesMerge,tilesNum);
            tileCollapse.Collapse(row, tilesNum);
        }
        //if (!isChange)
        //{
        //    isMoving = false;
        //    return;
        //}
    }
    private void MoveLeft()
    {
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
            tileMerge.Merge(tilesMerge, tilesNum);
            tileCollapse.Collapse(row, tilesNum);
        }
    }
    private void MoveUp()
    {
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
            tileMerge.Merge(tilesMerge, tilesNum);
            tileCollapse.Collapse(col, tilesNum);
        }
    }
    private void MoveDown()
    {
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
            tileMerge.Merge(tilesMerge, tilesNum);
            tileCollapse.Collapse(col, tilesNum);
        }
    }
}
