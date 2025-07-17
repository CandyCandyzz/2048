using System.Collections.Generic;

public class TileCollapse
{
    public void Collapse(List<Tile> tiles, TileNum[,] tilesNum)
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

                    SetTileNum(tilesNum, rowFrom, rowTo, colFrom, colTo, current);
                }
            }
        }
    }

    private void SetTileNum(TileNum[,] tilesNum,int rowFrom,int rowTo,int colFrom,int colTo,Tile targetTile)
    {
        TileNum tileNum = tilesNum[rowFrom, colFrom];
        tilesNum[rowFrom, colFrom] = null;
        tilesNum[rowTo, colTo] = tileNum;

        tileNum.target = targetTile.transform;
        tileNum.SetPos(targetTile, false);
    }
}
