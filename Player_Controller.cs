using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int count = 6;
    private int lives;
    private int level;
    private bool facingRight;
    private bool isGrounded;

    public static int score;
    public static bool win;

    public Text scoretext;
    public Text wintext;
    public Text livestext;
    public float speed;
    public float jumpforce;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        level = 0;
        wintext.text = "";
        anim = GetComponent<Animator>();
        facingRight = true;
        win = false;

        musicSource.clip = musicClipOne;
        musicSource.Play();

        SetCountText ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();



        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
           {
            anim.SetInteger("State", 1);
           }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
           {
            anim.SetInteger("State", 0);
           }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
        flip(moveHorizontal);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (anim.GetInteger("State") == 2)
            {
                anim.SetInteger("State", 0);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        if (score == 6 && level == 0)
        {
            lives = 3;
            transform.position = new Vector2(19.52f, -2.43f);
            level = 1;
        }

        scoretext.text = "Score: " + score.ToString();
        livestext.text = "Lives: " + lives.ToString();

        if (score >= 12)
        {
            wintext.text = "You Win! Esc to Close";
            musicSource.clip = musicClipTwo;
            musicSource.Play();

        }
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            wintext.text = "You Lose! R to restart";
        }
    }

    private void flip(float horizontal)
    {
        if (horizontal >0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
