using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Kill", .6f);
    }

    void Kill()
    {
        Destroy(this.gameObject);
    }
}
