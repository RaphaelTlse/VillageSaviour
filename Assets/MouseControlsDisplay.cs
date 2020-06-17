using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControlsDisplay : MonoBehaviour
{
    public Text NewText;

    private void Start()
    {
        NewText.text = "<b>Mouse</b> :\n\n<b>Left click</b> : Move\n<b>Right click</b> : Interact";
    }
}
