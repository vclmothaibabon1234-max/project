using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float movespeed = 2f;
    float horizontalinput;
    private SpriteRenderer sp;
    private Animator animator;
    private Rigidbody2D rb;
    public float jump = 3f;
    private bool isjumping = false;
    private bool isgrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalinput = Input.GetAxisRaw("Horizontal");
        
        
        if (Input.GetKey(KeyCode.A))
        {
            sp.flipX = true;
            animator.SetBool("isrunning", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            sp.flipX = false;
            animator.SetBool("isrunning", true);

        }
        else
        {
            animator.SetBool("isrunning", false);
        }
        if (Input.GetButtonDown("Jump") && !isjumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            isjumping = true;
            animator.SetBool("isjumping", true);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalinput * movespeed, rb.linearVelocity.y);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isjumping = false;
        animator.SetBool("isfalling", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("isfalling", false);
    }
}
