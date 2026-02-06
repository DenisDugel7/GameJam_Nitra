using System;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TAC : MonoBehaviour
{
    public bool trigger = false;
    bool fail = false;

    public void Update()
    {
        AudioSource audiosrc = GetComponent<AudioSource>();

        if (!audiosrc.isPlaying && fail) SceneManager.LoadScene("Level 1");

        if (trigger)
        {
             
                GameObject interacts = GameObject.Find("Interacts");
                TextMeshProUGUI interacts_text = interacts.GetComponent<TextMeshProUGUI>();
                interacts_text.text = "Press E to sign Terms and Conditions";

            if (Input.GetKey(KeyCode.E)) {
                audiosrc.Play();
                fail = true;
            }

        }
        else {
                GameObject interacts = GameObject.Find("Interacts");
                TextMeshProUGUI interacts_text = interacts.GetComponent<TextMeshProUGUI>();
                interacts_text.text = " ";    
            
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
