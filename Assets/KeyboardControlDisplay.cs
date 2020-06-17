using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardControlDisplay : MonoBehaviour
{
    public Text NewText;

    private void Start()
    {
        NewText.text = "<b>Keyboard</b> :\n\n<b>Arrow keys</b> : Move\n<b>Space, Enter</b> : Interact, close dialogue\n<b>B (in dialogue)</b> : Close dialogue\n<b>X (in dialogue)</b> : Arrest\n<b>Escape</b> : Main menu";
    }
}
