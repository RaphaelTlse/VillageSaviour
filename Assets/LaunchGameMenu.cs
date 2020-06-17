using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGameMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("joystick button 7"))
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
