using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameDataPref : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    int maxscore = 0;
    void Start()
    {
        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(int score)
    {
        if(score > maxscore)
        {
            maxscore = score;
            //maxScoreText.text = "BestScore: " + maxscore;
            PlayerPrefs.SetInt("PlayerMaxScore", maxscore);
        }
        PlayerPrefs.SetInt("PlayerScore", score);
        scoreText.text = "Score: " + score;
        PlayerPrefs.SetString("PlayerName", name);
    }
    public void GetData()
    {
        maxscore = PlayerPrefs.GetInt("PlayerMaxScore");
        maxScoreText.text = "BestScore: " + maxscore;
        string namePlayer = PlayerPrefs.GetString("PlayerName");
        nameText.text = "" + namePlayer;
    }
}
