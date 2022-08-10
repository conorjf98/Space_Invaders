using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Action bulletDestroyed;
    bool playing= true;
    private void Update()
    {
        if (playing)
        {
            this.transform.position += this.direction * this.speed * Time.deltaTime;
        }
        
    }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        playing = (state == GameState.Playing);
        if(state == GameState.Lose || state == GameState.Win)
        {
            if (this.bulletDestroyed != null)
            {
                this.bulletDestroyed.Invoke();
            }
            Destroy(this.gameObject);
        }
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
