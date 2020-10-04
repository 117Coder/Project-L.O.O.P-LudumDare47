using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public GameObject MainUIGroup;
    public GameObject ThisUI;
    public int TitleSceneIndex = 1;

    public GameObject mutebtn;
    public GameObject unmutebtn;
    
    public void game_pause()
    {
        Time.timeScale = 0f;

        ThisUI.SetActive(true);
        MainUIGroup.SetActive(false);
    }

    public void game_resume()
    {
        Time.timeScale = 1f;

        ThisUI.SetActive(false);
        MainUIGroup.SetActive(true);
    }

    public void quit_to_title()
    {
        SceneManager.LoadScene(TitleSceneIndex);
        Time.timeScale = 1f;
    }

    public void mute()
    {
        mutebtn.SetActive(false);
        unmutebtn.SetActive(true);
        AudioListener.pause = true;
    }

    public void unmute()
    {
        mutebtn.SetActive(true);
        unmutebtn.SetActive(false);
        AudioListener.pause = false;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    
    }

}
