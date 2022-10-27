using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTriggers : MonoBehaviour
{
    public GameObject lightning;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(lightning);
            Destroy(gameObject);
        }
    }
}
