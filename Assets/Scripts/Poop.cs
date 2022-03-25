using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.Score();
            animator.SetTrigger("poop");
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            animator.SetTrigger("poop");
        }
    }
}
