using UnityEngine;

public class jumpad : MonoBehaviour
{
    public float power = 8f;
    private Animator Animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * power, ForceMode2D.Impulse);
            Animator = GetComponent<Animator>();
            Animator.SetBool("iscolliding", true);
        } 
        
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Animator.SetBool("iscolliding", false);
        }
    }
    }

