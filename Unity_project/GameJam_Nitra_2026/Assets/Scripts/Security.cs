using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityAI : MonoBehaviour
{
    enum State { Idle, Chase, Return }
    State currentState = State.Idle;

    public float speed = 3f;

    [Header("Idle Movement")]
    public float idleDistance = 4f;

    [Header("Vision")]
    public float viewRadius = 8f;
    public float viewAngle = 60f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    Rigidbody2D rb;
    Transform player;

    Vector2 startPosition;
    Vector2 idleTop;
    Vector2 idleBottom;
    bool movingUp = true;
    bool isDeath;

    AudioSource audiosrc;
    public AudioClip fail;

    void Start()
    {
        isDeath = false;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audiosrc = player.GetComponent<AudioSource>();

        startPosition = rb.position;
        idleTop = startPosition + Vector2.up * idleDistance;
        idleBottom = startPosition - Vector2.up * idleDistance;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleMove();
                if (CanSeePlayer())
                    currentState = State.Chase;
                break;

            case State.Chase:
                ChasePlayer();
                if (!CanSeePlayer())
                    currentState = State.Return;
                break;

            case State.Return:
                ReturnToStart();
                if (Vector2.Distance(rb.position, startPosition) < 0.2f)
                    currentState = State.Idle;
                break;
        }
        if (!audiosrc.isPlaying && !isDeath) audiosrc.Play();
    }

    void IdleMove()
    {
        Vector2 target = movingUp ? idleTop : idleBottom;

        rb.MovePosition(Vector2.MoveTowards(
            rb.position,
            target,
            speed * Time.fixedDeltaTime
        ));

        RotateTowards(target);

        if (Vector2.Distance(rb.position, target) < 0.05f)
            movingUp = !movingUp;
    }

    void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        RotateTowards(player.position);
    }

    void ReturnToStart()
    {
        rb.MovePosition(Vector2.MoveTowards(
            rb.position,
            startPosition,
            speed * Time.fixedDeltaTime
        ));

        RotateTowards(startPosition);
    }

    bool CanSeePlayer()
    {
        Vector2 dirToPlayer = (player.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, player.position) > viewRadius)
            return false;

        float angle = Vector2.Angle(transform.right, dirToPlayer);
        if (angle > viewAngle / 2f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dirToPlayer,
            viewRadius,
            obstacleMask | playerMask
        );

        return hit && hit.collider.CompareTag("Player");
    }

    void RotateTowards(Vector2 target)
    {
        Vector2 dir = target - rb.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isDeath)
            {
                isDeath = true;
                audiosrc.clip = fail;
                audiosrc.Play();
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Rigidbody2D rb_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
                rb_player.constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(RestartScene());
            }
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(audiosrc.clip.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
