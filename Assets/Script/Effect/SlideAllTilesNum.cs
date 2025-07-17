using System.Collections;
using UnityEngine;

public class SlideAllTilesNum : MonoBehaviour
{
    [SerializeField] private Transform tileNumBG;

    public bool isDone = false;
    public void Slide()
    {
        StartCoroutine(SlideTilesNum());
    }    

    private IEnumerator SlideTilesNum()
    {
        isDone = false;
        for (int i = 0; i < tileNumBG.childCount; i++)
        {
            tileNumBG.GetChild(i).GetComponent<TileNum>().StartSlide();
        }
        yield return new WaitUntil(() => AllTilesDone());
        isDone = true;
    }
    private bool AllTilesDone()
    {
        for (int i = 0; i < tileNumBG.childCount; i++)
        {
            TileNum tileNum = tileNumBG.GetChild(i).GetComponent<TileNum>();
            if (tileNum.isMoving) return false;
        }
        return true;
    }
}
