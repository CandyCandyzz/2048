using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameOver
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TileManager tileManager;

    //Score
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textBest;
    private int score;
    private int best;

    private void Start()
    {
        GetSaveBest();
    }
    private void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if (!tileManager.IsAnyTileEmpty() && !tileManager.IsAnyTileCanMerge())
        {
            gameOverPanel.SetActive(true);
            SaveBest();
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ScoreIncrease(int score)
    {
        this.score += score;
        textScore.text = this.score.ToString();
    }
    
    private void SaveBest()
    {
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BestScore", best);
            PlayerPrefs.Save();
        }
    }
    private void GetSaveBest()
    {
        best = PlayerPrefs.GetInt("BestScore", 0);
        textBest.text = best.ToString();
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }    
}
