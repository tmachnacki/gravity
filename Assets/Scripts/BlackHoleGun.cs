using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleGun : MonoBehaviour
{
    public GameObject black_hole_prefab;
    public GameObject current_black_hole;
    public Vector3 offset_right;
    public Vector3 offset_left;
    public int aether_usage = 5;

    PlayerInventory player_inventory;

    // Start is called before the first frame update
    void Start()
    {
        player_inventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && PlayerState.can_use_black_hole)
        {
            //if (player_inventory.current_aether >= 5)
            if (current_black_hole == null)
            {
                UseBlackHole();
            }
            else{
                Destroy(current_black_hole);
            }
            
        }
    }

    void UseBlackHole()
    {
        if (PlayerState.moveDirection == PlayerState.MoveDirection.Left)
        {
            current_black_hole = Instantiate(black_hole_prefab, transform.position + offset_left, Quaternion.identity);
        }
        else if (PlayerState.moveDirection == PlayerState.MoveDirection.Right)
        {
            current_black_hole = Instantiate(black_hole_prefab, transform.position + offset_right, Quaternion.identity);
        }

        // player_inventory.UseAether(aether_usage);
    }
}
