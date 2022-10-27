using UnityEngine;

public class Singularity : MonoBehaviour
{
    //This is the main script which pulls the objects nearby
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;

    public float player_scale;

    public float laser_scale;

    public float object_scale;

    bool activated = false;

    private void Start() {
        m_GravityRadius = GetComponent<CircleCollider2D>().radius;
    }
    private void OnTriggerEnter2D(Collider2D other) {
       
        if (other.GetComponent<Floater>())
        {
            other.GetComponent<Floater>().is_enabled = false;

        }
        
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            other.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            // PlayerState.is_in_black_hole = true;
        }
    }

    void OnTriggerStay2D (Collider2D other) {
        
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();

        if(rigidbody2D && other.GetComponent<SingularityPullable>()) {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            // float gravityIntensity = Vector3.Distance(transform.position, other.transform.position);

            if (other.CompareTag("Player"))
            {
                rigidbody2D.AddForce((transform.position - other.transform.position).normalized * gravityIntensity * rigidbody2D.mass * GRAVITY_PULL * player_scale * Time.smoothDeltaTime);
            }
            else if (other.CompareTag("Laser"))
            {
                /*
                Vector3 difference = transform.position - other.transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                other.transform.Rotate(new Vector3(0, 0, rotationZ) * 3 *Time.smoothDeltaTime);
                */
                


                rigidbody2D.AddForce((transform.position - other.transform.position).normalized * gravityIntensity * rigidbody2D.mass * GRAVITY_PULL * laser_scale* Time.smoothDeltaTime);
            }
            else
            {
                rigidbody2D.AddForce((transform.position - other.transform.position).normalized * gravityIntensity * rigidbody2D.mass * GRAVITY_PULL * object_scale * Time.smoothDeltaTime);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Floater>())
        {
            Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
            
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            
            //other.GetComponent<Floater>().Restart();
            //other.GetComponent<Floater>().is_enabled = true;
        }
        if (other.CompareTag("Player"))
        {
         
            PlayerState.is_in_black_hole = false;
            
            if (PlayerState.is_inverted)
            {
                other.GetComponent<Rigidbody2D>().gravityScale = -18.2f;
            }
            else
            {
                other.GetComponent<Rigidbody2D>().gravityScale = 18.2f;    
            }
            
            
        }
    }

    


}
