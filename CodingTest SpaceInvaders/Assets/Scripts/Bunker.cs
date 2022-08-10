using UnityEngine;

public class Bunker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.gManager.UpdateGameState(GameState.Lose);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
