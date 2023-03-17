using Pathfinding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public int health;
    public Slider slider;
   public static EnemyHealth instance;
 

    public void decreaseHealth(int damage) {
        health -= damage;



    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            StartCoroutine(KnockCo(this.GetComponent<Rigidbody2D>(), this.gameObject));
            GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
            GetComponent<FlipToPlayer>().enabled = true;
            GetComponent<AIPath>().canMove = false;
            GetComponent<ShootPlayer>().enabled = true;
            GetComponent<AIPath>().gravity = new Vector3(0,0,0);
        }
        if (other.CompareTag("Wall"))
        {

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(moveCo());

        }
  


    }


    private void Start()
    {
        instance = this;
        slider.maxValue = health;
        if (GameManager.instance.hardMode)
        {
            health = Convert.ToInt32(health * 1.5);
            Debug.Log(health);
        }
    }
    private void Update()
    {
       
        slider.value = health;
    }
    IEnumerator KnockCo(Rigidbody2D enemy, GameObject other)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(0.3f);
            Debug.Log("Stop");
            enemy.velocity = Vector2.zero;
            other.GetComponent<AIPath>().canMove = true;
            enemy.isKinematic = true;

        }
    }
    IEnumerator moveCo()
    {
          yield return new WaitForSeconds(3f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

    }
}
