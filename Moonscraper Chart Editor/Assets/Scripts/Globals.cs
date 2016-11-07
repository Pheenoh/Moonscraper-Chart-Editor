﻿using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
    public static float zoom = 0.01f;

    public static Sprite[] normalSprites { get; private set; }
    public static Sprite[] hopoSprites { get; private set; }
    public static Sprite[] tapSprites { get; private set; }

    [SerializeField]
    Sprite[] normalNotes = new Sprite[5];
    [SerializeField]
    Sprite[] hopoNotes = new Sprite[5];
    [SerializeField]
    Sprite[] tapNotes = new Sprite[5];

    void Awake()
    {
        normalSprites = normalNotes;
        hopoSprites = hopoNotes;
        tapSprites = tapNotes;
    }
}