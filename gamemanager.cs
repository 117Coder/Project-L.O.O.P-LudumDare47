using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{


    public bool OBJECTIVE1_FindAdminCard;
    public bool OBJECTIVE2_UnlockVaults;
    public bool OBJECTIVE3_EnterPasswordAtTheControlCentre;
    public bool OBJECTIVE4_OpenSyslogAtTech;
    public bool OBJECTIVE5_GoGetSprayFromTools;
    public bool OBJECTIVE6_FixCafeteriaBreachUsingSpray;
    public bool OBJECTIVE7_SeekSustinence;
    public bool OBJECTIVE8_UnlockComms;
    public bool OBJECTIVE9_BroadcastMessageFromComms;
    public bool OBJECTIVE10_UnlockExitRampfromSecurity;
    public bool OBJECTIVE11_SealAirlock;
    public bool OBJECTIVE12_CheckOnPlants;
    public bool OBJECTIVE13_getWaterfromKitchen;
    public bool OBJECTIVE14_waterplants;

    public GameObject map;




    private audiomanager audioManager;

    public Texture2D mouseTexture;

    public bool isgame = true;

    void Start()
    {
        //Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.ForceSoftware);
        

        audioManager = audiomanager.instance;
        if (audioManager == null)
        {
            Debug.LogError("GameManager could not find an AudioManager. ERROR");
        }

        if(isgame == true)
        {
            audioManager.playSound("HighAmbience");
            audioManager.playSound("noise");
        }
        else
        {
            audioManager.playSound("Dynamic");
        }
    }

    public float countdownnumber = 60f;
    public Text countdownlabel;
    public Text objectiveLabel;

    public float degreesPerSec = 300f;
    public GameObject cameratorotate;
    private bool startcoro = true;
    private bool canspin = false;

    public void startcountdown()
    {
        canspin = true;
    }

    private void Update()
    {
        if (canspin == true)
        {
            countdownnumber -= Time.deltaTime;
            countdownlabel.text = Mathf.RoundToInt(countdownnumber).ToString();

        }
        if(countdownnumber <= 0)
        {
            if (canspin == true)
            {
                
                if (startcoro == true) { StartCoroutine("Finishingspin"); }
                startcoro = false;
                float num = Random.Range(1, 1000);
                countdownlabel.text = num.ToString();
                float rotAm = degreesPerSec * Time.deltaTime;
                float curRot = cameratorotate.transform.localRotation.eulerAngles.z;
                cameratorotate.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot - rotAm));
                degreesPerSec += 10;
            }
                       
        }

        if (Input.GetKey(KeyCode.M))
        {
            map.SetActive(true);
        }
        else
        {
            map.SetActive(false);
        }
            
    }

    IEnumerator Finishingspin()
    {
        audioManager.playSound("hum");
        yield return new WaitForSeconds(8);
        audioManager.stopSound("hum");
        canspin = false;
        countdownlabel.text = "LOOPED";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resetcounter()
    {
        countdownnumber = 60;
    }

    public Text passBox;
    public string password;
    public GameObject controlcentrescreen2;
    public void readPasswordField()
    {
        if(passBox.text == password)
        {
            Debug.Log("Correct!");
            controlcentrescreen2.SetActive(true);
        }
        else
        {
            audioManager.playSound("wrong");
        }
        
    }



    public Text CommspassBox;
    public string Commspassword;
    public GameObject Commdoor;
    public void readCommsPasswordField()
    {
        if (CommspassBox.text == Commspassword)
        {
            Debug.Log("Correct!");
            openCommsDoor();
        }
        else
        {
            audioManager.playSound("wrong");
        }

    }

    public void openCommsDoor()
    {
        Commdoor.transform.Rotate(0, 90, 0);
        StartCoroutine("closeCommsDoor");
    }

    IEnumerator closeCommsDoor()
    {
        yield return new WaitForSeconds(2);
        Commdoor.transform.Rotate(0, -90, 0);
    }


    public Text currentToollbl;
    public string toolinhand = "NONE";

    public void changeToolToADMINCARD()
    {
        toolinhand = "ADMINCARD";
        currentToollbl.text = "CURRENT TOOL: Admin Card";
        finishObj1();
        audioManager.playSound("ping");
    }
    public void changeToolToMATTERSPRAY()
    {
        toolinhand = "MATTERSPRAY";
        currentToollbl.text = "CURRENT TOOL: Matter Spray";
        finishObj5();
        audioManager.playSound("ping");
    }

    public void changeToolToWATERCONTAINER()
    {
        toolinhand = "WATER";
        currentToollbl.text = "CURRENT TOOL: Container of Water";
        finishObj13();
        audioManager.playSound("ping");
    }

    public Text warningLabelForCafeBreach;
    public void checkRequirementsToFixCafe()
    {
        if (toolinhand != "MATTERSPRAY")
        {
            warningLabelForCafeBreach.text = "Go and find MATTER SPRAY";
        }
        else
        {
            warningLabelForCafeBreach.text = "Fix Breach...";
            finishObj6();
        }
    }

    public Text warninglabelforplantwatering;
    public void checkRequirementsToWaterPlants()
    {
        
        if (toolinhand != "WATER")
        {

            warninglabelforplantwatering.text = "Go and find WATER CONTAINER";
        }
        else
        {
            warningLabelForCafeBreach.text = "Water Plants";
            finishObj14();
        }
    }


    public GameObject door;
    public Text warninglabelforcardswipe;
    public void checkdoorvaultrequirments()
    {
        if(toolinhand != "ADMINCARD")
        {
            warninglabelforcardswipe.text = "NO CARD - FIND CARD";
        }
        else
        {
            openVaultDoor();
            warninglabelforcardswipe.text = "SWIPE CARD";
        }
    }

    public void openVaultDoor()
    {
        door.transform.Rotate(0, 90, 0);
        StartCoroutine("closeVaultDoor");
    }

    IEnumerator closeVaultDoor()
    {
        yield return new WaitForSeconds(2);
        door.transform.Rotate(0, -90, 0);
    }

    IEnumerator dissolveFinishMessage()
    {
        objectiveLabel.text = "FINISH OUTSTANDING OBJECTIVES";
        objectiveLabel.color = Color.red;
        yield return new WaitForSeconds(2);
        objectiveLabel.text = oldstring;
        objectiveLabel.color = Color.blue;
    }


    string oldstring = "N/A"; 

    public void finishObj1()
    {
        OBJECTIVE1_FindAdminCard = true;
        objectiveLabel.text = "OBJECTIVE: Unlock Vault and Find Password";
        countdownnumber += 20f;
    }

    public void finishObj2()
    {
        if(OBJECTIVE1_FindAdminCard == true)
        {
            countdownnumber += 20f;
            OBJECTIVE2_UnlockVaults = true;
            objectiveLabel.text = "OBJECTIVE: Sign into L.O.O.P OS at the control centre";

        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }

    }

    public void finishObj3()
    {


        if (OBJECTIVE2_UnlockVaults == true)
        {
            countdownnumber += 20f;
            OBJECTIVE3_EnterPasswordAtTheControlCentre = true;
            objectiveLabel.text = "OBJECTIVE: Open Syslog at Tech";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public void finishObj4()
    {


        if (OBJECTIVE3_EnterPasswordAtTheControlCentre == true)
        {
            countdownnumber += 20f;
            OBJECTIVE4_OpenSyslogAtTech = true;
            objectiveLabel.text = "OBJECTIVE: Go and get Matter Spray from Tools";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public void finishObj5()
    {


        if (OBJECTIVE4_OpenSyslogAtTech == true)
        {
            countdownnumber += 20f;
            OBJECTIVE5_GoGetSprayFromTools = true;
            objectiveLabel.text = "OBJECTIVE: Use Matter Spray to fix cafeteria breach";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public void finishObj6()
    {


        if (OBJECTIVE5_GoGetSprayFromTools == true)
        {
            countdownnumber += 20f;
            OBJECTIVE6_FixCafeteriaBreachUsingSpray = true;
            objectiveLabel.text = "OBJECTIVE: Seek Sustinence from cafeteria vending machine";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public void finishObj7()
    {


        if (OBJECTIVE6_FixCafeteriaBreachUsingSpray == true)
        {
            countdownnumber += 20f;
            OBJECTIVE7_SeekSustinence = true;
            objectiveLabel.text = "OBJECTIVE: Unlock Security (the password is in the vault)";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public bool rounddoing = true;
    public void finishObj8()
    {
        if(rounddoing == true)
        {
            if (OBJECTIVE7_SeekSustinence == true)
            {
                countdownnumber += 20f;
                OBJECTIVE8_UnlockComms = true;
                objectiveLabel.text = "OBJECTIVE: Broadcast a message from Security";
                rounddoing = false;
            }
            else
            {
                oldstring = objectiveLabel.text;
                StartCoroutine("dissolveFinishMessage");
            }
        }
        else
        {
            Debug.Log("SKIPPED");
        }

        
    }

    public void finishObj9()
    {


        if (OBJECTIVE8_UnlockComms == true)
        {
            countdownnumber += 20f;
            OBJECTIVE9_BroadcastMessageFromComms = true;
            objectiveLabel.text = "Unlock the exit ramp from Security";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public GameObject airlockDoor;
    public void finishObj10()
    {


        if (OBJECTIVE9_BroadcastMessageFromComms == true)
        {
            countdownnumber += 20f;
            airlockDoor.SetActive(false);
            OBJECTIVE10_UnlockExitRampfromSecurity = true;
            objectiveLabel.text = "OBJECTIVE: Seal Airlock (button at exit ramp)";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public GameObject airlock;
    public void finishObj11()
    {


        if (OBJECTIVE10_UnlockExitRampfromSecurity == true)
        {
            countdownnumber += 20f;
            airlock.SetActive(true);
            OBJECTIVE11_SealAirlock = true;
            objectiveLabel.text = "OBJECTIVE: Check on Plants in the not-so-green-greenhouse (Piece of paper on desk)";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public void finishObj12()
    {


        if (OBJECTIVE11_SealAirlock == true)
        {
            countdownnumber += 20f;
            OBJECTIVE12_CheckOnPlants = true;
            objectiveLabel.text = "Get Water from Kitchen";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public void finishObj13()
    {


        if (OBJECTIVE12_CheckOnPlants == true)
        {
            countdownnumber += 20f;
            OBJECTIVE13_getWaterfromKitchen = true;
            objectiveLabel.text = "OBJECTIVE: Water the Plants";
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }

    public GameObject endUI;

    public void finishObj14()
    {


        if (OBJECTIVE13_getWaterfromKitchen == true)
        {
            countdownnumber += 20f;
            OBJECTIVE14_waterplants = true;
            objectiveLabel.text = "OBJECTIVE: COMPLETE!";
            endUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            oldstring = objectiveLabel.text;
            StartCoroutine("dissolveFinishMessage");
        }
    }
}
