using System.Collections;
using UnityEngine;

public class respawncontroller : MonoBehaviour
{
    public static respawncontroller instance;
    public Transform respawnPoint;
    public float spawndelay = 1f;
    public GameObject dead;
    private void Awake()
    {
        instance = this;
    }
    public void startspawn( GameObject playerobject)
    {
        StartCoroutine(respawnroutine(playerobject));
    }

    public IEnumerator  respawnroutine(GameObject playerobject)
    {
        movement playermove = playerobject.GetComponent<movement>();
        Rigidbody2D rb = playerobject.GetComponent<Rigidbody2D>();
        Animator animator = playerobject.GetComponent<Animator>();
        if (playermove != null)
        {
            playermove.enabled = false;

        }
        if (playermove != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
     
        yield return new WaitForSeconds(spawndelay); // doi 1 khoang delay ngan de nv spawn

        if (respawnPoint != null)
        {
            playerobject.transform.position = respawnPoint.position;
        }

        if (dead != null)
        {
            GameObject effect = Instantiate(dead, playerobject.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (playermove != null)
        {
            playermove.enabled = true;
        }
        Debug.Log("Đã hồi sinh Player sau thời gian delay!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            startspawn(collision.gameObject);
            
        }
    }
    }

    

