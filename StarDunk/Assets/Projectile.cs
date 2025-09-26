using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float force = 1;

    private LineRenderer lr;
    private Rigidbody2D rb;
    private Vector2 lineStartPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            lineStartPos = mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 lineEndPos = mousePosition;
            Vector2 velocity = (lineStartPos - lineEndPos) * force;

            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, velocity, 500);

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
            Vector2 velocity = (lineStartPos - lineEndPos) * force;

            rb.linearVelocity = velocity;
        }
    }

    private Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Mathf.Pow(timestep, 2) * rigidbody.gravityScale * Physics2D.gravity;

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
}
