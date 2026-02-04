using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    int direction = -1; // 1 -> sað, -1 -> sol

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        gameObject.transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if (direction == 1)
            {
                direction = -1;
                spriteRenderer.flipX = false;
            }
            else
            {
                direction = 1;
                spriteRenderer.flipX = true;
            }
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().TakeDamage();
        }
    }
}
