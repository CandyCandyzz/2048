using UnityEngine;

public class Tile : MonoBehaviour
{
    public int value;

    public int row;
    public int col;

    public bool isEmpty = true;
    public bool isMerged = false;

    public void SetPos(int row,int col)
    {
        this.row = row;
        this.col = col;
    }    
}
