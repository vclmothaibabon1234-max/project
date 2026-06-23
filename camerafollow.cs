using UnityEngine;

public class camerafollow : MonoBehaviour
{
    private Vector3 offset = new Vector3 (0, 0, -10);
    private float smoothtime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
   

    // Update is called once per frame
    void Update()
    {
        Vector3 targetposition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothtime);
    }
}
