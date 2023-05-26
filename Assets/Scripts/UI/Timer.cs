using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText;

    private float startTime = 60f;
    private float currentTime;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            Debug.Log("End game");
        }
    }
}
