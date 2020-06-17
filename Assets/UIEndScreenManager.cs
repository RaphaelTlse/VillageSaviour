using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndScreenManager : MonoBehaviour
{
    public static UIEndScreenManager instance;
    void Awake()
    {
        instance = this;
    }
    public Text Result;
    public Text SavedLives;
    public Text WastedLives;
}
