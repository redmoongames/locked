using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public static DebugUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    public Text dubugText;

    public void Clear()
    {
        dubugText.text = "";
    }

    public void WriteLine(string line)
    {
        dubugText.text += "\n" + line;
    }
}
