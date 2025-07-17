using System.Collections.Generic;

public class TileMerge
{
    public void Merge(List<Tile> tiles, TileNum[,] tilesNum)
    {
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            Tile current = tiles[i];
            Tile next = tiles[i + 1];

            if (current.value == next.value && current.value != 0 
                && !current.isMerged && !next.isMerged && next.value != 0)
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

                SetTileNum(tilesNum, rowFrom, colFrom, rowTo, colTo);
            }
        }
    }

    private void SetTileNum(TileNum[,] tilesNum, int rowFrom,int colFrom,int rowTo, int colTo)
    {
        TileNum tileNumNext = tilesNum[rowFrom, colFrom];
        tileNumNext.isDestroyed = true;
        tileNumNext.target = tilesNum[rowTo, colTo].transform;

        tilesNum[rowFrom, colFrom] = null;
    }    
}
