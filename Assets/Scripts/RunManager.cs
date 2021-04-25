using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public float timeOut;
    float time;
    bool isStarted = false;
    public GameObject RetryText;
    public GameObject WinText;

    public void StartTime()
    {
        time = timeOut;
        isStarted = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                Retry();
            }
        }
    }

    internal void HitOil()
    {
        WinText.SetActive(true);
        isStarted = false;
    }

    private void Retry()
    {
        RetryText.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Curd"))
        {
            if (isStarted)
            {
                Retry();
            }
        }
    }
}
