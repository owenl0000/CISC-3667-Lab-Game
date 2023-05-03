using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBalloons : MonoBehaviour
{
    public AudioClip popSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Pin")) 
        {
            AudioSource.PlayClipAtPoint(popSound, transform.position);
            Destroy(gameObject);
        }
    }
}
