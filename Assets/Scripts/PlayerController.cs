using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Info")]

    public int speed;
    public int jumpForce;
    public int lives;
    public int levelTime; //minutes

    public Canvas canvas;

    private Rigidbody2D rb;
    private GameObject foot;
    private bool isJumping;
    private SpriteRenderer sprite;
    private Animator animator;

    private float startTime;
    private float spentTime;

    private HUDController hud;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

        //find the reference to the foot gameoject child
        foot = transform.Find("foot").gameObject;

        //get the sprite components of the sprite child object
        sprite = gameObject.transform.Find("player-idle-1").GetComponent<SpriteRenderer>();
        
        //get the animator controller of the sprite child object
        animator = gameObject.transform.Find("player-idle-1").GetComponent<Animator>();

        //get the current time
        startTime = Time.time;
        Debug.Log(startTime);

        //get the HUD controller
        hud = canvas.GetComponent<HUDController>();
    }

    private void FixedUpdate()
    {
        //get the value from -1 to 1 of the horizontal move
        float inputX = Input.GetAxis("Horizontal");

        //apply phisic velocity to the object with the move value * speed
        //the y coordenate is the same
        rb.velocity = new Vector2 (inputX * speed, rb.velocity.y);

        //Calculate spent time
        int timeleft = levelTime - (int)(Time.time);
        int minutes = timeleft / 60;
        int seconds = timeleft % 60;

        Debug.Log(minutes.ToString("00") + ":" + seconds.ToString("00"));
        if (timeleft <= 0)
        {
            //TODO: End Game
            Debug.Log("LOOSE Time up...");
        }
    }

    private void Update()
    {
        //pressing space and touching the ground
        if (Input.GetKeyDown(KeyCode.Space) && TouchGround() && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //moving right
        if (rb.velocity.x > 0) sprite.flipX = false;
        //moving left
        else if (rb.velocity.x < 0) sprite.flipX = true;

        //Play animations
        PlayerAnimate();

        
        
    }
    /// <summary>
    /// Check if touching the ground
    /// </summary>
    /// <returns>if touching or not</returns>
    private bool TouchGround()
    {
        //Send a imaginary line down 0.2f distance 
        RaycastHit2D hit = Physics2D.Raycast(foot.transform.position , Vector2.down, 0.2f);
        
        // touching something
        return hit.collider != null;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping=false;    
        }
    }

    /// <summary>
    /// Reduce live to the player
    /// </summary>
    /// <param name="damage">number of damage</param>
    public void TakeDamage(int damage)
    {
        lives -= damage;
        Debug.Log("Player Lives: " + lives);
        if (lives == 0)
        {
            GameManager.instance.Win = false;
        }
    }

    /// <summary>
    /// Control by code all the animation states
    /// </summary>
    private void PlayerAnimate()
    {
        //player jumping
        if (!TouchGround()) animator.Play("PlayerJump");
        //player running
        else if (TouchGround() && Input.GetAxis("Horizontal") != 0)
            animator.Play("PlayerRuning");
        //player idle
        else if (TouchGround() && Input.GetAxis("Horizontal") == 0)
            animator.Play("PlayerIdle");


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {

            Destroy(collision.gameObject);
            //Destroy the PowerUp after 0.1 seconds
            Invoke(nameof(InfoPowerUp), 0.1f);  

           
        }
        
    }

    private void InfoPowerUp()
    {
        int powerUpsNum = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        
        //TODO: write in HUD how many PowerUp left
        Debug.Log("PowerUps: " + powerUpsNum);

        if (powerUpsNum == 0)
        {
            GameManager.instance.Win = true;
            //TODO: Change Scene WIN
            Debug.Log("WIN!!!");
        }

    }
}
