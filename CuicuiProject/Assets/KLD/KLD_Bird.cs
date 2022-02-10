using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD_Bird : MonoBehaviour
{

    //refs
    Rigidbody2D rb;
    Transform scaler;
    KLD_LevelManager levelManager;
    KLD_ScoreManager scoreManager;
    KLD_GameManager gameManager;

    //global
    Vector2 velo = Vector2.zero;
    Vector3 scale = Vector3.one;

    //attributes
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpVelo = 5f;
    bool dead = false;

    bool goRight = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Level").GetComponent<KLD_LevelManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<KLD_ScoreManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<KLD_GameManager>();

        scaler = transform.GetChild(0);
        scale.x = 1f;
        scale.y = 1f;
        scale.z = 1f;
        goRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        velo = rb.velocity;

        velo.x = speed * (goRight ? 1f : -1f);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            velo.y = jumpVelo;
        }


        rb.velocity = velo;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            goRight = !goRight;
            scale.x = goRight ? 1f : -1f;
            scaler.localScale = scale;

            scoreManager.AddScore();
            levelManager.BirdTouch(!goRight);
        }
        if (other.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    void Die()
    {
        if (dead)
            return;

        dead = true;
        gameManager.EndGame();
    }

    public void SetNotDead()
    {
        dead = false;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }


}
