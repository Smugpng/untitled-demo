using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private bool isCharging = false;
    float timer;
    [SerializeField] private UnityEngine.UI.Image  chargeUI;

    [Header("BulletStuff")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject explosion;
    [SerializeField] Transform spawnPos;
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            isCharging=true;
        }
        else if (context.action.WasReleasedThisFrame())
        {
            isCharging = false;
            Shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChargeShot();
        UpdateUI();
    }
    private void UpdateUI()
    {
        chargeUI.fillAmount = timer;
    }
    private void ChargeShot()
    {
        if (isCharging)
        {
            timer += Time.deltaTime/7;
        }
        else if (!isCharging)
        {
            timer = 0;
        }
    }
    private void Shoot()
    {
        if (timer < 1)
        {
            GameObject shot = Instantiate(bulletPrefab, spawnPos.position, transform.rotation);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            float speed = (7 + (timer * 2));
            rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }
        else if (timer > 1)
        {
            GameObject explosionWave = Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
