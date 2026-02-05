using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlinkingWall : MonoBehaviour
{
    UnityEngine.SceneManagement.Scene a_scene;
    string a_scene_name, Scene_name = "Level 1";
    float time = 3f;
    float timer = 0f;
    bool on = true;

    void Update()
    {
        a_scene = SceneManager.GetActiveScene();
        a_scene_name = a_scene.name;
        timer = (a_scene_name == Scene_name) ? timer + Time.deltaTime : 0f;
        if (timer >= time) {
            Switch(on);
            on = !on;
            timer = 0f;
        }
    }

    private void Switch(bool on) {
        this.GetComponent<SpriteRenderer>().enabled = on;
        this.GetComponent<BoxCollider2D>().enabled = !on;
    }
}
