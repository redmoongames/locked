using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    private DialogueSystem dialogSystem;
    private string[] chapter1;
    public string playerName;

    private void Awake()
    {
        PlayerPrefs.SetString("playerName", "Maximchik");
        playerName = PlayerPrefs.GetString("playerName");


        if (playerName != "Maximchik")
            playerName = "Buggy Fredd";
    }

    void Start()
    {
        dialogSystem = DialogueSystem.instance;
        chapter1 = new string[]
        {
            
        };
    }

    int index = 0;
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (!dialogSystem.IsSpeaking || dialogSystem.isWaitingForUserInput)
            {
                Debug.Log(2);
                if (index >= chapter1.Length)
                {
                    return;
                }
                Say(chapter1[index]);
                index++;
            }
        }
        */
    }

    void Say(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        dialogSystem.Say(speech, speaker);
    }
}