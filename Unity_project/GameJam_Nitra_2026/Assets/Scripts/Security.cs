using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityAI : MonoBehaviour
{
    enum State { Idle, Chase, Return }
    State currentState = State.Idle;

    public float speed = 3f;
    public float idleRange = 2f;

    [Header("Vision")]
    public float viewRadius = 8f;
    public float viewAngle = 60f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    Rigidbody2D rb;
    Transform player;

    Vector2 startPosition;
    Vector2 topPoint;
    Vector2 bottomPoint;
    bool movingUp = true;

    Vector2 idleTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = rb.position;
        topPoint = startPosition + Vector2.up * 10f;
        bottomPoint = startPosition - Vector2.up * 10f;

    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleMove();
                if (CanSeePlayer()) currentState = State.Chase;
                break;

            case State.Chase:
                ChasePlayer();
                if (!CanSeePlayer()) currentState = State.Return;
                break;

            case State.Return:
                ReturnToStart();
                if (Vector2.Distance(rb.position, startPosition) < 0.2f)
                    currentState = State.Idle;
                break;
        }
    }

    void IdleMove()
    {
        Vector2 target = movingUp ? topPoint : bottomPoint;

        rb.MovePosition(Vector2.MoveTowards(
            rb.position,
            target,
            speed * Time.fixedDeltaTime
        ));

        RotateIdle(target);

        if (Vector2.Distance(rb.position, target) < 0.05f)
            movingUp = !movingUp;
    }
    void RotateIdle(Vector2 target)
    {
        Vector2 dir = target - rb.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }


    void PickNewIdleTarget()
    {
        float offsetY = Random.Range(-idleRange, idleRange);
        idleTarget = startPosition + Vector2.up * offsetY;
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
        if (collision.gameObject.tag == "Player") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
