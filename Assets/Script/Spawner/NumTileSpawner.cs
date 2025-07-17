using System.Collections.Generic;
using UnityEngine;

public class NumTileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabsTileNum;
    [SerializeField] private Transform tileNumBG;


    public void Spawn(Tile[,] tiles, TileNum[,] tilesNum)
    {
        List<Tile> tilesEmpty = FindEmptySlot(tiles);
        if (tilesEmpty.Count == 0) { return; }

        int index = Random.Range(0, tilesEmpty.Count);

        tilesEmpty[index].value = 2;
        tilesEmpty[index].isEmpty = false;

        CreateTile(tilesEmpty,tilesNum,index);
    }

    private List<Tile> FindEmptySlot(Tile[,] tiles)
    {
        List<Tile> tilesEmpty = new();
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (tiles[x, y].isEmpty == true)
                {
                    tilesEmpty.Add(tiles[x, y]);
                }
            }
        }
        return tilesEmpty;
    }

    private void CreateTile(List<Tile> tilesEmpty, TileNum[,] tilesNum, int index)
    {
        TileNum tileNum = Instantiate(prefabsTileNum, tileNumBG).GetComponent<TileNum>();
        tileNum.SetPos(tilesEmpty[index],true);
        tilesNum[tileNum.row, tileNum.col] = tileNum;
    }
}
