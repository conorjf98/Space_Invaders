using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //shoot bullet
        }
    }
}
