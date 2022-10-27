using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walk_speed;
    public float vertical_thrust;
    public float aether_use;
    public float fuel_use;
    public float refuel_speed;
    public float refuel_delay;
    bool can_refuel;
    public Color default_color;
    public Color invert_color;
    Rigidbody2D rigidbody2D;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    SpriteRenderer spriteRenderer;
    PlayerInventory playerInventory;
    float horizontal_input;
    float vertical_input;


    LayerMask platform_layer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerState.can_walk = true;
        PlayerState.can_move = true;
        PlayerState.is_inverted = false;
        PlayerState.moveDirection = PlayerState.MoveDirection.Right;
        platform_layer = LayerMask.GetMask("Platforms");
        playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.can_move)
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }
        
        horizontal_input = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontal_input) > 0) {
            if (horizontal_input > 0) 
            {
                if (PlayerState.moveDirection == PlayerState.MoveDirection.Left)
                {
                    spriteRenderer.flipX = false;
                }

                PlayerState.moveDirection = PlayerState.MoveDirection.Right;
            }
            else if (horizontal_input < 0)
            {
                if (PlayerState.moveDirection == PlayerState.MoveDirection.Right) 
                {
                    spriteRenderer.flipX = true;
                }
                PlayerState.moveDirection = PlayerState.MoveDirection.Left;
            }

            if (PlayerState.can_walk)
            {
                PlayerState.is_walking = true;
                animator.SetBool("is_walking", true);
            }
        }
        else
        {
            PlayerState.is_walking = false;
            animator.SetBool("is_walking", false);
        }

        // inversion mechanics
        /*
        if ( (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && playerInventory.current_aether > 0)
        {
            InvertGravity();
            StartCoroutine(ColorChange());
        }

        if (PlayerState.is_inverted)
        {
            playerInventory.UseAether(aether_use);
            if (playerInventory.current_aether <= 0)
            {
                InvertGravity();
                StartCoroutine(ColorChange());
            }
        }
        */

        if ( (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && PlayerState.can_fly && playerInventory.current_fuel > 0) 
        {

            PlayerState.is_walking = false;
            animator.SetBool("is_walking", false);
            PlayerState.can_walk = false;
            PlayerState.is_flying = true;
            animator.SetBool("is_flying", true);
            
            can_refuel = false;
            playerInventory.UseFuel(fuel_use);
            if (PlayerState.is_inverted)
            {
                vertical_input = -vertical_thrust;
            }
            else
            {
                vertical_input = vertical_thrust;
            }
        }
        else
        {
            PlayerState.is_flying = false;
            animator.SetBool("is_flying", false);
            vertical_input = 0.0f;
            if (can_refuel)
            {
                playerInventory.AddFuel(refuel_speed);
            }

            
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            PlayerState.is_flying = false;
            animator.SetBool("is_flying", false);
            StartCoroutine(EnableRefuel());
        }

        if (!PlayerState.is_in_black_hole)
        {
            rigidbody2D.velocity = new Vector2((horizontal_input) * walk_speed, vertical_input);
        }
        
    }

    private void FixedUpdate() {
        
        if (IsGrounded())
        {
            PlayerState.can_walk = true;
            PlayerState.is_flying = false;
            animator.SetBool("is_flying", false);
            
            
        }
        else {
            PlayerState.can_walk = false;
            PlayerState.is_walking = false;
            animator.SetBool("is_walking", false);
        }
        
    }

    
    bool IsGrounded()
    {
        float extra_distance = 0.2f;
        RaycastHit2D raycastHit2D;

        if (!PlayerState.is_inverted)
        {
           raycastHit2D = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + extra_distance, platform_layer);
           
        }
        else 
        {
            raycastHit2D = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.up, (capsuleCollider.bounds.extents.y + extra_distance), platform_layer);
        }

        
        Color rayColor;
        if (raycastHit2D.collider != null) 
        {
            rayColor = Color.green;
        }
        else 
        {
            rayColor = Color.red;
        }

        if (!PlayerState.is_inverted)
        {
            Debug.DrawRay(transform.position, new Vector3(0, -(capsuleCollider.bounds.extents.y + extra_distance), 0), rayColor);
        }
        else 
        {
            Debug.DrawRay(transform.position, new Vector3(0, (capsuleCollider.bounds.extents.y + extra_distance), 0), rayColor);
        }
        
        return raycastHit2D.collider != null;
    }

    public IEnumerator ColorChange()
    {
        spriteRenderer.color = invert_color;
        yield return new WaitForSecondsRealtime(0.2f);
        spriteRenderer.color = default_color;
    }

    public void InvertGravity()
    {
        
        PlayerState.is_inverted = !PlayerState.is_inverted;
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }

    IEnumerator RotatePlayer(Quaternion targetRotation)
    {
        while(transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator EnableRefuel()
    {
        yield return new WaitForSecondsRealtime(refuel_delay);
        can_refuel = true;
    }

    public IEnumerator ChangeGravityGradually(float delta, float target)
    {
        yield return StartCoroutine(ColorChange());
        InvertGravity();

        PlayerState.can_fly = false;

        if (target < 0)
        {
            while (rigidbody2D.gravityScale > target)
            {
                rigidbody2D.gravityScale += (delta * Time.deltaTime);
                Debug.Log(rigidbody2D.gravityScale);
                yield return new WaitForEndOfFrame();
            }
            rigidbody2D.gravityScale = target;
            PlayerState.can_fly = true;
            
            
        }
        else if (target > 0)
        {
            while (rigidbody2D.gravityScale < target)
            {
                rigidbody2D.gravityScale += (delta * Time.deltaTime);
                Debug.Log(rigidbody2D.gravityScale);
                yield return new WaitForEndOfFrame();
            }
            rigidbody2D.gravityScale = target;
            PlayerState.can_fly = true;

            
        }
        
    }
}
