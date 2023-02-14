using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    public float speed=2f;
    public float jumpHeight = 2f; // the force of the jump
    public float jumpTime = 0.5f; // the time the jump will last
    public bool isGrounded=true;
    LayerMask groundMask=1<<6;
    Rigidbody rb ;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TouchManager.Instance.getCurrentHit()!=Vector3.zero){
            MoveTowardsPosition(transform.gameObject,TouchManager.Instance.getCurrentHit(),speed);
        }
        playerJump();
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
}
