using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public GameObject Player;
    PlayerMovement player;
    public bool hasCoin = false;

    void Start()
    {
        player = Player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (hasCoin && collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E)) {
            player.currentSpeed /= 2;
        }
    }
}
