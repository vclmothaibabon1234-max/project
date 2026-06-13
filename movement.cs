using UnityEngine;



public class movement : MonoBehaviour
{
    public float movespeed = 2f;
    float horizontalinput;
    private SpriteRenderer sp;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isjumping = false;
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

        }
        if (Input.GetKey(KeyCode.D))
        {
            sp.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
            isjumping = true;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalinput * movespeed, rb.linearVelocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isjumping = false;
}
}



