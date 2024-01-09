using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform body;
    public LayerMask layerGrounded;
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private BoxCollider2D m_BoxCollider;

    private bool isGrounded;
    private bool isFacingRight;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        isFacingRight = false;
    }

    private void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        m_Rigidbody.velocity = new Vector2(move * moveSpeed, m_Rigidbody.velocity.y);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.velocity = Vector2.up * jumpForce;
        }

        if (move != 0)
        {
            m_Animator.SetBool("isMoving", true);
        } else
        {
            m_Animator.SetBool("isMoving", false);
        }

        if (move < 0 && isFacingRight)
        {
            Flip();
        } else if (move > 0 && !isFacingRight) {
            Flip();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.bounds.size, 0f, Vector2.down, 0.1f, layerGrounded);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight; 
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
