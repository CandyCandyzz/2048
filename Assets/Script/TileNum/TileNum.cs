using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileNum : MonoBehaviour
{
    private TileManager tileManager;
    private GameManager gameManager;

    public int row;
    public int col;
    public int value;
    public Tile tile;

    //Slide
    public Transform target;

    //Display
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image image;

    //Status
    public bool isDestroyed = false;
    public bool isMoving = false;

    //PopUp
    [SerializeField] private NumTileEffect numTileEffect;

    //Slide
    [SerializeField] private SlideTileNum slideTileNum;

    private void Start()
    {
        tileManager = FindAnyObjectByType<TileManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        SetDisPlay();
    }

    public void SetPos(Tile tile, bool isSpawn)
    {
        if (isSpawn)
        {
            transform.position = tile.transform.position;
        }
        this.tile = tile;
        value = tile.value;
        row = tile.row;
        col = tile.col;
    }

    public void StartSlide()
    {
        if (target == null)
        {
            return;
        }
        StartCoroutine(Slide());
    }
    private IEnumerator Slide()
    {
        isMoving = true;
        slideTileNum.Slide(target.position);
        yield return new WaitUntil(() => slideTileNum.isDone);
        isMoving = false;

        TryGetTargetTileNum();
        Delete();
    }    

    private void TryGetTargetTileNum()
    {
        TileNum targetTileNum = target.GetComponent<TileNum>();
        if (targetTileNum != null)
        {
            targetTileNum.PlayPopUp();
            targetTileNum.SetDisPlay();
            targetTileNum.AddScore();
        }
    }

    public void SetDisPlay()
    {
        TileNumSO tileNumSO = tileManager.listTileNumSO.Find(a => a.value == tile.value);
        textMeshPro.text = tile.value.ToString();
        textMeshPro.color = tileNumSO.colorNum;
        image.color = tileNumSO.colorTile;
    }
    private void Delete()
    {
        if (isDestroyed)
        {
            Destroy(gameObject);
        }
    }
    private void AddScore()
    {
        gameManager.ScoreIncrease(tile.value);
    }

    public void PlayPopUp()
    {
        numTileEffect.StartPopUpMerge();
    }    
}
