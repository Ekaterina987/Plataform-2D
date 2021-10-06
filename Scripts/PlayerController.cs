using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 4f;
    public float DoubleJumpSpeed = 2.5f;

    private bool canDoubleJump = false;

    public Rigidbody2D rb;

    public SpriteRenderer spriteR;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if(CheckGround.isGrounded)
            {
                anim.SetBool("Jump", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                canDoubleJump = true;
            }
            else
            {
                if (Input.GetKeyDown("space"))
                {
                    if (canDoubleJump)
                    {
                        anim.SetBool("DoubleJump", true);
                        rb.velocity = new Vector2(rb.velocity.x, DoubleJumpSpeed);
                        canDoubleJump = false;
                    }
                }
            }
            
        }
        if (CheckGround.isGrounded == false)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Run", false);
        }
        if (CheckGround.isGrounded)
        {
            anim.SetBool("DoubleJump", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }
        if(rb.velocity.y < 0 && Input.GetKey("space"))
        {
            anim.SetBool("Fall", false);
            anim.SetBool("DoubleJump", true);
        }
        else if (rb.velocity.y < 0 && canDoubleJump == false)
        {

            anim.SetBool("Fall", false);

        }
        else if(rb.velocity.y < 0)
        {

            anim.SetBool("Fall", true);

        }
        if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            anim.SetBool("Fall", false);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            spriteR.flipX = false;
            anim.SetBool("Run", true);
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            spriteR.flipX = true;
            anim.SetBool("Run", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("Run", false);
        }
    }
}
