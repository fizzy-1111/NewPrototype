using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Obi;
public class GoalKeeperAim : MonoBehaviour
{
    // Start is called before the first frame update
    Transform ball;
    public Animator animator;
    Vector3 finalPosition;
    public float speed=100f;
    void Start()
    {
        //ball= GameObject.FindGameObjectWithTag("Ball").transform;
    }

    // Update is called once per frame
    void Update()
    {
        catchBall();
    }
      private void OnDrawGizmos()
    {
        
    }
    void catchBall(){
        if(Input.GetKeyDown(KeyCode.L)){
            GameObject currentBall= GameObject.FindGameObjectWithTag("Ball");
            if(currentBall==null) return;
            ball=currentBall.transform;
            finalPosition = FirstPersonLauncher.finalDestination;
            animator.SetTrigger("catchLeft");
            Time.timeScale=0.5f;
        }
        if(Input.GetKeyDown(KeyCode.R)){
            GameObject currentBall= GameObject.FindGameObjectWithTag("Ball");
            if(currentBall==null) return;
            ball=currentBall.transform;
             finalPosition = FirstPersonLauncher.finalDestination;
            animator.SetTrigger("catchRight");
            Time.timeScale=0.5f;
        }
    }
    public IEnumerator gotoBall(){
        if(ball==null) yield return null;
        yield return new WaitForSeconds(0.5f);
        Vector3 newBallPosition= new Vector3(finalPosition.x,finalPosition.y,transform.position.z);
        transform.DOMove(newBallPosition,0.5f);
    }
}
