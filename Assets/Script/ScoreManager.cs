using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    public Text scoreTXT;
    private void Awake()
    {
        makeSingle();
        scoreTXT = GameObject.Find("ScoreTXT").GetComponent<Text>();
    }
    void Start()
    {
        addScore(0);
    }
    void makeSingle()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }


    void Update()
    {
        if (scoreTXT == null)
        {
            scoreTXT = GameObject.Find("ScoreTXT").GetComponent<Text>();
        }
    }
    public void addScore(int value)
    {
        score += value;
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        scoreTXT.text = ""+score;
    }
    public void resetScore()
    {
        score = 0;
    }
    public int getScore()
    {
        return score;
    }
}
