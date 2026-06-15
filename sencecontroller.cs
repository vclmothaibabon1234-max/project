using UnityEngine;
using UnityEngine.SceneManagement;

public class sencecontroller : MonoBehaviour
{
    public static sencecontroller instance;
    public Vector3 lastCheckpointPosition;
    public bool hasTouchedCheckpoint = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ Object này sống xuyên Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
