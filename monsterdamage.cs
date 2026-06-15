using UnityEngine;
using UnityEngine.SceneManagement;

public class monsterdamage : MonoBehaviour
{

    public movement playermovement;
    public GameObject dead; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
 

        //kiem tra va cham
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (dead != null)
            {
                //tranh bi loi tang hinh
                Vector3 spawnPos = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
                //xuat hien hieu ung
                GameObject effect = Instantiate(dead, spawnPos, Quaternion.identity);
                Destroy(effect, 1f); // bien mat sau 1s
            }
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 3. Ra lệnh cho Unity nạp lại Scene đó từ đầu
            SceneManager.LoadScene(currentSceneName);

            //goi ham respawn de xu ly viec bien mat cua effect
            if (respawncontroller.instance != null)
            {
                respawncontroller.instance.startspawn(collision.gameObject);
            }
        }
    }
}