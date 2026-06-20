using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndShoot : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    public TrajLine tl;

    private Camera cam;

    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        // left mouse button pressed
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            startPoint.z = 0f;
        } 

        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            currentPoint.z = 0f;
            tl.RenderLine(startPoint, currentPoint);
        }

        // left mouse button released
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            endPoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            endPoint.z = 0f;

            force = new Vector2(
                Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y)
            );

            rb.AddForce(force * power, ForceMode2D.Impulse);
            tl.EndLine();
        }
    }
}