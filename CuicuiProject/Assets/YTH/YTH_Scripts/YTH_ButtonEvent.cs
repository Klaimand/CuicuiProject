using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YTH_ButtonEvent : MonoBehaviour
{

    [SerializeField] UnityEvent onBuy;

    public void OnBuy()
    {
        onBuy.Invoke();
    }

}
