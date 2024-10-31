using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ship : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)) * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        rb.AddForce(
            (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized -
             (Vector2) (this.transform.position - Vector3.zero).normalized) * force, ForceMode2D.Impulse);
    }
}
