using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    //used methods to seperate just edit it then it's done
    public Transform target;
    [Range(0, 10)] public float speed;
    [Range(0, 10)] public float dodgeSpeed;
    public bool assassinMove;

    void Update()
    {
        if (assassinMove)
            MoveAssassin();
        else
            MoveNormal();
    }

    void MoveNormal()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void MoveAssassin()
    {
        //get a random direction
        Vector3 r = Random.insideUnitCircle;    //for 2D
        //for 3D top down, you need to swap the x and z components
        //r.Set(r.x, 0, r.y);

        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + r, dodgeSpeed * Time.deltaTime);
        transform.position =
            Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
