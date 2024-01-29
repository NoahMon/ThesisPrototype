using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPopUp : MonoBehaviour
{
    public GameObject popUpPanel;
    public MonoBehaviour fpsController;
    public GameObject question;
    public static QuestionPopUp Instance { get; private set; }

    private void Awake()
    {
        popUpPanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            popUpPanel.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnButtonClicked(bool playerChoice)
    {
        popUpPanel.SetActive(false);
        ToggleCursorState(false);
        ToggleFPSController(true);

        Debug.Log("Player chose: " + playerChoice);
    }

    public void ToggleCursorState(bool showCursor)
    {
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = showCursor;
    }

    public void ToggleFPSController(bool enable)
    {
        if (fpsController != null)
        {
            fpsController.enabled = enable;
        }
        // If you have other scripts or components that should be disabled, disable them here
    }


}
