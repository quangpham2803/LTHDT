using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMove : MonoBehaviour
{
    public float speedRun;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public float punchRate = 4f;
    private float nextPunchTime = 0f;
    public int statusMovement;
    public static PlayerMove instance;

    // Start is called before the first frame update
    void Start()
    {
        statusMovement = 3;
        instance = this;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("movement_x", movement.x);
        animator.SetFloat("movement_y", movement.y);
        animator.SetFloat("movement_speed", movement.magnitude);
        if (Mathf.Abs(movement.x) > 0.2f && Mathf.Abs(movement.y) < 0.2)
        {
            animator.SetFloat("last_x", movement.x);
            animator.SetFloat("last_y", 0);
        }
        else if (Mathf.Abs(movement.y) > 0.2f & Mathf.Abs(movement.x) < 0.2)
        {
            animator.SetFloat("last_y", movement.y);
            animator.SetFloat("last_x", 0);
        }

        if (Time.time >= nextPunchTime)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("is_attacking", true);
                nextPunchTime = Time.time + 1f / punchRate;
            }
            else
            {
                animator.SetBool("is_attacking", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            statusMovement = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            statusMovement = 2;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            statusMovement = 3;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            statusMovement = 4;
        }
    }
    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * speedRun * Time.fixedDeltaTime);
    }
  
}


