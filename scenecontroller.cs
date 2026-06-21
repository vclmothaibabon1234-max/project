using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller : MonoBehaviour
{
    public static scenecontroller instance;
    public Animator animator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void nextlevel()
    {
        StartCoroutine(Nextlevel());
    }
    IEnumerator Nextlevel()
    {
        animator.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("start");
    }
}
