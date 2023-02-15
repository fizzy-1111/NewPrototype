using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{
    // Start is called before the first frame update
    public LayerMask touchLayer;
    Vector3 currentHit = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            CheckHitPosition();
        }
    }
    bool CheckHitPosition()
    {
        Vector3 pos = Input.mousePosition;
        bool isHit;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        LayerMask maskclick=1<<6;
        isHit = Physics.Raycast(ray, out hit, 10000,maskclick);
        if (isHit)
        {
            //Debug.Log(hit.point);
            currentHit=hit.point;
        }
        return isHit;
    }
    public Vector3 getCurrentHit(){
        return currentHit;
    }
    public void setCurrentHit(Vector3 setVector){
        currentHit=setVector;
    }
}
