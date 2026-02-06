using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6;
    public float currentSpeed;
    public Vector2 startPosition;
    float horizontal, vertical;
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (Input.GetKey(KeyCode.UpArrow)) vertical = 1;
            else if (Input.GetKey(KeyCode.DownArrow)) vertical = -1;
            else vertical = 0;
            if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1;
            else if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1;
            else horizontal = 0;


        }
        else { 
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        
        }

        this.transform.position = new Vector2(this.transform.position.x + (horizontal * currentSpeed * Time.deltaTime), this.transform.position.y + (vertical * currentSpeed * Time.deltaTime));
    }
}
