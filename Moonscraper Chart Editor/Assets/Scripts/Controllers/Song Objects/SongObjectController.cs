﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public abstract class SongObjectController : MonoBehaviour {
    static int lastDeleteFrame = 0;
    bool deleteStart = false;

    protected const float CHART_CENTER_POS = 0;

    protected ChartEditor editor;
    private SongObject songObject = null;
    Renderer ren;

    public abstract void Delete();
    public abstract void UpdateSongObject();

    protected void Awake()
    {
        ren = GetComponent<Renderer>();
        editor = GameObject.FindGameObjectWithTag("Editor").GetComponent<ChartEditor>();
    }

    protected virtual void Update()
    {
        if (ren.isVisible || songObject != null && (songObject.position > editor.minPos && songObject.position < editor.maxPos))
            UpdateSongObject();
    }
    
    protected void OnBecameVisible()
    {
        UpdateSongObject();
    }

    protected void OnMouseEnter()
    {
        OnMouseOver();
    }

    protected void OnMouseOver()
    {
        // Delete the object on erase tool
        if (Toolpane.currentTool == Toolpane.Tools.Eraser && Input.GetMouseButton(0) && Globals.applicationMode == Globals.ApplicationMode.Editor)
        {
            if (!deleteStart)
                StartCoroutine(startDelete());
        }
    }

    IEnumerator startDelete()
    {
        deleteStart = true;

        while (Time.frameCount == lastDeleteFrame)
        {           
            yield return null;
        }

        lastDeleteFrame = Time.frameCount;
        Delete();
    }

    protected void Init(SongObject _songObject)
    {
        songObject = _songObject;
    }
}
