using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curd : MonoBehaviour
{
    public RunManager rm;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Oil"))
        {
            rm.HitOil();
            Destroy(gameObject);
        }
    }
}
