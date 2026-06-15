using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public BoxCollider2D trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //luu toa do
           sencecontroller.instance.lastCheckpointPosition = transform.position;
            sencecontroller.instance.hasTouchedCheckpoint = true;
            Debug.Log("Đã lưu vị trí checkpoint!");
        }
    }
}
