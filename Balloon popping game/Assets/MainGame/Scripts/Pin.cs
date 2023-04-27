using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float speed = 5f;
    private void Update() {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
