using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character {

    public static Character instance;

    private void Awake() {
        instance = this;
    }

    public Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    public string characterName;
    
    //The root object for all images related to the character in the scene. 
    public RectTransform root;


    // Begin Transformong Images\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

 
    
    public void SetBody(string bodyName)
    {
        if (spriteDictionary.ContainsKey(bodyName)) {
            renderers.bodyRenderer.sprite = spriteDictionary[bodyName];
        } else {
            Debug.Log("Body \"" + bodyName + "\" not found");
        }
    }
    
    public void SetExpression(string expressionName)
    {
        if (spriteDictionary.ContainsKey(expressionName)) {
            renderers.expressionRenderer.sprite = spriteDictionary[expressionName];
        } else {
            Debug.Log("Expression \"" + expressionName + "\" not found");
        }
    }

    // End Transformong Images\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


    // <sector>
    // Rendering

    public Renderers renderers = new Renderers();
    public bool isMultiLayer { get { return renderers.renderer == null; } }

    [System.Serializable]
    public class Renderers {
        // Used as the only image for single layer character.
        public RawImage renderer;

        // The body renderer for a multy layer character.
        public Image bodyRenderer;
        // The expression renderer for a multy layer character.
        public Image expressionRenderer;

    }
    // </sector>

    // <sector>
    //  Talking

    DialogueSystem dialogue;
    public void Say(string speech) {
        dialogue.Say(speech, characterName);
    }
    // </sector>

    public Character(string name) {
        Debug.Log("char constructor");
        CharacterManager cm = CharacterManager.instance;
        Debug.Log(Resources.Load(@"Prefabs/VN Characters/" + name).name);
        GameObject characterPref = Resources.Load(@"Prefabs/VN Characters/" + name) as GameObject;
        GameObject characterObj = GameObject.Instantiate(characterPref, cm.characterPanel);

        root = characterObj.GetComponent<RectTransform>();
        characterName = name;

        renderers.renderer = characterObj.GetComponentInChildren<RawImage>();
        if(isMultiLayer) {
            renderers.bodyRenderer = characterObj.transform.Find("bodyLayer").GetComponent<Image>();
            renderers.expressionRenderer = characterObj.transform.Find("expressionLayer").GetComponent<Image>();
        }

        dialogue = DialogueSystem.instance;

        Sprite[] spriteList = Resources.LoadAll<Sprite>("Sprites/" + characterName);
        foreach (var t in spriteList) {
            spriteDictionary.Add(t.name, t);
            Debug.Log("Sprite \"" + t.name + "\" loaded successfully!");
        }
        Debug.Log(spriteList.Length + " sprites loaded of character " + characterName);
    }


    // <sector>
    // Moving


    Vector2 targetPosition;

    Coroutine moving;
    bool isMoving { get { return moving != null; } }

    public void MoveTo(Vector2 pos, float speed, bool smooth = true) {
        StopMoving();
        moving = CharacterManager.instance.StartCoroutine(Moving(pos, speed, smooth));
    }

    public void StopMoving() {
        if (isMoving) {
            CharacterManager.instance.StopCoroutine(moving);
        }
        moving = null;
    }

    IEnumerator Moving(Vector2 target, float speed, bool smooth) {
        speed *= Time.deltaTime;

        Vector2 padding = root.anchorMax - root.anchorMin;

        Vector2 maxVector = new Vector2(1, 1) - root.anchorMax + root.anchorMin;

        Vector2 minAnchorTarget = new Vector2(maxVector.x * target.x, maxVector.y * target.y);

        while(root.anchorMin != minAnchorTarget) {
            root.anchorMin = (smooth) ? Vector2.Lerp(root.anchorMin, minAnchorTarget, speed) : Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
    }

    // </sector>
}
