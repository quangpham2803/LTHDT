using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExPlayer : MonoBehaviour
{
    public int ex;
    public int lv;

    [HideInInspector]
    public static ExPlayer instance;

    private ExBar exBar;

    
    void Start()
    {   
        lv = 1;
        ex = 0;
        exBar = GameObject.Find("EXBar").GetComponent<ExBar>();
        exBar.SetMaxEx(100);
        exBar.SetEx(ex);
    }

    // Update is called once per frame
    void Update()
    {
       
        if(ex == 100)
        {
            ex = 0;
            lv++;
            exBar.txtLv.text = "LV." + lv.ToString();
            GameObject.Find("HealthBarPlayer").GetComponent<Slider>().maxValue  += 50;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health = (Convert.ToInt32(GameObject.Find("HealthBarPlayer").GetComponent<Slider>().maxValue));
            GameObject.Find("HealthBarPlayer").GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health;

        }
        exBar.SetEx(ex);
    }
}
