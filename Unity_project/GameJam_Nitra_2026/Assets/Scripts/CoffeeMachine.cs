using TMPro;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public GameObject Player;
    PlayerMovement player;
    public bool hasCoin = false;
    public bool trigger = false;
    bool slowed = false;

    void Start()
    {
        player = Player.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (trigger && hasCoin && !slowed)
        {
            GameObject interacts = GameObject.Find("Interacts");
            TextMeshProUGUI interacts_text = interacts.GetComponent<TextMeshProUGUI>();
            interacts_text.text = "Press E to buy a coffee";
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.currentSpeed /= 2;
                slowed = true;
            }
        }
        else {
            if (!GameObject.Find("Terms_and_conditions").GetComponent<TAC>().trigger) {
                GameObject interacts = GameObject.Find("Interacts");
                TextMeshProUGUI interacts_text = interacts.GetComponent<TextMeshProUGUI>();
                interacts_text.text = " ";
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") trigger = false;
    }
}
