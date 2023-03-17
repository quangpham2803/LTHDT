using Pathfinding;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour {
    private EnemyHealth enemyHealth;
    public int damage;
    public float thrust;
    private GameObject explosion;
    public UnityEvent OnBegin, OnDone;
    void Start() {
        Destroy(gameObject, 4f);
        explosion = Resources.Load<GameObject>("Prefabs/Effects/BulletExplosionEffect1");
    }
  
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.decreaseHealth(damage);
            Rigidbody2D rbEnemy = other.GetComponent<Rigidbody2D>();

            if (rbEnemy != null)
            {
                other.GetComponent<AIPath>().canMove = false;
                rbEnemy.isKinematic = false;
                Vector2 diff = rbEnemy.transform.position - transform.position;
                diff = diff.normalized * thrust;
                rbEnemy.AddForce(diff, ForceMode2D.Impulse);
                rbEnemy.isKinematic = true;
            }

        }
        
        if (other.CompareTag("Boss")) {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            bossHealth.TakeDamage(damage);
        }
        
        if (other.CompareTag("Hydra")) {
            HydraHealth bossHealth = other.GetComponent<HydraHealth>();
            bossHealth.TakeDamage(damage);
        }
        
        if (!other.CompareTag("Player") &&
            !other.CompareTag("PlayerChild") &&
            !other.CompareTag("Bullet") &&
            !other.CompareTag("Hole") &&
             !other.CompareTag("Area1") &&
                 !other.CompareTag("Area2") &&
                     !other.CompareTag("Area3") &&
            !other.CompareTag("EnemyBullet") &&
            !other.CompareTag("Ignore")) {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            //SoundManager.PlaySound("Explosion");
        }
    }
    private void Update()
    {
        if(PlayerMove.instance.statusMovement == 4)
        {
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,90);
        }
        if (PlayerMove.instance.statusMovement == 3)
        {
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -90);
        }
        if (PlayerMove.instance.statusMovement == 2)
        {
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public int Damage {
        get { return damage; }
    }
 
}
