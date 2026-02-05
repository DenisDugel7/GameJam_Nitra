using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6;
    public float currentSpeed;
    public Vector2 startPosition;
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        this.transform.position = new Vector2(this.transform.position.x + (horizontal * currentSpeed * Time.deltaTime), this.transform.position.y + (vertical * currentSpeed * Time.deltaTime));
    }
}
