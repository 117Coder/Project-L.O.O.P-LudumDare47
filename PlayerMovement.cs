using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public Animator anim;

    float horizontal;
    float vertical;
    
    public float moveLimiter = 0.7f;
    public float runSpeed = 20.0f;

    public GameObject ETooltip;
    public bool interactability = false;

    private audiomanager audioManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = audiomanager.instance;
        if (audioManager == null)
        {
            Debug.LogError("GameManager could not find an AudioManager. ERROR");
        }
    }

    GameObject body;

    
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical > 0 || vertical < 0)
        {
            audioManager.playSound("footsteps");
            
        }

        if(interactability == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("E pressed");
                ETooltip.SetActive(false);
                audioManager.playSound("beep1");
                body = item.associatedwindow;
                body.SetActive(true);

            }
        }


    }

    private void FixedUpdate()
    {
        if (horizontal !=0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
            audioManager.playSound("footsteps");

        }
        else
        {
            
            if(horizontal > 0)
            {
                anim.SetBool("runright", true);
                audioManager.playSound("footsteps");


            }
            else if(horizontal < 0)
            {
                anim.SetBool("runleft", true);
                audioManager.playSound("footsteps");
            }
            else
            {
                anim.SetBool("runleft", false);
                anim.SetBool("runright", false);
            }
        }
        
        if(vertical > 0)
        {
            anim.SetBool("runbackward", true);
            audioManager.playSound("footsteps");
        }
        else if(vertical < 0)
        {
            anim.SetBool("runforward", true);
            audioManager.playSound("footsteps");
        }
        
        if (vertical == 0 && horizontal == 0)
        {
            anim.SetBool("runforward", false);
            anim.SetBool("runbackward", false);
        }

        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }


    public interactableitem item;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "interactable")
        {
            Debug.Log("INTERACTION AVAILABLE");
            interactability = true;
            item = collision.GetComponent<interactableitem>();
            ETooltip.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "interactable")
        {
            interactability = false;
            ETooltip.SetActive(false);
            if(body != null)
            {
                body.SetActive(false);
            }
            
        }
    }
}
