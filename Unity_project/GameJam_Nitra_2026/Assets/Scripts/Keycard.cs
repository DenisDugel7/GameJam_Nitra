using UnityEngine;

public class Keycard : MonoBehaviour
{
    public GameObject doors_go;
    Doors doors;
    void Start()
    {
        doors = doors_go.GetComponent<Doors>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        doors.opened = false;
        Destroy(this.gameObject);
    }
}
