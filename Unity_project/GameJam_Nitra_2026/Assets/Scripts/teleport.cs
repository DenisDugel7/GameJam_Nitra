using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject player;
    public AudioClip fail;
    public AudioSource audiosrc;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        audiosrc.clip = fail;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerMovement playerClass = player.GetComponent<PlayerMovement>();
            player.transform.position = playerClass.startPosition;
            audiosrc.Play();

        }
    }

}
