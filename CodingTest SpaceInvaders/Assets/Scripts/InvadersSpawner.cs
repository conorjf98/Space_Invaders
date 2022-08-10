using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersSpawner : MonoBehaviour
{
    [SerializeField]
    //different variants of invader (narrow, wide..)
    GameObject[] invaderTypes;
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
    float timeBetweenMovements;
    [SerializeField]
    float timeBetweenShots;
    Vector3 direction = Vector2.right;
    bool canChangeDirection = true;
    bool playing = false;
    public int invadersKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public int amountAlive => totalInvaders - invadersKilled;
    float movementTimer = 0;
    float fireTimer = 0;
    [SerializeField]
    Bullet enemyBullet;
    [SerializeField]
    GameObject[] bunkers;


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
    }

    public void SpawnInvaders()
    {
        
        int currentLevel = GameManager.gManager.currentLevel;
        timeBetweenMovements = LevelManager.lManager.levelList.level[currentLevel].timeBetweenMovements;
        timeBetweenShots = LevelManager.lManager.levelList.level[currentLevel].timeBetweenShots;
        ResetInvaders();
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
                invader.GetComponent<Invader>().killed += InvaderKilled;
                invader.GetComponent<Invader>().touchedBoundary += TouchedBoundary;
                Vector3 position = rowPosition;

                //adding horizontal spacing to each invader
                position.x += column * spacing;
                invader.transform.localPosition = position;
            }
        }
        GameManager.gManager.UpdateGameState(GameState.Playing);
    }

    public void ResetInvaders()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //regenerate all bunkers
        foreach (GameObject bunker in this.bunkers)
        {
            foreach (Transform child in bunker.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        this.transform.position = new Vector3(0.0f, 2.5f, 0.0f);
        invadersKilled = 0;
    }

    private void Update()
    {
        if (playing)
        {
            movementTimer += Time.deltaTime;
            fireTimer += Time.deltaTime;
            if (movementTimer > timeBetweenMovements)
            {
                canChangeDirection = true;
                this.transform.position += this.direction * this.speed * Time.deltaTime;
                movementTimer = 0;
            }

            if (fireTimer > timeBetweenShots)
            {
                fireBullet();
                fireTimer = 0;
            }
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
            GameManager.gManager.UpdateGameState(GameState.Win);
        }
    }

    private void TouchedBoundary()
    {
        if (canChangeDirection)
        {
            AdvanceToNextRow();
            canChangeDirection = false;
        }
        
    }

    private void fireBullet()
    {
        if (playing)
        {
            foreach (Transform invader in this.transform)
            {
                //if the invader has been killed then ignore it
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }
                //in the case of there being 55 enemies, theres a 1 in 55 chance for each to fire a bullet. chances increase as the enemies are killed off
                if (Random.value < (1.0f / (float)this.amountAlive))
                {
                    Instantiate(this.enemyBullet, invader.position, Quaternion.identity);
                    //break forces 1 bullet to be fired at once
                    break;
                }

            }
        } 
    }
}
