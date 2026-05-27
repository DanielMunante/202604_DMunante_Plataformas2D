using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 3f;
    float originalSpeed;
    [SerializeField] float jumpVelocity = 5f;

    [Header("Ground Check")]
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask groundLayerMask = Physics2D.DefaultRaycastLayers;

    [Header("Combat")]
    [SerializeField] Transform leftHit;
    [SerializeField] Transform rightHit;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spritRenderer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        spritRenderer = GetComponent<SpriteRenderer>();

        rightHit.gameObject.SetActive(false);
        leftHit.gameObject.SetActive(false);

        originalSpeed = movementSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    const float moveThreshold = 0.1f;
    // Update is called once per frame
    void Update()
    {
        rb2D.linearVelocityX = rawMove.x * movementSpeed;
        bool isMoving = Mathf.Abs(rawMove.x) > moveThreshold;
        //animator.SetBool("IsRunning", Mathf.Abs(rawMove.x) > moveThreshold);
        animator.SetBool("IsRunning", isMoving);

        if (isMoving)
        {
            spritRenderer.flipX = rawMove.x < 0f;
        }

        animator.SetBool("IsGrounded", IsGrounded());
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayerMask);
        return hit && (hit.collider != null);
    }

    Vector2 rawMove;
    public void SetRawMove(Vector2 rawMove)
    {

        this.rawMove = rawMove;

    }

    internal void Jump()
    {
        if (IsGrounded()) { rb2D.linearVelocityY = jumpVelocity; }
    }

    internal void Punch()
    {
        animator.SetTrigger("Punch");
        //animator.ResetTrigger("Punch");
    }

    const float deactivateHitDelay = 0.25f;
    public void OnAnimationPunch()
    { 
        if (spritRenderer.flipX)
        {
            leftHit.gameObject.SetActive(true);
            Invoke(nameof(DeactivateHits),deactivateHitDelay);
        }
        else
        {
            rightHit.gameObject.SetActive(true);
            Invoke(nameof(DeactivateHits), deactivateHitDelay);
        }
    }

    void DeactivateHits()
    {
        leftHit.gameObject.SetActive(false);
        rightHit.gameObject.SetActive(false);
    }


    public void SetSpeedMultiplier(float multiplier)
    {
        movementSpeed = originalSpeed * multiplier;
    }

    public void ResetSpeed()
    {
        movementSpeed = originalSpeed;
    }

}
