using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6;
    public float currentSpeed;
    public Vector2 startPosition;

    float horizontal, vertical;

    [Header("Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    [Header("Audio")]
    public AudioClip walking;

    SpriteRenderer sr;
    AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;

        sr = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walking;
        audioSource.loop = true;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            vertical = Input.GetKey(KeyCode.UpArrow) ? 1 :
                       Input.GetKey(KeyCode.DownArrow) ? -1 : 0;

            horizontal = Input.GetKey(KeyCode.RightArrow) ? 1 :
                         Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        transform.position += new Vector3(
            horizontal * currentSpeed * Time.deltaTime,
            vertical * currentSpeed * Time.deltaTime
        );

        UpdateSprite();
        HandleWalkingSound();
    }

    void HandleWalkingSound()
    {
        bool isMoving = horizontal != 0 || vertical != 0;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    void UpdateSprite()
    {
        if (vertical > 0)
            sr.sprite = upSprite;
        else if (vertical < 0)
            sr.sprite = downSprite;
        else if (horizontal > 0)
            sr.sprite = rightSprite;
        else if (horizontal < 0)
            sr.sprite = leftSprite;
    }
}
