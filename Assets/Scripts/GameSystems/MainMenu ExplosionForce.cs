using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MainMenuExplosionForce : MonoBehaviour
{
    Collider2D[] inRadius = null;
    [SerializeField] private float Force = 5;
    [SerializeField] private float radius = 5f;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15)
        {
            timer = 0;
            Explode();
        }
    }

    void Explode()
    {
        inRadius = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D c in inRadius)
        {
            Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 distanceVector = c.transform.position - transform.position;
                if (distanceVector.magnitude > 0 )
                {
                    float explosionForce = Force * distanceVector.magnitude;
                    rb.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }
}
