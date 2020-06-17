using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogsManager : MonoBehaviour
{
    public static LogsManager instance;
    void Awake()
    {
        instance = this;
    }
    public Text NewText;
}
