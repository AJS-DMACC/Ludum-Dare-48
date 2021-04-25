using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Sprite friedSprite;
    public SpriteRenderer foodRenderer;
    public GameObject friedParticles;
    public float GrowTimes;
    bool isFried;

    private void Start()
    {
        isFried = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Oil"))
        {
            if (!isFried)
            {
                foodRenderer.sprite = friedSprite;
                Instantiate(friedParticles, transform);
                isFried = true;
            }
            else
            {
                transform.localScale *= GrowTimes;
            }
        }
    }
}
