using System.Collections;
using UnityEngine;

public class ActivateTurret : MonoBehaviour {
    public GameObject turret;
    public bool wait;
    public float waitTime;
    public static ActivateTurret instance;
    
    void Start() {
        wait = true;
        instance = this;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha2) && wait && Time.timeScale != 0) {
            StartCoroutine(waitToTurret());
            Instantiate(turret, new Vector2(transform.position.x, transform.position.y+0.5f), transform.rotation);
     
      
        }
    }

    IEnumerator waitToTurret() {
        wait = false;
        yield return new WaitForSeconds(waitTime);
        wait = true;
    }
}
