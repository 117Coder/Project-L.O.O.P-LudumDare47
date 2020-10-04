using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    
    public string string1 = "";
    public string string2 = "";
    public string string3 = "";
    public string string4 = "";
    public string string5 = "";
    public string string6 = "";
    public string string7 = "";
    public string string8 = "";

    public GameObject associatedCanvas;
    public Text textObject;
    public float timing = 0.2f;
    public float pausetime = 0.6f;

    public gamemanager gm;


    private audiomanager audioManager;


    void Start()
    {



        audioManager = audiomanager.instance;
        if (audioManager == null)
        {
            Debug.LogError("GameManager could not find an AudioManager. ERROR");
            
        }

        StartCoroutine(TypewritingDialogue());
    }



    private IEnumerator TypewritingDialogue()
    {
        foreach (char character in string1.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }



        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string2.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }

        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string3.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }


        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string4.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }

        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string5.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }

        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string6.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }

        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string7.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }


        yield return new WaitForSeconds(pausetime);
        textObject.text = "";
        foreach (char character in string8.ToCharArray())
        {
            textObject.text += character;
            yield return new WaitForSeconds(timing);
            audioManager.playSound("keypress");
        }

        yield return new WaitForSeconds(pausetime);


        associatedCanvas.SetActive(false);
        gm.startcountdown();
    }

}
