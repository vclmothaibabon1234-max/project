using UnityEngine;

public class accelaration : MonoBehaviour
{

    public Transform[] waypoints;      
    public float reachDistance = 0.2f; 

    public float acceleration = 3f;    
    public float maxSpeed = 5f;       

    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f;

    private Animator animator;
    void Update()
    {
        if (waypoints.Length == 0) return;

        Patrol();
    }

    void Patrol()
    {
        //lay toa do tuan tra hien tai
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 1.tinh huong cua quai vat den diem tuan tra
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // 2. tang toc theo thoi gian
        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // ko co vuot qua max speed

        // 3.thuc hien di chuyen quai vat
        transform.Translate(direction * currentSpeed * Time.deltaTime);

        // 4.kiem tra den diem dc danh dau chua
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= reachDistance)
        {
            // chuyen qua diem moi
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // den diem moi thi toc do ve 0
            currentSpeed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            animator = GetComponent<Animator>();
            animator.SetBool("hit", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator = GetComponent<Animator>();
        animator.SetBool("hit", false);
    }
}
