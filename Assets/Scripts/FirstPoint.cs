using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPoint : MonoBehaviour
{   public GameObject player;
   public Transform firstPoint;
    private void Start()
    {
        Instantiate(player, firstPoint);
    }
}
