using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
public class movement : MonoBehaviour
{
    public float movespeed = 10f;
    float horizontalinput;
    private SpriteRenderer sp;
    private Animator animator;
    private Rigidbody2D rb;
    private float jumpforce = 5f;
    public Vector2 boxsize;
    public float castdistance;
    private float doublejumpforce = 5f;
    private bool candoublejump;

    //wall jump
    public Transform groundcheck;
    public LayerMask groundlayer;
    public Transform wallcheck;
    public Transform wallcheck2;
    public LayerMask walllayer;
    private bool iswallsliding;
    private float wallslidingspeed = 2f;
    private bool iswalljumping;
    private float walljumpingdirection;
    private float walljumpingtime = 0.2f;
    private float walljumpingcounter;
    private float walljumpingduration;
    private Vector2 walljumpingpower = new Vector2(8f, 5f);
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
            //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool("isrunning", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            sp.flipX = false;
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool("isrunning", true);
        }
        else
        {
            animator.SetBool("isrunning", false);
        }

        //neu an space va isgrounded = true thi thuc hien nhay
        if (Input.GetButtonDown("Jump") && isgrounded())
        {
            jump(jumpforce);
            animator.SetBool("isjumping", true);
            animator.SetBool("isdoublejumping", false);

        }
        //neu nhu nut space da dc an, isgrounded = false va candoublejump = true
        //thuc hien double jump
        else if (Input.GetButtonDown("Jump") && !isgrounded() && candoublejump)
        {
            rb.linearVelocity = new Vector2(0, doublejumpforce);
            //jump(doublejumpforce);
            candoublejump = false;
            animator.SetBool("isjumping", false);
            animator.SetBool("isdoublejumping", true);
        }
        wallslide();
        walljumping();
        animator.SetBool("iswallsliding", iswallsliding);
        
    }
    private bool iswalled()
    {
        return Physics2D.OverlapCircle(wallcheck.position, 0.1f, walllayer);
        
    }
    private bool iswalled2()
    {
        return Physics2D.OverlapCircle(wallcheck2.position,0.1f, walllayer);
    }
    private void wallslide()
    {
        if (iswalled() && !isgrounded() && horizontalinput != 0f)
        {
            iswallsliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallslidingspeed, float.MaxValue));
        }
        else
        {
            iswallsliding = false;
        }
        if (iswalled2() && !isgrounded() && horizontalinput != 0f)
        {
            iswallsliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallslidingspeed, float.MaxValue));
        }
    }
    private void walljumping()
    {
        if (iswallsliding)
        {
            iswalljumping = false;
            walljumpingdirection = -transform.localScale.x;
            walljumpingcounter = walljumpingtime;
            CancelInvoke(nameof(stopwalljumping));

        }
        else
        {
            walljumpingcounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && walljumpingcounter > 0f)
        {
            iswalljumping = true;
            rb.linearVelocity = new Vector2(walljumpingdirection * walljumpingpower.x, walljumpingpower.y);
            walljumpingcounter = 0f;
            
            Invoke(nameof(stopwalljumping), walljumpingduration);
        }
    }
    private void stopwalljumping()
    {
        iswalljumping = false;
    }
    private void jump(float force)
    {
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

    }
    //neu nhu nv va cham ===> co the double jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isgrounded();
        animator.SetBool("isdoublejumping", false);
        animator.SetBool("isjumping", false);
    }
    public bool isgrounded()
    {
        //dung boxcast de check xem player co cham vao mat dat ko
        bool hit = Physics2D.BoxCast(transform.position, boxsize, 0, -transform.up, castdistance, LayerMask.GetMask("ground"));
        //neu nhu boxcast xac dinh nv da hoac dang cham vao mat dat ==> candoublejump = true
        if (hit)
        {
            candoublejump = true;
        }
        return hit;
    }
    //check boxcast
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position - transform.up * castdistance, boxsize);
    }

    private void FixedUpdate()
    {
        if (!iswalljumping)
        {
            rb.linearVelocity = new Vector2(horizontalinput * movespeed, rb.linearVelocity.y);
        }
    }
}
