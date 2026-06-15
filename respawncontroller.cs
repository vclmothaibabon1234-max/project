using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
public class respawncontroller : MonoBehaviour
{
    public static respawncontroller instance;
    public Transform respawnPoint;
    public float spawndelay = 1f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sp;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        
    }
    private void Awake()
    {
        instance = this;
    }

    public void startspawn(GameObject playerobject)
    {
        StartCoroutine(respawnroutine(playerobject));
    }

    public IEnumerator respawnroutine(GameObject playerobject)
    {
        movement playermove = playerobject.GetComponent<movement>();
        Rigidbody2D rb = playerobject.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = playerobject.GetComponent<SpriteRenderer>(); //lay sprite de an nv

        // 1chet thi ko di chuyen dc
        if (playermove != null)
        {
            playermove.enabled = false;
        }
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        //nv bien mat
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; //an hinh anh
        }

        // delay 1 khaong thoi gian r hoi sinh
        yield return new WaitForSeconds(spawndelay);

        // dich chuyen ve diem spawn
        if (respawnPoint != null)
        {
            playerobject.transform.position = respawnPoint.position;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        //nv hien tro lai o vi tri hoi sinh
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; 
        }
        //cho phep di chuyen sau khi hoi sinh
        if (playermove != null)
        {
            playermove.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerDieRoutine(collision.gameObject));

        }
    }
    private IEnumerator PlayerDieRoutine(GameObject playerobject)
    {
        movement playermove = playerobject.GetComponent<movement>();
        Rigidbody2D rb = playerobject.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = playerobject.GetComponent<SpriteRenderer>();

        //ko cho di chuyen vi da chet
        if (playermove != null) playermove.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        //delay 1 giay de hoi sinh
        yield return new WaitForSeconds(spawndelay);
       
    }
}




