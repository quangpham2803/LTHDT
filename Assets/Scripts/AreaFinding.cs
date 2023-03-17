using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFinding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
           
            other.GetComponent<FlipToPlayer>().enabled = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
           other.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Area1").transform;
            other.GetComponent<FlipToPlayer>().enabled = false;
           other.GetComponent<ShootPlayer>().enabled = false;
       

        }
    }
}
