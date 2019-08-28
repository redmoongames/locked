using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    public static CharacterManager instance;

    private void Awake() {
        instance = this;
    }

    public RectTransform characterPanel;
    
    public List<Character> characters = new List<Character>();
    
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();
    

    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true) {
        if (characterDictionary.TryGetValue(characterName, out int index)) {
            return characters[index];
        } else {
            return CreateCharacter(characterName);
        }
    }
    
    public Character CreateCharacter(string characterName) {
        Character newCharacter = new Character(characterName);
        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;
    }
}
