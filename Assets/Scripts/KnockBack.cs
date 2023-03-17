using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D;
    [SerializeField]
    private float strength = 16, delay = 0.15f;


    public static KnockBack instance;
    private void Start()
    {
        instance = this;
    }
    public void PlayFeedBack(GameObject Sender, GameObject reciver)
    {
        StopAllCoroutines();
        reciver.GetComponent<AIPath>().canMove = false;
        Vector2 direction = (transform.position - Sender.transform.position).normalized;
        rb2D.AddForce(direction* strength,ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {

        yield return new WaitForSeconds(delay);
        rb2D.velocity = Vector3.zero;
        this.GetComponent<AIPath>().canMove = true;
    }
}
