using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;
        private float jumpPower = 30f; //Set Gravity Scale in Rigidbody2D Component to 5
        public float cameraFollowSpeed = 5f;

        public Pin pin;
        public Transform LaunchOffset;
        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        public int direction = 1;
        bool isJumping = false;
        private bool alive = true;
        private bool isKickboard = false;
        private Camera mainCamera;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            //Restart();
            if (alive)
            {
                //Hurt();
                //Die();
                Attack();
                Jump();
                KickBoard();
                Run();

            }
        }

         private void FixedUpdate()
        {
            if (mainCamera != null)
            {
                Vector3 targetPosition = transform.position;
                targetPosition.z = mainCamera.transform.position.z;
                targetPosition.y = mainCamera.transform.position.y;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {   
            if(other.gameObject.CompareTag("Ground")) {
            anim.SetBool("isJump", false);
            }
        }
        void KickBoard()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && isKickboard)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && !isKickboard )
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }

        }

        void Run()
        {
            if (!isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                anim.SetBool("isRun", false);


                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = -1;
                    moveVelocity = Vector3.left;

                    transform.localScale = new Vector3(direction, 1, 1);
                    if (!anim.GetBool("isJump"))
                        anim.SetBool("isRun", true);

                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 1;
                    moveVelocity = Vector3.right;

                    transform.localScale = new Vector3(direction, 1, 1);
                    if (!anim.GetBool("isJump"))
                        anim.SetBool("isRun", true);

                }
                transform.position += moveVelocity * movePower * Time.deltaTime;

            }
            if (isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = -1;
                    moveVelocity = Vector3.left;

                    transform.localScale = new Vector3(direction, 1, 1);
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = 1;
                    moveVelocity = Vector3.right;

                    transform.localScale = new Vector3(direction, 1, 1);
                }
                transform.position += moveVelocity * KickBoardMovePower * Time.deltaTime;
            }
        }
        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }

            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
        }
    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
            if(direction == 1) {
                Instantiate(pin, LaunchOffset.position, Quaternion.Euler(0f, 0f, 0f) );
            }
            else {
                Instantiate(pin, LaunchOffset.position, Quaternion.Euler(0f, 180f, 0f) );
            }
        }
    }
}

    

