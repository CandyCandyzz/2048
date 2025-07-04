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

    //Animate
    public Transform target;
    [SerializeField] private float speed;

    //Display
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image image;

    //Status
    public bool isDestroyed = false;
    public bool isMoving = false;

    //PopUp
    [SerializeField] private float scaleValuePopUpMerge;
    [SerializeField] private float timedurPopUpMerge;
    [SerializeField] private float scaleValuePopUpSpawn;
    [SerializeField] private float timedurPopUpSpawn;

    private void Start()
    {
        StartPopUpSpawn();
        tileManager = FindAnyObjectByType<TileManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        SetDisPlay();
    }

    public void SetPos(Tile tile)
    {
        transform.position = tile.transform.position;
        this.tile = tile;
        this.value = tile.value;
        this.row = tile.row;
        this.col = tile.col;
    }

    public void SetPosInArray(Tile tile)
    {
        this.tile = tile;
        this.value = tile.value;
        this.row = tile.row;
        this.col = tile.col;
    }

    public void Move()
    {
        if (target == null)
        {
            return;
        }
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        isMoving = true;
        while (Vector2.Distance(transform.position, target.position) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        transform.position = target.position;
        TryGetTargetTileNum();
        Delete();
    }

    private void TryGetTargetTileNum()
    {
        TileNum targetTileNum;
        target.TryGetComponent<TileNum>(out targetTileNum);
        if (targetTileNum != null)
        {
            targetTileNum.StartPopUpMerge();
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

    public void StartPopUpMerge()
    {
        StartCoroutine(PopUpMerge());
    }    
    private IEnumerator PopUpMerge()
    {
        float time = 0;
        Vector3 originScale = transform.localScale;
        Vector3 newScale = originScale * scaleValuePopUpMerge;
        while (time < timedurPopUpMerge)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, newScale, time/timedurPopUpMerge);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = newScale;

        time = 0;
        while (time < timedurPopUpMerge)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originScale, time/timedurPopUpMerge);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originScale;
    }

    private void StartPopUpSpawn()
    {
        StartCoroutine(PopUpSpawn());
    }
    private IEnumerator PopUpSpawn()
    {
        float time = 0;
        Vector3 originScale = transform.localScale;
        transform.localScale = transform.localScale * scaleValuePopUpSpawn;
        while (time < timedurPopUpSpawn)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originScale, time / timedurPopUpSpawn);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originScale;
    }
}
