using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/CharacterData")]
public class CharacterData : ScriptableObject {

    public string chrName;
    public Color characterColour;
    public Texture2D background;

}
