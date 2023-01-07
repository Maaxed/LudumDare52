using UnityEngine;

public class MouseObject : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D mouseRigidbody2D;

    private void Awake()
    {
        if (mouseRigidbody2D == null)
            mouseRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mouseRigidbody2D.MovePosition(cam.ScreenToWorldPoint(Input.mousePosition));
    }
}
