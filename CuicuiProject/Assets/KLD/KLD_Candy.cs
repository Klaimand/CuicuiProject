using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD_Candy : MonoBehaviour
{
    [SerializeField] KLD_CandyManager candyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bird"))
        {
            candyManager.TakeCandy();
        }
    }
}
