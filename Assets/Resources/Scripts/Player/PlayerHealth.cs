using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {   
    public int health;
    private bool isKill;

    [HideInInspector]
    public bool invinsible;

    private HealthBar healthBar;

    private GameObject blood1;

    private GameObject bloodHeal1;

    public static PlayerHealth instance;
    
    void Start() {
        health = 100;
        isKill = false;
        instance = this;
        healthBar = GameObject.Find("HealthBarPlayer").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(health);
        blood1 = Resources.Load<GameObject>("Prefabs/Effects/BloodEffect1");
        bloodHeal1 = Resources.Load<GameObject>("Prefabs/Effects/HealEffect1");
        

    }
   
    void Update() {   
        if (health == 0 && !isKill) {
            isKill = true;
        }
   
    }

    public void decreaseHealth(int damage) {
        if (!invinsible) {
            health = Math.Max(0, health - damage);
            healthBar.SetHealth(health);
            GameObject blood = Instantiate(blood1, transform.position, transform.rotation);
            blood.transform.SetParent(transform);
            SoundManager.PlaySound("Hurt");
        }
    }

    public void IncreaseHealth(int heal) {
        health = Math.Min(100, health + heal);
        healthBar.SetHealth(health);
        GameObject blood = Instantiate(bloodHeal1, transform.position, transform.rotation);
        blood.transform.SetParent(transform);
    }

    public bool IsKill {
        get { return isKill; }
    }
}
