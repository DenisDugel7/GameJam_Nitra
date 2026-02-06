using UnityEngine;

public class coin : MonoBehaviour
{
    public GameObject CoffeMachineGameObject;
    AudioSource audiosrc;
    CoffeeMachine CoffeeMachine;

    private void Start()
    {
        CoffeeMachine = CoffeMachineGameObject.GetComponent<CoffeeMachine>();
        audiosrc = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            CoffeeMachine.hasCoin = true;
            if (!audiosrc.isPlaying) audiosrc.Play();
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            if (!audiosrc.isPlaying && !sprite.enabled)Destroy(gameObject);
        } 
    }
}
