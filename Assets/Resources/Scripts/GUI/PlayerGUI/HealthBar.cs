using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public static HealthBar instance;

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health) {
        slider.value = health;
    }
}
