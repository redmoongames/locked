using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptParser : MonoBehaviour {

    DialogueSystem dialogue = DialogueSystem.instance;

    TextAsset textScript;
    string[] textArray;
    
    private int lineNum = 0;

    private void Awake()
    {
        textScript = Resources.Load("TestScript", typeof(TextAsset)) as TextAsset;
        textArray = textScript.text.Split('\n');
        for (int i = 0; i < textArray.Length; i++)
        {
            if (textArray[i].Length < 5 || textArray[i].StartsWith("//"))
            {
                textArray[i] = null;
            }
        }
    }

    private string line;
    private string characterName;
    private string speechText;
    private string[] additiveSpeech;
    private string[] tempStrArray;
    private bool waitingForInput = false;
    private int processed;

    private void Update()
    {
        if (waitingForInput)
        {
            additiveSpeech = speechText.Split(new string[] { "&&" }, StringSplitOptions.None); 
            characterName.Trim();
            if (Input.GetMouseButtonDown(0))
            {
                if (DialogueSystem.instance.IsSpeaking)
                { 
                    DialogueSystem.instance.StopSpeaking();
                }
                else
                {
                    additiveSpeech[processed].TrimStart();
                    DialogueSystem.instance.Say(additiveSpeech[processed], characterName, (processed != 0));
                    processed++;
                }
            }
            if (processed == additiveSpeech.Length)
            {
                lineNum++;
                waitingForInput = false;
            }
        }
        else if (lineNum < textArray.Length)
        {
            line = textArray[lineNum];
            while ((line == null) && (lineNum < textArray.Length - 1))
            {
                Debug.Log(null);
                lineNum++;
                line = textArray[lineNum];
            }
            line.Trim();

            if (line.StartsWith("@"))
            {
                //tempStrArray = line.Split(' ');

                if (line.StartsWith("@MoveTo") || line.StartsWith("@Show"))
                {
                    tempStrArray = line.Split(' ');
                    Character targetCharacter = CharacterManager.instance.GetCharacter(tempStrArray[1]);

                    if (line.StartsWith("@MoveTo"))
                    {
                        
                        Vector2 targetVector;
                        targetVector.x = (float)Convert.ToDouble(tempStrArray[2]);
                        targetVector.y = (float)Convert.ToDouble(tempStrArray[3]);
                        targetCharacter.MoveTo(targetVector, 1);
                    }

                    else if (line.StartsWith("@Show"))
                    {
                        string body = tempStrArray[2];
                        targetCharacter.SetBody(tempStrArray[2]);
                        //targetCharacter.SetExpression(tempStrArray[3]);
                    }
                }
                else
                {
                    Debug.Log("Такой команды не существует: " + line);
                }
                lineNum++;
            }
            else
            {
                tempStrArray = line.Split(new string[] { "::" }, StringSplitOptions.None);
                if (tempStrArray.Length == 2)
                {
                    characterName = tempStrArray[0];
                    speechText = tempStrArray[1].Trim();
                    processed = 0;
                    waitingForInput = true;
                } else {
                    Debug.Log("line: \"" + line + "\"");
                    Debug.Log(line == null);
                }
                DebugUI.instance.Clear();
                DebugUI.instance.WriteLine("Current Line is " + lineNum);
            }
        }
    }
}
