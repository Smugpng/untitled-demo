using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    private float horizontal;
    private float vertical;
    private float speed = 5.0f;
    private float dashMultiplier = 2;
    private bool isFacingRight;
    private bool isFacingUp;

    private bool isDashing;
    PlayerHealth health;
    PlayerAudio audio;

    public bool canHurt = true;
    private bool HitWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.3f, wallLayer);
    }
    private bool HitEnemy()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.3f, enemyLayer);
    }
    private void HorizontalFlip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
  
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<PlayerAudio>();
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        FlipCheck();
        CheckCollision();
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            StartCoroutine(AddDash());
        }
    }
    private IEnumerator AddDash()
    {
        speed *= dashMultiplier;
        audio.dash();
        isDashing = true;
        yield return new WaitForSeconds(.2f);
        speed /= dashMultiplier;
        yield return new WaitForSeconds(1f);
        isDashing = false;
    }
    private void FlipCheck()
    {
        if (!isFacingRight && horizontal > 0f) 
        {
            HorizontalFlip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            HorizontalFlip();
        }
    }

    private void CheckCollision()
    {
        if (isDashing && HitWall())
        {
            CamShake camShake = FindAnyObjectByType<CamShake>();
            camShake.start = true;
        }
        if(HitEnemy())
        {
            if (canHurt)
            {
                canHurt = false;
                health.TakeDamage();
                CamShake camShake = FindAnyObjectByType<CamShake>();
                camShake.start = true;
                Invoke("resetIframes", 1) ;
            }

        }
    }
    void resetIframes()
    {
        canHurt = true;
    }
}
