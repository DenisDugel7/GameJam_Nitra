using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_endGame : MonoBehaviour
{
    float timer;
    public float time = 7f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time) SceneManager.LoadScene("MainMenu");
    }
}
