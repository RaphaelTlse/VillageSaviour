using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerControlsDisplay : MonoBehaviour
{
    public Text NewText;

    private void Start()
    {
        NewText.text = "<b>Controller (Xbox 360)</b> :\n\n<b>Left stick</b> : Move\n<b>A</b> : Interact\n<b>B (in dialogue)</b> : Close dialogue\n<b>X (in dialogue)</b> : Arrest\n<b>Y</b> : Main menu";
    }
}
