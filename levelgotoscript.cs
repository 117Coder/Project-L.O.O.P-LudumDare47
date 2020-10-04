using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelgotoscript : MonoBehaviour
{
    public int levelToGoTo;

    public void teleportToLevel()
    {
        SceneManager.LoadScene(levelToGoTo);
    }
}
