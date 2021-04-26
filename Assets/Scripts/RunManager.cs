using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    internal void HitOil()
    {
        WinText.SetActive(true);
        isStarted = false;
        GetComponent<AudioSource>().Play();
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
