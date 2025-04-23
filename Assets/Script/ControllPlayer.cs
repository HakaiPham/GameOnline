using UnityEngine;
using Fusion;

public class ControllPlayer : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    float moveInput;
    public int score = 0;
    public GameObject panel;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private bool isGrounded;

    Animator animator;

    GameDataPref gameDataManager;
    PlayerControlll2 propertiveControll;

    void Start()
    {
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        gameDataManager = FindObjectOfType<GameDataPref>();
        propertiveControll = GetComponent<PlayerControlll2>();
        panel = GameObject.Find("PanelScore");
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;
        Move();
        Jump();
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            panel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            panel.SetActive(true);
        }
    }

    void Move()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput != 0)
        {
            if(animator!= null) animator.SetBool("Run", true);
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            if (animator != null)  animator.SetBool("Run", false);
        }
        // Flip sprite based on movement direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.5f, groundLayer);
        Debug.Log("isGrounded: " + isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (animator != null) animator.SetTrigger("Jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else
        {
            if (animator != null) animator.SetTrigger("Fall");
            if (isGrounded && moveInput == 0)
            {
                if (animator != null) animator.SetTrigger("Idle");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce+2f);
        }
        if (collision.gameObject.tag == "Fan")
        {
            rb.linearVelocity += new Vector2(rb.linearVelocity.x, jumpForce);
        }
        if (collision.gameObject.tag == "Fruit")
        {
            score++;
            gameDataManager.SetData(score);
        }
        if (collision.gameObject.tag == "Trap")
        {
            animator.SetTrigger("Hit");
            rb.freezeRotation = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce + 3f);
            circleCollider.enabled = false;
        }
    }
}
