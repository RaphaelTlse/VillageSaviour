﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGame : MonoBehaviour
{
    public void loadMainScene()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
