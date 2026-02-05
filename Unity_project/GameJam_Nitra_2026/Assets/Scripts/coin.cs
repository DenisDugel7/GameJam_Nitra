using UnityEngine;

public class coin : MonoBehaviour
{
    public GameObject CoffeMachineGameObject;
    CoffeeMachine CoffeeMachine;

    private void Start()
    {
        CoffeeMachine = CoffeMachineGameObject.GetComponent<CoffeeMachine>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            CoffeeMachine.hasCoin = true;
            Destroy(gameObject);
        } 
    }
}
