using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PortalTraveller
{
   NavMeshAgent nav;
   public Transform player;

   private void Start() {
    nav = GetComponent <NavMeshAgent> ();
   }
     void Update ()
    {
        // Set the destination of the nav mesh agent to the player.
        nav.SetDestination (player.position);
    }

}
