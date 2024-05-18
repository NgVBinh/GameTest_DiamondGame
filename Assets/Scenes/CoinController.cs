using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x,100);
    }
}
