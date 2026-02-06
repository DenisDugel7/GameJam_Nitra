using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rules : MonoBehaviour
{
    string SceneName;
    GameObject rules;
    TextMeshProUGUI rules_text;
    void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;
        rules = GameObject.Find("Rules");
        rules_text = rules.GetComponent<TextMeshProUGUI>();
        switch (SceneName) {
            case "Level 1":
                rules_text.text = "Rules:\n1. Use WASD to move";
                break;
            case "Level 2":
                rules_text.text = "Rules:\n1. The elevator is broken, use teleport instead";
                break;
            case "Level 3":
                rules_text.text = "Rules:\n1. You have to come to security for help out of here\n2. Buying a coffee gives you a speed boost";
                break;

            case "Level 4":
                rules_text.text = "Rules:\n1. Just sign a Terms and Conditions and leave";
                break;
            default:
                rules_text.text = "Press the Start to begin";
                break;

        }
    }


}
