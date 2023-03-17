using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{

 
    public float currentTime;
    public GameObject objTime;
    public TextMeshProUGUI txtTime;
    public GameObject backGround;


    public float currentTime2;
    public GameObject objTime2;
    public TextMeshProUGUI txtTime2;
    public GameObject backGround2;
  
    private void Start()
    {
     
     
    }
    private void Update()
    {
        if (!ActivateForceShield.instance.wait && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            backGround.SetActive(true);
            objTime.SetActive(true);
            txtTime.text = Convert.ToInt32(currentTime).ToString();
        }
         if(ActivateForceShield.instance.wait && currentTime < 0)
        {
            backGround.SetActive(false);
            objTime.SetActive(false);
            currentTime = 4;
        }
   

        if(!ActivateTurret.instance.wait && currentTime2 > 0)
        {
            currentTime2 -= Time.deltaTime;
            txtTime2.text = Convert.ToInt32(currentTime2).ToString();
            backGround2.SetActive(true);
            objTime2.SetActive(true);
        }
         if(ActivateTurret.instance.wait && currentTime2 < 0)
        {
            currentTime2 = 8;
            backGround2.SetActive(false);
            objTime2.SetActive(false);
        }
    }
}
