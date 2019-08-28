using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem instance;
    public ELEMENTS elements;
    Coroutine speaking = null;
    
    public bool IsSpeaking { get { return speaking != null; } }
    public GameObject SpeechPanel { get { return elements.speechPanel; } }
    public Text SpeakerNameText { get { return elements.speakerNameText; } }
    public TextMeshProUGUI SpeechText { get { return elements.speechText; } }


    private void Awake() {
        instance = this;
    }

    public string displayedSpeech;
    private string targetSpeech;

    public void JumpTo(string speech, string speaker) {
        SpeechText.text = speech;
        SpeakerNameText.text = speaker;
    }

    public void Say(string speech, string speaker, bool additive = false) {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, speaker, additive));
    }

    public void StopSpeaking() {
        if (IsSpeaking && speaking != null) {
            StopCoroutine(speaking);
            SpeechText.text = targetSpeech;
        }
        speaking = null;
    }

    IEnumerator Speaking(string speech, string speaker, bool additive, float textSpeed = 3.5f) {
        SpeechPanel.SetActive(true);
        targetSpeech = speech;

        if (!additive) {
            SpeechText.text = "";
        } else {
            targetSpeech = SpeechText.text + speech;
        }
        SpeakerNameText.text = speaker;

        float lettersToWriteInNextFrame = 0;
        while (SpeechText.text != targetSpeech) {
            float lettersToWriteInFrame = textSpeed;
            while (lettersToWriteInFrame > 1)
            {
                SpeechText.text += targetSpeech[SpeechText.text.Length];
                lettersToWriteInFrame--;
                if (SpeechText.text == targetSpeech)
                    break;
            }
            lettersToWriteInNextFrame += lettersToWriteInFrame;
            yield return new WaitForEndOfFrame();
        }
        displayedSpeech = SpeechText.text;
        StopSpeaking();
    }
    
    // Close the entire speech panel. Stop all dialogues.
    public void Close() {
        StopSpeaking();
        SpeechPanel.SetActive(false);
    }
    

    [System.Serializable]
    public class ELEMENTS {
        public GameObject speechPanel;
        public Text speakerNameText;
        public TextMeshProUGUI speechText;

    }
}
