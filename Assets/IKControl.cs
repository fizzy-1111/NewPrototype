using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    public Animator animator;

    public bool ikActive = false;
    float distance=9999f;
    float ikWeight = 0f;
    public Transform rightHandObj = null;
    public Transform lookObj ;

    void Start ()
    {
        //animator = GetComponent<Animator>();
        Debug.Log(animator);
        StartCoroutine(CalculateDistance());
        StartCoroutine(settingWeight());
    }

    //a callback for calculating IK     
    private void Update() {
        GameObject currentBall= GameObject.FindGameObjectWithTag("Ball");
        if(currentBall==null) return;
        rightHandObj.transform.position=new Vector3(currentBall.transform.position.x,currentBall.transform.position.y,lookObj.transform.position.z);
        lookObj.transform.position=new Vector3(currentBall.transform.position.x,currentBall.transform.position.y,lookObj.transform.position.z);
    }
    void OnAnimatorIK()
    {
        if(animator) {

            //if the IK is active, set the position and rotation directly to the goal.
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,ikWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,ikWeight);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                     animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,ikWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,ikWeight);  
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand,rightHandObj.rotation);
                    Debug.Log(ikWeight);
                }        

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
                animator.SetLookAtWeight(0);
            }
        }
    }
    IEnumerator CalculateDistance()
    {
        while (true)
        {
            GameObject currentBall= GameObject.FindGameObjectWithTag("Ball");
            if(currentBall!=null){
                Transform otherObject = currentBall.transform;
                distance = Vector3.Distance(transform.position, otherObject.position);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator settingWeight(){
        while (true)
        {
            if(distance<3f){
                ///Debug.Log("distance is less than 3");
                StartCoroutine(RaiseValue(0.5f));
            }
            else{
                StartCoroutine(decreaseValue(0.5f));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator RaiseValue(float speed)
    {
        while (ikWeight < 1)
        {
            ikWeight += Time.deltaTime * speed;
            yield return null;
        }
    }
    IEnumerator decreaseValue(float speed){
        while (ikWeight > 0)
        {
            ikWeight -= Time.deltaTime * speed;
            yield return null;
        }
    }
}
