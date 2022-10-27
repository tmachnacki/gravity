using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 2;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            health -= 1;
            
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
