using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Action bulletDestroyed;
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(this.bulletDestroyed != null)
        {
            this.bulletDestroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
