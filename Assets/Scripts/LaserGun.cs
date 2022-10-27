using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject laser_prefab;

    public Vector3 laser_offset_right;
    public Vector3 laser_offset_left;

    public float cool_down_time;

    public int aether_usage = 1;

    PlayerInventory player_inventory;

    MouseAim mouse;

    // Start is called before the first frame update
    void Start()
    {
        player_inventory = GetComponent<PlayerInventory>();
        mouse = GetComponent<MouseAim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerState.can_shoot)
        {
            /*
            if (player_inventory.current_aether > 0)
            {
                StartCoroutine(Shoot());
            }
            */
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (PlayerState.moveDirection == PlayerState.MoveDirection.Left)
        {
            Instantiate(laser_prefab, transform.position + laser_offset_left, Quaternion.Euler(0, 0, GetZRotation() - 90));
        }
        else if (PlayerState.moveDirection == PlayerState.MoveDirection.Right)
        {
            Instantiate(laser_prefab, transform.position + laser_offset_right, Quaternion.Euler(0, 0, GetZRotation() - 90 ));
        }

        // player_inventory.UseAether(aether_usage);
        
        PlayerState.can_shoot = false;
        yield return new WaitForSeconds(cool_down_time);
        PlayerState.can_shoot = true;
    }

    public float GetZRotation()
    {
        Vector3 difference;
        if (PlayerState.moveDirection == PlayerState.MoveDirection.Left)
        {
            difference = mouse.crosshairs.transform.position - (transform.position + laser_offset_left);
        }
        else
        {
            difference = mouse.crosshairs.transform.position - (transform.position + laser_offset_right);
        }
        
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return rotationZ;
    }
}
