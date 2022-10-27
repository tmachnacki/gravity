using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum MoveDirection {Left, Right, None};
    public static bool is_walking = false;
    public static bool can_walk = true;
    public static bool is_flying = false;
    public static bool can_fly = true;
    public static MoveDirection moveDirection;
    public static bool can_move = true;
    public static bool can_shoot = true;
    public static bool can_use_black_hole = true;
    public static bool is_inverted = false;
    public static bool is_in_black_hole = false;
    
}
