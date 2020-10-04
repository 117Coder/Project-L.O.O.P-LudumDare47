using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodoorscript : MonoBehaviour
{
    public Animator anim;
    // Update is called once per frame

    private audiomanager audioManager;


    void Start()
    {



        audioManager = audiomanager.instance;
        if (audioManager == null)
        {
            Debug.LogError("GameManager could not find an AudioManager. ERROR");
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetBool("door", true);
            audioManager.playSound("door");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("door", false);
        }
    }

}
