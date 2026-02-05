using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TAC : MonoBehaviour
{
    bool trigger = false;
    public void Update()
    {
        if (trigger && Input.GetKey(KeyCode.E)) {
            SceneManager.LoadScene("Level 1");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") trigger = false;
    }


}
