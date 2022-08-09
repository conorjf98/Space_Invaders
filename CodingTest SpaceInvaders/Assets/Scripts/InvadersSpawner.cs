using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersSpawner : MonoBehaviour
{
    [SerializeField]
    //different variants of invader (narrow, wide..)
    GameObject[] invaderTypes;
    List<GameObject> invaders = new List<GameObject>();
    [SerializeField]
    int rows;
    [SerializeField]
    int columns;
    [SerializeField]
    float spacing;
    [SerializeField]
    float speed;
    [SerializeField]
    [Tooltip("how far the invaders drop down when hitting a side of the screen")]
    float dropAmount;
    [SerializeField]
    [Tooltip("how close an invader can get to the edges")]
    float boundaryOffset;
    [SerializeField]
    float timeBetweenMovements;
    [SerializeField]
    float fireRate;
    Vector3 direction = Vector2.right;
    public int invadersKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public int amountAlive => totalInvaders - invadersKilled;
    float timer = 0;
    [SerializeField]
    Bullet enemyBullet;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        for (int row = 0; row < this.rows; row++)
        {
            //calculating the width and the height given the spacing values in the unity editor to center the invaders
            float width = spacing * (this.columns - 1);
            float height = spacing * (this.rows - 1);
            //half the width and height gives a center point
            Vector2 centerpoint = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centerpoint.x, centerpoint.y - (row * spacing), 0.0f);

            for (int column = 0; column < this.columns; column++)
            {
                
                GameObject invader = Instantiate(this.invaderTypes[row], this.transform);
                invaders.Add(invader);
                invader.GetComponent<Invader>().killed += InvaderKilled;
                Vector3 position = rowPosition;

                //adding horizontal spacing to each invader
                position.x += column * spacing;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(fireBullet), this.fireRate, this.fireRate);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBetweenMovements)
        {
            Debug.Log("Entered timer");

            this.transform.position += this.direction * this.speed * Time.deltaTime;
            Vector3 leftSide = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightSide = Camera.main.ViewportToWorldPoint(Vector3.right);
            foreach (Transform invader in this.transform)
            {
                //if the invader has been killed then ignore it
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (direction == Vector3.right && invader.position.x >= rightSide.x - boundaryOffset)
                {
                    AdvanceToNextRow();
                }
                else if (direction == Vector3.left && invader.position.x <= leftSide.x + boundaryOffset)
                {
                    AdvanceToNextRow();
                }

            }
            timer = 0;
        }
        
    }

    private void AdvanceToNextRow()
    {
        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= dropAmount;
        this.transform.position = position;
    }
    private void InvaderKilled()
    {
        this.invadersKilled++;

        if(this.invadersKilled >= this.totalInvaders)
        {
            //win
        }
    }

    private void fireBullet()
    {
        foreach (Transform invader in this.transform)
        {
            //if the invader has been killed then ignore it
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            //in the case of there being 55 enemies, theres a 1 in 55 chance for each to fire a bullet. chances increase as the enemies are killed off
            if(Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.enemyBullet, invader.position, Quaternion.identity);
                //break forces 1 bullet to be fired at once
                break;
            }

        }
    }
}
