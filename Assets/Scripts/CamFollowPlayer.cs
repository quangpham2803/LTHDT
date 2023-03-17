using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector2 minPos;
    public Vector2 maxPos;
    public float smoothing;
    private void FixedUpdate()
    {   
        if(transform.position != target.position)
        {
           transform.position = new Vector3(target.position.x,target.position.y,-10);
        }
       

    }


}   
