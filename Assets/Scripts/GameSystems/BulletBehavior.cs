using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    EnemyBehavior enemyBehavior;
    public GameObject hitFX;
    public bool isBullet;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyBehavior>() != null)
        {
            enemyBehavior = other.gameObject.GetComponent<EnemyBehavior>();
            if (isBullet)
            {
                Instantiate(hitFX, this.transform.position, Quaternion.identity);
            }
            
            enemyBehavior.HitDelay();
        }
        if (isBullet)
        {
            
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
