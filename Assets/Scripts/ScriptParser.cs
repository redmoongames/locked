﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptParser : MonoBehaviour
{

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
<<<<<<< HEAD

    public GameObject BGGameObject; //новый фон
    public GameObject TextGameObject; //текстовое поле
    public GameObject ChoosingGameObject; //кнопки
    public GameObject ArtGameObject; //арт
    AudioSource music;
    AudioSource sound;

=======
	
	public GameObject BGGameObject; //новый фон
	public GameObject TextGameObject; //текстовое поле
	public GameObject ChoosingGameObject; //кнопки
	public GameObject ArtGameObject; //арт
	AudioSource music;
	AudioSource sound;
	
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
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
<<<<<<< HEAD
        music = transform.GetChild(0).GetComponent<AudioSource>();
        sound = transform.GetChild(1).GetComponent<AudioSource>();
    }

    void Start()
    {
        //Запускаем сразу первую фразу из файла
        line = textArray[lineNum];
        while ((lineNum < textArray.Length - 1) && (line == null || line.StartsWith("@")))
        {
            if ((line != null) && line.StartsWith("@"))
            {
                if (line.Length > 1) line = line.Substring(0, line.Length - 1);
                if (line.StartsWith("@moveTo") || line.StartsWith("@show"))
=======
		music = transform.GetChild(0).GetComponent<AudioSource>();
		sound = transform.GetChild(1).GetComponent<AudioSource>();
    } 
	
	void Start()
	{
		//Запускаем сразу первую фразу из файла
		line = textArray[lineNum];
		while ((lineNum < textArray.Length - 1) && (line == null || line.StartsWith("@")))
		{
			if ((line != null) && line.StartsWith("@"))
            {
				if(line.Length > 1) line = line.Substring(0, line.Length - 1);
                if (line.StartsWith("@MoveTo") || line.StartsWith("@Show"))
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
                {
                    tempStrArray = line.Split(' ');
                    Character targetCharacter = CharacterManager.instance.GetCharacter(tempStrArray[1]);

<<<<<<< HEAD
                    if (line.StartsWith("@moveTo"))
                    {

=======
                    if (line.StartsWith("@MoveTo"))
                    {
                        
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
                        Vector2 targetVector;
                        targetVector.x = (float)Convert.ToDouble(tempStrArray[2]);
                        targetVector.y = (float)Convert.ToDouble(tempStrArray[3]);
                        targetCharacter.MoveTo(targetVector, 1);
                    }
<<<<<<< HEAD
                    else if (line.StartsWith("@show"))
=======
                    else if (line.StartsWith("@Show"))
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
                    {
                        string body = tempStrArray[2];
                        targetCharacter.SetBody(tempStrArray[2]);
                        //targetCharacter.SetExpression(tempStrArray[3]);
                    }
                }
<<<<<<< HEAD
                else if (line.StartsWith("@hide"))
                {
                    tempStrArray = line.Split(' ');
                    string person = tempStrArray[1];
                    CharacterManager.instance.HideCharacter(person);
                }
                else if (line.StartsWith("@bg"))
                {
                    Debug.Log(line + " - " + line.Length);
                    tempStrArray = line.Split(' ');
                    string bg = tempStrArray[1];
                    GameObject b = Instantiate(BGGameObject);
                    b.GetComponent<Background>().SetImage(bg);
                }
                else if (line.StartsWith("@music"))
                {
                    tempStrArray = line.Split(' ');
                    music.clip = Resources.Load<AudioClip>("Music/" + tempStrArray[1]);
                    music.Play();
                }
                else if (line.StartsWith("@sound"))
                {
                    tempStrArray = line.Split(' ');
                    AudioClip a = Resources.Load<AudioClip>("Sounds/" + tempStrArray[1]);
                    sound.PlayOneShot(a, 1);
                }
                else if (line.StartsWith("@choosing"))
                {
                    stopInput = true;
                    tempStrArray = line.Split(' ');
                    TextGameObject.SetActive(false);
                    ChoosingGameObject.SetActive(true);
                    int countActive = int.Parse(tempStrArray[1]);
                    for (int i = 0; i < countActive; ++i)
                    {
                        ChoosingGameObject.transform.GetChild(i).gameObject.SetActive(true);
                        //Это условие дополню еще попозже
                        if (int.Parse(tempStrArray[2 + i]) != 0) ChoosingGameObject.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 1);
                        ChoosingGameObject.transform.GetChild(i).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = tempStrArray[2 + countActive + i];
                    }
                    for (int i = countActive; i < 3; ++i)
                    {
                        ChoosingGameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    lineNum--;
                }
                else if (line.StartsWith("@jump"))
                {
                    while (line == null || !line.StartsWith("@metka"))
                    {
                        lineNum++;
                        line = textArray[lineNum];
                    }
                }
                else if (line.StartsWith("@art"))
                {
                    ArtGameObject.SetActive(true);
                    TextGameObject.SetActive(false);
                    stopInput = true;
                }
=======
				else if(line.StartsWith("@hide"))
				{
					tempStrArray = line.Split(' ');
					string person = tempStrArray[1];
					CharacterManager.instance.HideCharacter(person);
				}
				else if(line.StartsWith("@bg"))
				{
					Debug.Log(line + " - " + line.Length);
					tempStrArray = line.Split(' ');
					string bg = tempStrArray[1];
					GameObject b = Instantiate(BGGameObject);
					b.GetComponent<Background>().SetImage(bg);
				}
				else if(line.StartsWith("@Music"))
				{
					tempStrArray = line.Split(' ');
					music.clip = Resources.Load<AudioClip>("Music/" + tempStrArray[1]);
					music.Play();
				}
				else if(line.StartsWith("@Sound"))
				{
					tempStrArray = line.Split(' ');
					AudioClip a = Resources.Load<AudioClip>("Sounds/" + tempStrArray[1]);
					sound.PlayOneShot(a, 1);
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
				else if(line.StartsWith("@art"))
				{
					ArtGameObject.SetActive(true);
					TextGameObject.SetActive(false);
					stopInput = true;
				}
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
                else
                {
                    Debug.Log("Такой команды не существует: " + line);
                }
            }
<<<<<<< HEAD
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
        else
        {
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
=======
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
		processed = 0;
	}
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765

    private void Update()
    {
        if (waitingForInput && !stopInput)
        {
<<<<<<< HEAD
            additiveSpeech = speechText.Split(new string[] { "&&" }, StringSplitOptions.None);
=======
            //additiveSpeech = speechText.Split(new string[] { "&&" }, StringSplitOptions.None); 
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
            characterName.Trim();
            if (Input.GetMouseButtonDown(0))
            {
                if (DialogueSystem.instance.IsSpeaking)
                {
                    DialogueSystem.instance.StopSpeaking();
                }
                else
                {
					Debug.Log(processed + " - " + additiveSpeech.Length);
					if (processed == additiveSpeech.Length - 1)
					{
						lineNum++;
						waitingForInput = false;
					}
					else
					{
						Debug.Log(processed + " - " + additiveSpeech.Length);
						processed++;
						additiveSpeech[processed].TrimStart();
						DialogueSystem.instance.Say(additiveSpeech[processed], characterName, (processed != 0));
					}
                }
				
            }
        }
        else if (lineNum < textArray.Length && !stopInput)
        {
            line = textArray[lineNum];
<<<<<<< HEAD

=======
	
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
            while ((line == null) && (lineNum < textArray.Length - 1))
            {
                Debug.Log(null);
                lineNum++;
                line = textArray[lineNum];
            }
            line.Trim();
			
			//Из-за корявого разделения (не знаю с чем связано) убираю последний символ в строке
			if(line.Length > 1) line = line.Substring(0, line.Length - 1);

            //Из-за корявого разделения (не знаю с чем связано) убираю последний символ в строке
            if (line.Length > 1) line = line.Substring(0, line.Length - 1);

            if (line.StartsWith("@"))
            {
                //tempStrArray = line.Split(' ');

                if (line.StartsWith("@moveTo") || line.StartsWith("@show"))
                {
                    tempStrArray = line.Split(' ');
                    Character targetCharacter = CharacterManager.instance.GetCharacter(tempStrArray[1]);

                    if (line.StartsWith("@moveTo"))
                    {

                        Vector2 targetVector;
                        targetVector.x = (float)Convert.ToDouble(tempStrArray[2]);
                        targetVector.y = (float)Convert.ToDouble(tempStrArray[3]);
                        targetCharacter.MoveTo(targetVector, 1);
                    }
                    else if (line.StartsWith("@show"))
                    {
                        string body = tempStrArray[2];
                        targetCharacter.SetBody(tempStrArray[2]);
                        //targetCharacter.SetExpression(tempStrArray[3]);
                    }
                }
<<<<<<< HEAD
                else if (line.StartsWith("@hide"))
                {
                    tempStrArray = line.Split(' ');
                    string person = tempStrArray[1];
                    CharacterManager.instance.HideCharacter(person);
                }
                else if (line.StartsWith("@bg"))
                {
                    Debug.Log(line + " - " + line.Length);
                    tempStrArray = line.Split(' ');
                    string bg = tempStrArray[1];
                    GameObject b = Instantiate(BGGameObject);
                    b.GetComponent<Background>().SetImage(bg);
                }
                else if (line.StartsWith("@music"))
                {
                    tempStrArray = line.Split(' ');
                    music.clip = Resources.Load<AudioClip>("Music/" + tempStrArray[1]);
                    music.Play();
                }
                else if (line.StartsWith("@sound"))
                {
                    tempStrArray = line.Split(' ');
                    AudioClip a = Resources.Load<AudioClip>("Sounds/" + tempStrArray[1]);
                    sound.PlayOneShot(a, 1);
                }
                else if (line.StartsWith("@choosing"))
                {
                    stopInput = true;
                    tempStrArray = line.Split(' ');
                    TextGameObject.SetActive(false);
                    ChoosingGameObject.SetActive(true);
                    int countActive = int.Parse(tempStrArray[1]);
                    for (int i = 0; i < countActive; ++i)
                    {
                        ChoosingGameObject.transform.GetChild(i).gameObject.SetActive(true);
                        //Это условие дополню еще попозже
                        if (int.Parse(tempStrArray[2 + i]) != 0) ChoosingGameObject.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 1);
                        ChoosingGameObject.transform.GetChild(i).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = tempStrArray[2 + countActive + i];
                    }
                    for (int i = countActive; i < 3; ++i)
                    {
                        ChoosingGameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    lineNum--;
                }
                else if (line.StartsWith("@jump"))
                {
                    while (line == null || !line.StartsWith("@metka"))
                    {
                        lineNum++;
                        line = textArray[lineNum];
                    }
                }
                else if (line.StartsWith("@art"))
                {
                    ArtGameObject.SetActive(true);
                    TextGameObject.SetActive(false);
                    stopInput = true;
                }
=======
				else if(line.StartsWith("@hide"))
				{
					tempStrArray = line.Split(' ');
					string person = tempStrArray[1];
					CharacterManager.instance.HideCharacter(person);
				}
				else if(line.StartsWith("@bg"))
				{
					Debug.Log(line + " - " + line.Length);
					tempStrArray = line.Split(' ');
					string bg = tempStrArray[1];
					GameObject b = Instantiate(BGGameObject);
					b.GetComponent<Background>().SetImage(bg);
				}
				else if(line.StartsWith("@Music"))
				{
					tempStrArray = line.Split(' ');
					music.clip = Resources.Load<AudioClip>("Music/" + tempStrArray[1]);
					music.Play();
				}
				else if(line.StartsWith("@Sound"))
				{
					tempStrArray = line.Split(' ');
					AudioClip a = Resources.Load<AudioClip>("Sounds/" + tempStrArray[1]);
					sound.PlayOneShot(a, 1);
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
				else if(line.StartsWith("@art"))
				{
					ArtGameObject.SetActive(true);
					TextGameObject.SetActive(false);
					stopInput = true;
				}
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
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
<<<<<<< HEAD
                }
                else
                {
=======
					additiveSpeech = speechText.Split(new string[] { "&&" }, StringSplitOptions.None); 
					additiveSpeech[processed].TrimStart();
					DialogueSystem.instance.Say(additiveSpeech[processed], characterName, (processed != 0));
                } else {
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
                    Debug.Log("line: \"" + line + "\"");
                    Debug.Log(line == null);
                }
                DebugUI.instance.Clear();
                DebugUI.instance.WriteLine("Current Line is " + lineNum);
            }
        }
    }
<<<<<<< HEAD

    public void PressButton(GameObject button)
    {
        if (button.GetComponent<UnityEngine.UI.Image>().color == new Color(0, 0, 0, 1))
        {
            button.SetActive(false);
        }
        else
        {
            bool isFind = false;
            line = textArray[lineNum];
            while ((line == null || !line.StartsWith("@choose")) || !isFind)
            {
                if (line != null && line.StartsWith("@choose"))
                {
                    string[] _tempStrArray = line.Split(new string[] { " " }, StringSplitOptions.None);

                    Debug.Log(_tempStrArray.Length);

                    if (_tempStrArray[1].Trim() == button.name)
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
            else
            {
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

    public void PressButtonArt()
    {
        lineNum++;
        ArtGameObject.SetActive(false);
        TextGameObject.SetActive(true);
        stopInput = false;
    }
}
=======
	
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
		processed = 0;
	}
	
	public void PressButtonArt()
	{
		lineNum++;
		ArtGameObject.SetActive(false);
		TextGameObject.SetActive(true);
		stopInput = false;
		processed = 0;
	}
}
>>>>>>> cecb6d96a12733f6dafbdae5727dc994be0eb765
