using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    [HideInInspector] public bool isDie = false;
    [Header("플레이어 이동속도")][SerializeField] private float speed = 0f;
    private float horizontal;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (GameManager.Instance.isStopTrigger)
        {
            animator.SetTrigger("start");
            PlayerMove();
        }

        if (!GameManager.Instance.isStopTrigger)
        {
            animator.SetTrigger("dead");
        }
        ScreenCheck();
    }
   
    private void PlayerMove()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
    }

    private void ScreenCheck()
    {
        Vector3 _worldPos = Camera.main.WorldToViewportPoint(transform.position);
        if (_worldPos.x < 0.05f) _worldPos.x = 0.05f;
        if (_worldPos.x > 0.95f) _worldPos.x = 0.95f;
        transform.position = Camera.main.ViewportToWorldPoint(_worldPos);
    }
}
