using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD_GameManager : MonoBehaviour
{
    [SerializeField] KLD_ScoreManager scoreManager;
    [SerializeField] KLD_Bird bird;
    [SerializeField] Rigidbody2D birdRb;
    [SerializeField] KLD_LevelManager levelManager;
    [SerializeField] GameObject mainMenu;
    [SerializeField] KLD_CandyManager candyManager;

    Vector3 birdStartPos;

    bool gameEnded = true;

    // Start is called before the first frame update
    void Start()
    {
        birdStartPos = bird.transform.position;
        birdRb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (gameEnded)
        {
            gameEnded = false;
            mainMenu.SetActive(false);
            bird.SetNotDead();
            birdRb.bodyType = RigidbodyType2D.Dynamic;
            birdRb.velocity = Vector2.zero;
            scoreManager.ResetScore();
            levelManager.RemoveSpikesAtStart();
        }
    }

    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            mainMenu.SetActive(true);
            birdRb.bodyType = RigidbodyType2D.Static;
            bird.transform.position = birdStartPos;
            candyManager.RemoveCandy();
        }
    }
}
