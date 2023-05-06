using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float flapForce = 5.0f; // force applied to bird when it flaps its wings
    public float maxHorizontalSpeed = 2.0f; // maximum horizontal speed of bird
    public float flapInterval = 1.0f; // time between flapping of wings
    public float moveInterval = 2.0f; // time between changes in horizontal movement
    public float moveRange = 2.0f; // range of horizontal movement

    private Rigidbody2D rb;
    private float horizontalMovementTimer = 0.0f;

    private AudioSource audioSource;
    public AudioClip birdSound;

    [SerializeField] GameObject score; 
    private int points;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        points = GameManager.Instance.points;
        rb = GetComponent<Rigidbody2D>();
        score = GameObject.FindGameObjectWithTag("Score");
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        InvokeRepeating("FlapWings", 0.0f, flapInterval);
        InvokeRepeating("ChangeHorizontalMovement", moveInterval, moveInterval);
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }
    // Update is called once per frame
    void Update()
    {
        points = GameManager.Instance.points;
        // clamp horizontal velocity of bird to maximum speed
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        rb.velocity = newVelocity;
    }

    void FlapWings()
    {
        // apply a force to the bird to make it flap its wings and fly upwards
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
    }

    void ChangeHorizontalMovement()
    {
        // change the horizontal velocity of the bird randomly
        float horizontalMovement = Random.Range(-moveRange, moveRange);
        horizontalMovementTimer = 0.0f;
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = horizontalMovement;
        rb.velocity = newVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Pin")) {
            AudioManager.Instance.PlayClip("bird");

            if(GameManager.Instance.difficultyLevel != 3) {
                score.GetComponent<Score>().UpdateScore(points);
            }
            else {
                GameManager.Instance.timer = GameManager.Instance.gameTime;
                GameManager.Instance.currentSceneIndex = 3;
            }
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Sky")) {
            Destroy(gameObject);
        }
    }
}
