using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSkipManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    bool canSkipLevel = false;
    bool canPreviousLevel = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SkipLevel"))
        {
            canSkipLevel = true;
        }

        if (collision.CompareTag("PreviousLevel"))
        {
            canPreviousLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SkipLevel"))
        {
            canSkipLevel = false;
        }

        if (collision.CompareTag("PreviousLevel"))
        {
            canPreviousLevel = false;
        }

    }

    public bool returnSkipLevel()
    {
        return canSkipLevel;
    }

    public bool returnPreviousLevel()
    {
        return canPreviousLevel;
    }
}
