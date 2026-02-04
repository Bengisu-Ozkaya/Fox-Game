using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private CrankManager crankManager;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private string axis; // "x" veya "y"

    private int direction;
    private int statusDirection = 1;

    private void Update()
    {
        if (crankManager == null) return;

        if (crankManager.getCrankAxis() == axis)
        {
            direction = crankManager.crankStatus() ? statusDirection : 0;

            if (axis == "x")
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
            }
            else if (axis == "y")
            {
                transform.Translate(0, direction * moveSpeed * Time.deltaTime, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") &&
            !collision.gameObject.CompareTag("Enemy"))
        {
            statusDirection *= -1;
        }
    }
}
