using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExBar : MonoBehaviour
{
    public Slider slider;
    public Text txtLv;

    public void SetMaxEx(int ex)
    {
        slider.maxValue = ex;
        
    }

    public void SetEx(int ex)
    {
        slider.value = ex;
    }
}
