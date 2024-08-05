using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosive : MonoBehaviour
{
    EnemyBehavior enemyBehavior;
    float timer = 0;
    void FixedUpdate()
    {
        timer += Time.deltaTime * 5;
        transform.localScale = new Vector3(timer * 2, timer * 2, 1);
        if (timer >= 15)
        {
            Destroy(gameObject);
        }
    }
    
}
