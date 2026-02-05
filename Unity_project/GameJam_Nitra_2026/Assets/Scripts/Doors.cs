using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{

    int sceneIndex;
    public bool opened = true;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened && collision.gameObject.tag == "Player") { 
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }
}
