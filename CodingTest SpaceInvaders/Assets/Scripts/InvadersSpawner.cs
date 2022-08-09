
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
}
