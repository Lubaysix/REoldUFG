using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Letter {
    public string name;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "Font", menuName = "SprUtil/Font", order = 1)]
public class Font : ScriptableObject
{
    public string FontName;
    public List<Letter> FontSprites;
}
