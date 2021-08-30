using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    public Sprite sprite;
    SpriteRenderer parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        parent.sprite = sprite;
        if(parent.sprite.name == "스프릿 이름1") parent.transform.localScale = new Vector3(5f, 5f, 0f);
        if (parent.sprite.name == "스프릿 이름2") parent.transform.localScale = new Vector3(1f, 1f, 0f);
    }
}
