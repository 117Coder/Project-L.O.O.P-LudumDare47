using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowHandler : MonoBehaviour
{
    public GameObject associatedbody;
    private audiomanager audioManager;


    void Start()
    {



        audioManager = audiomanager.instance;
        if (audioManager == null)
        {
            Debug.LogError("GameManager could not find an AudioManager. ERROR");
        }


    }

    public void closewindow()
    {
        associatedbody.SetActive(false);
        audioManager.playSound("ping");
    }

    public void openwindow()
    {
        associatedbody.SetActive(true);
        audioManager.playSound("ping");
    }

}
