using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Action killed;
    public Action touchedBoundary;
    public int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
            ScoreManager.sManager.AddScore(score);
            //freeze time for a 10th of a second to add impact to the shot
            TimeFreeze.tf.Stop(0.1f);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            this.touchedBoundary.Invoke();
        }
    }
}
