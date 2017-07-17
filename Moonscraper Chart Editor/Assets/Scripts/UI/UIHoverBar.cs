﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHoverBar : MonoBehaviour {
    public GameObject[] uiElements;
    int prevElement = -1;
    int currentElement = -1;
    Dropdown lastShownDropdown = null;
    bool inUIBar = false;
	
	// Update is called once per frame
	void Update () {
        bool menuBarObjectUnderMouse = false;

        if (Globals.applicationMode != Globals.ApplicationMode.Loading && inUIBar)
        {
            // Get a list of all the objects currently under the mouse to get past the blocker
            GameObject[] currentHoveringObjects = GetObjectsUnderMouse();

            // Check if mouse is hovering over an associated object
            foreach (GameObject objectUnderPointer in currentHoveringObjects)
            {
                for(int i = 0; i < uiElements.Length; ++i)
                {
                    if ((uiElements[i] == objectUnderPointer) || objectUnderPointer.transform.parent.gameObject == uiElements[i])
                    {
                        currentElement = i;
                        menuBarObjectUnderMouse = true;
                        break;
                    }
                }

                if (menuBarObjectUnderMouse)
                    break;
            }

            // Exit if not in a dropdown associated with the collected gameobjects or not hovering over an associated object
            if (!menuBarObjectUnderMouse)
                currentElement = -1;
            else if (prevElement != currentElement)
            {
                Dropdown dropdown = null;

                // Auto-switch dropdown on hover
                if (lastShownDropdown && lastShownDropdown.gameObject != uiElements[currentElement].gameObject)
                    lastShownDropdown.Hide();

                EventSystem.current.SetSelectedGameObject(uiElements[currentElement].gameObject);

                dropdown = uiElements[currentElement].GetComponentInParent<Dropdown>();

                if (dropdown)
                {
                    dropdown.Show();
                    lastShownDropdown = dropdown;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !inUIBar)
        {
            GameObject[] currentHoveringObjects = GetObjectsUnderMouse();

            GameObject foundObject = null;
            // Check if mouse is hovering over an associated object
            foreach (GameObject objectUnderPointer in currentHoveringObjects)
            {
                for (int i = 0; i < uiElements.Length; ++i)
                {
                    if ((uiElements[i] == objectUnderPointer) || objectUnderPointer.transform.parent.gameObject == uiElements[i])
                    {
                        currentElement = i;
                        foundObject = uiElements[i];
                        break;
                    }
                }

                if (foundObject)
                    break;
            }

            if (foundObject)
            {
                inUIBar = true;

                // Set the last shown dropdown
                Dropdown dropdown = foundObject.GetComponentInParent<Dropdown>();
                if (dropdown)
                {
                    lastShownDropdown = dropdown;
                }
            }
        }
        // Properly deselect dropdown
        else if (Input.GetMouseButtonUp(0) && (EventSystem.current.currentSelectedGameObject || !menuBarObjectUnderMouse))
        {
            EventSystem.current.SetSelectedGameObject(null);
            inUIBar = false;
            currentElement = -1;
        }

        //Debug.Log(inUIBar);

        prevElement = currentElement;
    }

    GameObject[] GetObjectsUnderMouse()
    {
        GameObject[] currentHoveringObjects;

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        currentHoveringObjects = new GameObject[raycastResults.Count];
        for (int i = 0; i < raycastResults.Count; ++i)
        {
            currentHoveringObjects[i] = raycastResults[i].gameObject;
        }

        return currentHoveringObjects;
    }
}