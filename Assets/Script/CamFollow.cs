using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; // the object to follow
    public float distance = 10f; // the distance between the camera and the object
    public float height = 5f; // the height offset between the camera and the object
    public float damping = 1f; // the camera movement damping

    void LateUpdate()
    {
        if (target != null) {
            // calculate the camera's position based on the target position
            Vector3 targetPosition = new Vector3(target.position.x, 2 + height, target.position.z - distance);
            // smoothly move the camera towards the target position
            transform.position = Vector3.Slerp(transform.position, targetPosition, damping * Time.deltaTime);

            // make the camera look at the target object
            //Vector3 trueTarget=new Vector3(target.position.x,2,target.position.z);
            //transform.LookAt(trueTarget);
        }
    }
}
