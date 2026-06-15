using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public BoxCollider2D trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            respawncontroller.instance.respawnPoint = transform;
            trigger.enabled = false;
        }
    }
}
