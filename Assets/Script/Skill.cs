using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Start is called before the first frame update
    float mana ;
    public GameObject skillInstance;
    bool playSkill1=false;
     private Quaternion startRotation;
    private Quaternion endRotation;
    public float rotationDuration = 2.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)&&skillInstance!=null){
            skillInstance.gameObject.SetActive(true);
            Invoke("stopSkill",0.5f);
        }
    }
    enum skill
    {
       firstSkill,
       secondSkill,
       thirdSkill,

    }
    
    void stopSkill(){
       skillInstance.gameObject.SetActive(false);
    }
}
