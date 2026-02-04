using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private int direction;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float jumpSpeed = 200f;
    private bool doubleJump;

    [SerializeField] private float climbSpeed = 4f;
    private bool canClimb;
    private int climbDirection;

    [SerializeField] private LevelSkipManager levelSkipManager;
    [SerializeField] private int playerHealth = 100;

    private bool isDead = false;
    [SerializeField] private float deathHight = -10f;
    private bool isFalling = false;

    [SerializeField] private CheckpointControl cp;
    [SerializeField] private UIManager uiManager;
    bool inCrank;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        levelSkipManager = GetComponent<LevelSkipManager>();
        cp = GetComponent<CheckpointControl>();
    }

    private void Update()
    {
        // ölüm kontrolü
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            isDead = true;
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.PauseMusic();
        }

        // mapten düşme kontrolü
        if (transform.position.y < deathHight)
        {
            isFalling = true;
            isDead = true;
            if (BackgroundMusic.Instance != null)
                BackgroundMusic.Instance.PauseMusic();
        }

        if (!isDead)
        {
            ButtonMove();
            KeyMove();
        }
    }

    // Buton Hareketleri
    private void ButtonMove()
    {
        if (direction == 1)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false;
            anim.SetBool("isRunning", true);
        }
        else if (direction == -1)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (canClimbing() && climbDirection != 0)
        {
            rb.gravityScale = 0;
            transform.Translate(0, climbDirection * climbSpeed * Time.deltaTime, 0);
            anim.SetBool("isClimbing", true);
        }
        else if (canClimbing())
        {
            rb.gravityScale = 1;
            anim.SetBool("isClimbing", false);
        }
    }

    // Klavye Hareketleri
    public void KeyMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true;
            anim.SetBool("isRunning", true);
        }

        // Zıplama
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jumping();
        }

        // Tırmanma
        if (canClimbing())
        {
            if (Input.GetKey(KeyCode.C))
            {
                rb.gravityScale = 0;
                transform.Translate(0, climbSpeed * Time.deltaTime, 0);
                anim.SetBool("isClimbing", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.gravityScale = 0;
                transform.Translate(0, -climbSpeed * Time.deltaTime, 0);
                anim.SetBool("isClimbing", true);
            }
        }

        // Level geçme
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (levelSkipManager.returnSkipLevel())
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            if (levelSkipManager.returnPreviousLevel())
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    // Zıplama
    public void Jumping()
    {
        if (!doubleJump && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            anim.SetTrigger("jumping");
            doubleJump = true;
        }
        else if (doubleJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            anim.SetTrigger("jumping");
            doubleJump = false;
        }
    }

    // Oyuncu yere değiyorsa zıplama yapması için
    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            0.7f,
            1 << LayerMask.NameToLayer("Ground")
        );

        return hit.collider != null;
    }

    public bool canClimbing()
    {
        return canClimb;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            canClimb = true;
        }

        if (collision.CompareTag("Portal"))
        {
            collision.GetComponent<Portal>().Teleport(gameObject);
        }

        if (collision.CompareTag("Checkpoint"))
        {
            cp.setCheckpointPosition(collision.transform);
        }

        if (collision.CompareTag("Crank"))
        {
            Debug.Log("crank objesindeyim");
            inCrank = true;
            Debug.Log("inCrank: " + inCrank);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stairs"))
        {
            canClimb = false;
            anim.SetBool("isClimbing", false);
            rb.gravityScale = 1;
        }

        if (collision.CompareTag("Crank"))
        {
            Debug.Log("crank objesinden çıktım");
            inCrank = false;
            Debug.Log("inCrank: " + inCrank);
        }
    }

    // Crank durumu
    public bool CrankStat()
    {
        return inCrank;
    }

    //Yön belirleme (yürüme ve tırmanma için)
    public void SetDirection(int dir)
    {
        direction = dir;
    }

    public void SetClimbDirection(int dir)
    {
        climbDirection = dir;
    }

    // karakter canı
    public int GetHealth() => playerHealth;

    public void TakeDamage()
    {
        rb.AddForce(Vector3.up * 250);
        playerHealth -= Random.Range(5, 10);
    }

    public bool isDeadStatus() => isDead;
    public bool isFallingStatus() => isFalling;

    public void restartLevel()
    {
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.ResumeMusic();

        Time.timeScale = 1f;

        if (isFalling)
        {
            isFalling = false;
            transform.position = cp.getCurrentCheckpointPosition().position;
        }

        playerHealth = 100;
        isDead = false;
        StartCoroutine(deathAndLive());
    }

    IEnumerator deathAndLive()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = cp.getCurrentCheckpointPosition().position;
    }
}