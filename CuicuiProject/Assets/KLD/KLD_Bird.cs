using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD_Bird : MonoBehaviour
{

    //refs
    Rigidbody2D rb;
    Transform scaler;

    //global
    Vector2 velo = Vector2.zero;
    Vector3 scale = Vector3.one;

    //attributes
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpVelo = 5f;

    bool goLeft = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        scaler = transform.GetChild(0);
        scale.x = 1f;
        scale.y = 1f;
        scale.z = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        velo = rb.velocity;

        velo.x = speed * (goLeft ? 1f : -1f);



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
            goLeft = !goLeft;
            scale.x = goLeft ? 1f : -1f;
            scaler.localScale = scale;
            print("p");
        }
    }
}
