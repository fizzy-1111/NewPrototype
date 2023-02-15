using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PortalTraveller
{
    // Start is called before the first frame update
    Vector3 pos;
    public float speed=2f;
    public float jumpHeight = 2f; // the force of the jump
    public float jumpTime = 0.5f; // the time the jump will last
    public bool isGrounded=true;
    public bool stopMoving=false;
    LayerMask groundMask=1<<6;
    LayerMask portalMask=1<<7;
    Rigidbody rb ;
    public GameObject ball;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        //detectPortal();
        playerJump();
        shoot();
        
    }
    public void MoveTowardsPosition(GameObject obj, Vector3 position, float speed)
    {
        // get the direction from the object to the position
        position.y=obj.transform.position.y;
        Vector3 direction = position - obj.transform.position;
        //Debug.Log(direction.magnitude);
        if(direction.magnitude<0.1f){
            TouchManager.Instance.setCurrentHit(Vector3.zero);
        }
        // normalize the direction to get a unit vector
        direction.Normalize();
        // move the object towards the position
        obj.transform.position += direction * speed * Time.deltaTime;
    }
    void playerJump(){
        //Debug.Log("isJumping");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // set the flag to indicate that the character is now jumping
            Debug.Log("isJumping");
            // apply the jump force to the character's rigidbody
            Debug.Log(Physics.gravity.magnitude);
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);

            // set a timer for the jump time
        }
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f, groundMask))
        {
            if (hit.collider.tag=="Ground")
            {
                isGrounded=true;
            }
        }
        else{
            isGrounded=false;
        }
    }
    void movingAgain(){
        stopMoving=false;
    }
    void Moving(){
        if(TouchManager.Instance.getCurrentHit()!=Vector3.zero&&!stopMoving){
            MoveTowardsPosition(transform.gameObject,TouchManager.Instance.getCurrentHit(),speed);
            Vector3 direction = TouchManager.Instance.getCurrentHit() - transform.position;
              // Determine which direction to rotate towards

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
            Vector3 xzOnlyDirection = new Vector3(newDirection.x, 0, newDirection.z);
            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, xzOnlyDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(xzOnlyDirection);
        }
    }
    void shoot(){
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("shooting");
            GameObject newball= Instantiate(ball,transform.position+ transform.forward*2,Quaternion.identity);
            newball.GetComponent<Rigidbody>().AddForce(transform.forward*30,ForceMode.Impulse);
        }
    }
}
