
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
    [Tooltip("how close an invader can get to the edges")]
    float boundaryOffset;
    Vector3 direction = Vector2.right;

    private void Awake()
    {
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
                Vector3 position = rowPosition;

                //adding horizontal spacing to each invader
                position.x += column * spacing;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Update()
    {
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

            if(direction == Vector3.right && invader.position.x >= rightSide.x - boundaryOffset)
            {
                AdvanceToNextRow();
            } 
            else if (direction == Vector3.left && invader.position.x <= leftSide.x + boundaryOffset)
            {
                AdvanceToNextRow();
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
}
