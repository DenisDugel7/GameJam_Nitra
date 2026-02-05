using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Has collided with smtg");
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Has collided with Player");
            PlayerMovement playerClass = player.GetComponent<PlayerMovement>();
            player.transform.position = playerClass.startPosition;
        }
    }

}
