using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    [SerializeField]
    float movementClampAmount = 3.0f;

    [SerializeField]
    Bullet bulletPrefab;
    bool bulletActive;
    bool canMove, paused;
    Vector3 offset = new Vector3(0.0f, 0.3f, 0.0f);
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
        canMove = (state == GameState.Playing);
        paused = (state == GameState.Paused);
        Debug.Log(state +", "+ canMove);
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += this.speed * Time.deltaTime * Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += this.speed * Time.deltaTime * Vector3.right;
            }

            // initially, the temporary vector should equal the player's position
            Vector3 clampedPosition = transform.position;
            // Now we can manipulte it to clamp the y element
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -movementClampAmount, movementClampAmount);
            // re-assigning the transform's position will clamp it
            this.transform.position = clampedPosition;

            if (Input.GetKeyDown(KeyCode.Space) && !bulletActive)
            {
                ShootBullet();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.gManager.UpdateGameState(GameState.Paused);
            }
        }
        else if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.gManager.UpdateGameState(GameState.Playing);
            }
        }
        
    }

    private void ShootBullet()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position + offset, Quaternion.identity);
        bullet.bulletDestroyed += BulletDestroyed;
        bulletActive = true;
    }

    private void BulletDestroyed()
    {
        bulletActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            GameManager.gManager.UpdateGameState(GameState.Lose);
        }
    }
}
