using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
    private Animator animator;
    private EnemyHealth enemyHealth;
    private GameObject explosion;
    public GameObject[] items ;
    private int randomNumItem;
    public bool deathFromAnimator;
    
    void Start() {
        items =  Resources.LoadAll<GameObject>("Prefabs/PassiveItemsDummy");
        animator = GetComponent<Animator>();
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        explosion = (GameObject) Resources.Load("Prefabs/Effects/EnemyDeath1", typeof(GameObject));
    }

    void Update() {
        if (enemyHealth.health <= 0 && !deathFromAnimator) {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<ExPlayer>().ex += 50;
            Instantiate(explosion, transform.position, transform.rotation);
            randomNumItem = Random.Range(0, items.Length);
            Instantiate(items[randomNumItem], transform.position, transform.rotation);
        }

        if (enemyHealth.health <= 0 && deathFromAnimator) {
            animator.SetBool("Death", true);
        }
    }

    void DestroyEnemy() {
        Destroy(gameObject);
    }
}
