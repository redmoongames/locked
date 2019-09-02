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

	private string line;
    private string characterName;
    private string speechText;
    private string[] additiveSpeech;
    private string[] tempStrArray;
    private bool waitingForInput = false;
	private bool stopInput = false;
    private int processed;
	
	public GameObject BGGameObject; //новый фон
	public GameObject TextGameObject; //текстовое поле
	public GameObject ChoosingGameObject; //кнопки
	
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
	
	void Start()
	{
		//Запускаем сразу первую фразу из файла
		line = textArray[lineNum];
		while ((lineNum < textArray.Length - 1) && (line == null || line.StartsWith("@")))
		{
			lineNum++;
			line = textArray[lineNum];
		}
		tempStrArray = line.Split(new string[] { "::" }, StringSplitOptions.None);
		if (tempStrArray.Length == 2)
		{
			characterName = tempStrArray[0];
			speechText = tempStrArray[1].Trim();
			processed = 0;
			waitingForInput = true;
		} 
		else{
			characterName = "";
			speechText = tempStrArray[0].Trim();
			processed = 0;
			waitingForInput = true;
		}
		additiveSpeech = speechText.Split(new string[] { "&&" }, StringSplitOptions.None); 
        characterName.Trim();
		additiveSpeech[processed].TrimStart();
		DialogueSystem.instance.Say(additiveSpeech[processed], characterName, (processed != 0));
		processed++;
	}

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
        else if (lineNum < textArray.Length && !stopInput)
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
				else if(line.StartsWith("@hide"))
				{
					tempStrArray = line.Split(' ');
					string person = tempStrArray[1];
					CharacterManager.instance.HideCharacter(person);
				}
				else if(line.StartsWith("@bg"))
				{
					tempStrArray = line.Split(' ');
					string bg = tempStrArray[1];
					GameObject b = Instantiate(BGGameObject);
					b.GetComponent<Background>().SetImage(bg);
				}
				else if(line.StartsWith("@choosing"))
				{
					stopInput = true;
					tempStrArray = line.Split(' ');
					TextGameObject.SetActive(false);
					ChoosingGameObject.SetActive(true);
					int countActive = int.Parse(tempStrArray[1]);
					for(int i = 0; i < countActive; ++i)
					{
						ChoosingGameObject.transform.GetChild(i).gameObject.SetActive(true);
						//Это условие дополню еще попозже
						if(int.Parse(tempStrArray[2 + i]) != 0) ChoosingGameObject.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = new Color(0,0,0,1);
						ChoosingGameObject.transform.GetChild(i).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = tempStrArray[2 + countActive + i];
					}
					for(int i = countActive; i < 3; ++i)
					{
						ChoosingGameObject.transform.GetChild(i).gameObject.SetActive(false);
					}
					lineNum--;
				}
				else if(line.StartsWith("@jump"))
				{
					while (line == null || !line.StartsWith("@metka"))
					{
						lineNum++;
						line = textArray[lineNum];
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
	
	public void PressButton(GameObject button)
	{
		if(button.GetComponent<UnityEngine.UI.Image>().color == new Color(0,0,0,1))
		{
			button.SetActive(false);
		}
		else
		{
			bool isFind = false;
			line = textArray[lineNum];
			while ((line == null || !line.StartsWith("@choose")) || !isFind)
			{
				if(line != null && line.StartsWith("@choose"))
				{
					string[] _tempStrArray = line.Split(new string[] { " " }, StringSplitOptions.None);
					
					Debug.Log(_tempStrArray.Length);
					
					if(_tempStrArray[1].Trim() == button.name)
					{
						isFind = true;
						lineNum++;
						line = textArray[lineNum];
						break;
					}
				}
				lineNum++;
				line = textArray[lineNum];
			}
			
			while ((lineNum < textArray.Length - 1) && (line == null))
			{
				lineNum++;
				line = textArray[lineNum];
			}
			
			tempStrArray = line.Split(new string[] { "::" }, StringSplitOptions.None);
			if (tempStrArray.Length == 2)
			{
				characterName = tempStrArray[0];
				speechText = tempStrArray[1].Trim();
				processed = 0;
				waitingForInput = true;
			} 
			else{
				characterName = "";
				speechText = tempStrArray[0].Trim();
				processed = 0;
				waitingForInput = true;
			}
			additiveSpeech = speechText.Split(new string[] { "&&" }, StringSplitOptions.None); 
			characterName.Trim();
			additiveSpeech[processed].TrimStart();
			DialogueSystem.instance.Say(additiveSpeech[processed], characterName, (processed != 0));
			processed++;
			
			TextGameObject.SetActive(true);
			ChoosingGameObject.SetActive(false);
			stopInput = false;
		}
	}
}
