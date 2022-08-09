using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    [SerializeField]
    float movementClampAmount = 3.0f;
    private void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //shoot bullet
        }
    }
}
