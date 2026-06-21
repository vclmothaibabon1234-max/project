using UnityEngine;

public class smoothchange : MonoBehaviour
{
    [Header("Xu hướng dịch chuyển (Chọn 1 cái bằng 1 hoặc -1)")]
    public float moveX = 0f; // Đi qua phải thì điền 1, đi qua trái điền -1
    public float moveY = 0f; // Đi lên trên thì điền 1, đi xuống dưới điền -1

    [Header("Kích thước của một phòng")]
    public float roomWidth = 16f;  // Chiều rộng phòng (mặc định camera 2D thường là 16 hoặc 18)
    public float roomHeight = 10f; // Chiều cao phòng (mặc định thường là 10)

    [Header("Độ đẩy Player vào phòng mới")]
    public float playerPushDistance = 2.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelchange controller = Camera.main.GetComponent<levelchange>();

            if (controller != null && !controller.IsPanning())
            {
                // Tự tính toán vị trí phòng tiếp theo dựa trên vị trí camera hiện tại
                Vector2 currentCamPos = Camera.main.transform.position;
                Vector2 newCamPos = new Vector2(
                    currentCamPos.x + (moveX * roomWidth),
                    currentCamPos.y + (moveY * roomHeight)
                );

                // Ra lệnh cho camera lướt đi
                controller.movetonewroom(newCamPos);

                // Đẩy Player lên một khoảng để sang hẳn màn mới, không bị dính trigger cũ
                Vector3 pushVector = new Vector3(moveX, moveY, 0) * playerPushDistance;
                collision.transform.position += pushVector;
            }
        }
    }
}
