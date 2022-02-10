using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLD_CandyManager : MonoBehaviour
{
    [SerializeField] GameObject candy;

    int candies = 0;

    bool isCandyPlaced = false;

    public void TakeCandy()
    {
        isCandyPlaced = false;
        candies++;
        candy.SetActive(false);
    }

    public void RemoveCandy()
    {
        isCandyPlaced = false;
        candy.SetActive(false);
    }

    public void PlaceCandy(Vector3 position)
    {
        if (!isCandyPlaced)
        {
            candy.transform.position = position;
            candy.SetActive(true);
            isCandyPlaced = true;
        }

    }
}
