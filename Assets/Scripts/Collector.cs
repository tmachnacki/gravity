using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    PlayerInventory player_inventory;
    // Start is called before the first frame update
    void Start()
    {
        player_inventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Aether"))
        {
            player_inventory.AddAether(other.gameObject.GetComponent<AetherValue>().aether_value);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Fuel"))
        {
            player_inventory.AddFuelPickUp(other.gameObject.GetComponent<FuelValue>().fuel_value);
            Destroy(other.gameObject);
        }

        
    }
}
