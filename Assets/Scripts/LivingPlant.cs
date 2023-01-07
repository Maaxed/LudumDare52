using UnityEngine;

public class LivingPlant : MonoBehaviour
{
    public Collider2D plantCollider;
    public Rigidbody2D plantRigidbody;
    public FixedJoint2D joint;
    public float speed = 1.0f;

    private bool grabbed = false;
    private int direction = -1;
    private float nextChoiceTime = -1.0f;

    private static Vector2[] moveDirections =
    {
        new Vector2(2, 1).normalized,
        new Vector2(2, -1).normalized,
        new Vector2(-2, 1).normalized,
        new Vector2(-2, -1).normalized,
    };

    private void Awake()
    {
        if (plantRigidbody == null)
        {
            plantRigidbody = GetComponentInChildren<Rigidbody2D>();
        }
        if (plantCollider == null)
        {
            plantCollider = GetComponentInChildren<Collider2D>();
        }
        if (joint == null)
        {
            joint = GetComponentInChildren<FixedJoint2D>();
        }
        nextChoiceTime = Time.time + Random.Range(0.5f, 2.0f);
    }

    private void Update()
    {
        if (grabbed)
            return;

        if (Time.time >= nextChoiceTime)
        {
            ChooseAction();
        }

        if (direction >= 0)
        {
            plantRigidbody.velocity = moveDirections[direction] * speed;

        }
        else
        {
            plantRigidbody.velocity = Vector2.zero;
        }
    }

    private void ChooseAction()
    {
        if (direction < 0)
        {
            direction = Random.Range(0, moveDirections.Length);
            nextChoiceTime = Time.time + Random.Range(2.0f, 4.0f);
        }
        else
        {
            direction = -1;
            nextChoiceTime = Time.time + Random.Range(2.0f, 4.0f);
        }
    }

    public void Grab(MouseObject mouse)
    {
        grabbed = true;
        joint.connectedBody = mouse.GetComponent<Rigidbody2D>();
        joint.enabled = true;
        plantCollider.enabled = false;
    }

    public void Ungrab()
    {
        joint.enabled = false;
        joint.connectedBody = null;
        plantCollider.enabled = true;
        grabbed = false;

        direction = -1;
        nextChoiceTime = Time.time + Random.Range(0.5f, 2.0f);
    }
}
