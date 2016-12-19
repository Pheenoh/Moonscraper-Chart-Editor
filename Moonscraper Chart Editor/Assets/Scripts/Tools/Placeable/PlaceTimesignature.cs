﻿using UnityEngine;
using System.Collections;

public class PlaceTimesignature : PlaceSongObject {
    public TimeSignature ts { get { return (TimeSignature)songObject; } set { songObject = value; } }

    protected override void Awake()
    {
        base.Awake();
        ts = new TimeSignature(editor.currentSong);

        controller = GetComponent<TimesignatureController>();
        ((TimesignatureController)controller).ts = ts;
    }

    protected override void Controls()
    {
        if (Toolpane.currentTool == Toolpane.Tools.Timesignature && Globals.applicationMode == Globals.ApplicationMode.Editor && Input.GetMouseButtonDown(0))
        {
            AddObject();
        }
    }

    protected override void AddObject()
    {
        TimeSignature tsToAdd = new TimeSignature(ts);
        editor.currentSong.Add(tsToAdd);
        editor.CreateTSObject(tsToAdd);

        // Only show the panel once the object has been placed down
        editor.currentSelectedObject = tsToAdd;
    }
}
