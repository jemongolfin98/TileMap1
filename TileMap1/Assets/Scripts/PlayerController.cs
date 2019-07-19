using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int count;
    private int health;
    public float speed;
    public float jumpForce;
    public float jumpForce1;
    public Text countText;
    public Text winText;
    public Text loseText;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        health = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText();
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

       
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        if (collision.collider.tag == "Platform")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce1), ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            health = health - 1;
            SetHealthText();

        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 4)
        {
            winText.text = "You Win!";
            FindObjectOfType<SoundEffects>().WinMusic();
            speed = 0;
            jumpForce = 0;
            jumpForce1 = 0;
        }
    }

    void SetHealthText()
    {
        healthText.text = "Lives: " + health.ToString();

        if(health == 0)
        {
            loseText.text = "You Lose!";
            speed = 0;
            jumpForce = 0;
            jumpForce1 = 0;

        }
        
    }
    
}
