using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName ="items/item")]
public class BaseItem : ScriptableObject {

    public string itemName;
    public string itemDescription;
    public Texture2D itemTexture;
    [Min(0)]
    public int cost;

}
