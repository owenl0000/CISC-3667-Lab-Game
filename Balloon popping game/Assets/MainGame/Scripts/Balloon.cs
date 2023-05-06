using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public enum BalloonSize { Small, Medium, Large };
    public BalloonSize balloonSize = BalloonSize.Small;
    public float speed = 2.0f; // speed of the balloon movement
    public float leftLimit = -4.0f; // left limit of the movement
    public float rightLimit = 4.0f; // right limit of the movement
    public float topLimit = 4.0f; // top limit of the movement
    public float bottomLimit = -4.0f; // bottom limit of the movement
    public float bounceForce = 2.0f;

    public AudioClip balloonSound;
    private AudioSource audioSource;

    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private float currentSize = 0.05f;
    [SerializeField] GameObject score; 

    private float smallSize = 0.05f;
    private float mediumSize = 0.1f;
    private float largeSize = 0.2f;

    private float sizeIncrease = 0f;
    private bool increaseSize = false;
    private float maxSizeTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // set the initial target position to the starting position of the balloon
        targetPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        score = GameObject.FindGameObjectWithTag("Score");
        

        switch (balloonSize)
        {
            case BalloonSize.Small:
                currentSize = smallSize;
                sizeIncrease = (mediumSize - smallSize) / GameManager.Instance.sizeIncreaseInterval;
                break;
            case BalloonSize.Medium:
                currentSize = mediumSize;
                sizeIncrease = (largeSize - mediumSize) / GameManager.Instance.sizeIncreaseInterval;
                break;
            case BalloonSize.Large:
                currentSize = largeSize;
                increaseSize = false;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = Vector3.one * currentSize;
        // move the balloon towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // if the balloon has reached the target position, set a new target position
        if (transform.position == targetPosition)
        {
            SetNewTargetPosition();
        }

        // increase the size of the balloon every 2 seconds
        if (increaseSize)
        {
            currentSize += sizeIncrease * Time.deltaTime;
            if (balloonSize == BalloonSize.Small && currentSize >= mediumSize)
            {
                balloonSize = BalloonSize.Medium;
                sizeIncrease = (largeSize - mediumSize) / GameManager.Instance.sizeIncreaseInterval;
            }
            else if (balloonSize == BalloonSize.Medium && currentSize >= largeSize)
            {
                balloonSize = BalloonSize.Large;
                increaseSize = false;
            }
        }

        if (balloonSize == BalloonSize.Large)
        {
            maxSizeTime += Time.deltaTime;
            if (maxSizeTime >= 1f) // Change 10f to the amount of time you want to allow the balloon to be at its maximum size
            {
                Destroy(gameObject);
            }
        }
    }

    // set a new target position for the balloon
    void SetNewTargetPosition()
    {
        // choose a random position within the rectangular movement limits
        float x = Random.Range(leftLimit, rightLimit);
        float y = Random.Range(bottomLimit, topLimit);
        targetPosition = new Vector3(x, y, transform.position.z);

        increaseSize = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin"))
        {
            AudioManager.Instance.PlayClip("balloon");

            switch (balloonSize)
            {
                case BalloonSize.Small:
                    score.GetComponent<Score>().UpdateScore(3);
                    break;
                case BalloonSize.Medium:
                    score.GetComponent<Score>().UpdateScore(2);
                    break;
                case BalloonSize.Large:
                    score.GetComponent<Score>().UpdateScore(1);
                    break;
            }
            Destroy(gameObject);
        }   
        else if (collision.CompareTag("Balloon"))
        {
            // calculate the bounce direction and apply force to the balloon
            Vector2 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * bounceForce, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Ground"))
        {
            // calculate the bounce direction and apply force to the balloon
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Sky"))
        {
            // calculate the bounce direction and apply force to the balloon
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            // calculate the bounce direction and apply force to the balloon
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            // calculate the bounce direction and apply force to the balloon away from the player
            Vector2 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * bounceForce * 2, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Bird")) {
            Vector2 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * bounceForce * 2, ForceMode2D.Impulse);
        }
    }
}
