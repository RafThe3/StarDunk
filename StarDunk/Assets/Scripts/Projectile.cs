using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float force = 1;
    [SerializeField] private float gravity = 1;
    [SerializeField] private AudioClip throwSFX;
    [SerializeField] private bool allowMultiJump = true;
    [SerializeField] private int maxJumps = 3;
    [SerializeField] private float jumpHeight = 1;

    private LineRenderer lr;
    private Rigidbody2D rb;
    private Vector2 lineStartPos;
    private bool canShoot = true;
    private bool canJump = false;
    private int timesJumped = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!canShoot)
        {
            if (canJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && timesJumped < maxJumps)
            {
                Jump();
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lineStartPos = mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 lineEndPos = mousePosition;
            Vector2 velocity = (lineEndPos - lineStartPos) * force;

            const int steps = 500;
            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, velocity, steps);

            lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = (Vector3)trajectory[i];
            }
            lr.SetPositions(positions);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 lineEndPos = mousePosition;
            Vector2 velocity = (lineEndPos - lineStartPos) * force;

            rb.gravityScale = gravity;
            rb.linearVelocity = velocity;
            //Clone(velocity);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(throwSFX);
            canShoot = false;
            lr.enabled = false;
            if (allowMultiJump)
            {
                canJump = true;
            }
        }
    }

    private void Jump()
    {
        rb.linearVelocityY = 0;
        rb.linearVelocityY += jumpHeight * Time.deltaTime;
        if (rb.linearVelocityX > 0)
        {
            rb.linearVelocityX = -rb.linearVelocityX;
        }
        timesJumped++;
    }

    private void Clone(Vector2 velocity)
    {
        GameObject projectileClone = Instantiate(gameObject, (Vector2)gameObject.transform.position + new Vector2(0, 5), Quaternion.identity);
        Rigidbody2D rbClone = projectileClone.GetComponent<Rigidbody2D>();

        projectileClone.tag = "Clone";
        rbClone.gravityScale = gravity;
        rbClone.linearVelocity = velocity;
    }

    private Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Mathf.Pow(timestep, 2) * gravity * Physics2D.gravity;

        float drag = 1 - timestep * rigidbody.linearDamping;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

    public void AllowMultiJump(bool allow)
    {
        allowMultiJump = allow;
    }

    public void CanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
