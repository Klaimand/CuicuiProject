using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    float shakeTimeRemaining;
    float shakePower;
    float shakeFadeTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShake(0.5f, 0.5f);
        }
    }
    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }   
    }

    public void StartShake(float lenght, float power)
    {
        shakeTimeRemaining = lenght;
        shakePower = power;
        shakeFadeTime = power / lenght;

    }

}
