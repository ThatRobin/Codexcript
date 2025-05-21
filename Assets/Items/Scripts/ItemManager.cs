using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    [SerializeField] public BaseItem item;

    void Start() {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        Texture2D tex = item.itemTexture;
        Sprite itemSprite = Sprite.Create(tex, new Rect(new Vector2(0, 0), new Vector2(tex.width,tex.height)), new Vector2(0.5f, 0.5f), tex.width);

        renderer.sprite = itemSprite;
    }
}
