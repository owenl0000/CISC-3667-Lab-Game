using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinMovement : MonoBehaviour
{
    public Rigidbody2D bullet;
    public PlayerMovement playerMovement;

    public float power = 100f;
    public float moveSpeed = 5f;

    void Start() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody2D instance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;

            // Check the player's direction and set the bullet's velocity accordingly
            if (playerMovement.direction == 1)
            {
                instance.velocity = new Vector2(15, instance.velocity.y);
            }
            else
            {
                instance.velocity = new Vector2(-15, instance.velocity.y);
            }

            instance.AddForce(instance.velocity * power);
        }
    }
}
