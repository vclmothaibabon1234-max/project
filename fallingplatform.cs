using System.Collections;
using UnityEngine;

public class fallingplatform : MonoBehaviour
{
    public float falldelay = 1f; //thoi gian trc khi roi
    public float destroydelay = 2f; //thoi gian huy object sau khi roi
    private Rigidbody2D rb;
    private bool isfalling;
    public BoxCollider2D box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kiem tra vat the co phai tag player ko
        if (collision.gameObject.CompareTag("Player"))
        {
            //kiem tra xem player co dung tren platform ko(tranh cham vao tu ben duoi)
            if (collision.contacts[0].normal.y < -0.5f)
            {
                StartCoroutine(fallroutine());
            }

        }
    }
    private IEnumerator fallroutine()
    {
        isfalling = true;
        //cho het thoi gian
        yield return new WaitForSeconds(falldelay);
        if (box != null)
        {
            box.isTrigger = true;//de box collider thanh istrigger va se roi xuyen vat the
        }
        //chuyen rigdibody thanh dynamic the roi xuong
        rb.bodyType = RigidbodyType2D.Dynamic;
        //de roi 1 chut r xoa
        yield return new WaitForSeconds(destroydelay);
        Destroy(gameObject);
    }


}
