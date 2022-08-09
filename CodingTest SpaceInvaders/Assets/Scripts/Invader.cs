using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Action killed;
    public int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
