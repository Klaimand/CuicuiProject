using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KLD_ScoreManager : MonoBehaviour
{
    [SerializeField] KLD_LevelManager levelManager;
    [SerializeField] KLD_Bird bird;

    [SerializeField] ScoreLevel[] levels;

    [SerializeField, Header("Refs")] Text scoretext;
    [SerializeField] Camera cam;
    [SerializeField] SpriteRenderer[] levelRenderers;
    [SerializeField] SpriteRenderer scoreCircle;

    ScoreLevel curlevel;

    int score = 0;

    void Start()
    {
        CheckLevelChange();
    }

    public void AddScore()
    {
        score++;
        scoretext.text = score.ToString("00");
        CheckLevelChange();
    }

    void CheckLevelChange()
    {
        for (int i = 0; i < levels.GetLength(0); i++)
        {
            if (levels[i].startAt == score)
            {
                LoadLevel(levels[i]);
                break;
            }
        }
    }

    void LoadLevel(ScoreLevel _level)
    {
        foreach (var item in levelRenderers)
        {
            item.color = _level.levelColor;
        }
        scoretext.color = _level.scoreColor;

        cam.backgroundColor = _level.backgroundColor;

        scoreCircle.color = _level.scoreCircleColor;


        levelManager.SetSpikeNumber(_level.nbSpikes);
        bird.SetSpeed(_level.birdSpeed);


        curlevel = _level;

    }

}

[System.Serializable]
public class ScoreLevel
{
    public int startAt = 0;

    [Header("Difficulty")]
    public float birdSpeed = 4f;
    public int nbSpikes = 4;

    [Header("Colors")]
    public Color levelColor = Color.white * 0.32f;
    public Color backgroundColor = Color.white * 0.556f;
    public Color scoreColor = Color.white * 0.32f;
    public Color scoreCircleColor = Color.white * 0.32f;
}