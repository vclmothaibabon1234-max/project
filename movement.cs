using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class movement : MonoBehaviour
{
    public float movespeed = 2f;
    float horizontalinput;
    private SpriteRenderer sp;
    private Animator animator;
    private Rigidbody2D rb;
    private float jumpforce = 5f;
    public Vector2 boxsize;
    public float castdistance;
    private float doublejumpforce = 5f;
    private bool candoublejump;

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
        else if (Input.GetKey(KeyCode.D))
        {
            sp.flipX = false;
           
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
        rb.linearVelocity = new Vector2(horizontalinput * movespeed, rb.linearVelocity.y);
    }
}
   



