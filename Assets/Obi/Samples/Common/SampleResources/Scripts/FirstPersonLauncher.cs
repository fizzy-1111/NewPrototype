using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class FirstPersonLauncher : MonoBehaviour {

	//public ObiColliderGroup colliderGroup;
	public GameObject prefab;
	public GoalKeeperAim goalKeeperAim;
	public float power = 0.5f;
	public static Vector3 finalDestination;

	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown(0)){

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			 RaycastHit hitInfo;

        // Check if the ray hits any colliders
			if (Physics.Raycast(ray, out hitInfo))
			{
				finalDestination = hitInfo.point;
				StartCoroutine(goalKeeperAim.gotoBall());
				Debug.Log("Ray hit object: " + hitInfo.collider.gameObject.name);
			}
			else
			{
				Debug.Log("Ray did not hit any objects");
			}

			GameObject projectile = GameObject.Instantiate(prefab,ray.origin,Quaternion.identity);
			Rigidbody rb = projectile.GetComponent<Rigidbody>();
			if (rb != null){
				rb.velocity = ray.direction * power;
			}

		}
	}
}
