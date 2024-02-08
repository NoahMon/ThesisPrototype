using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionPopUp : MonoBehaviour
{
    public GameObject popUpPanel;
    public MonoBehaviour fpsController;
    public GameObject question;
    private bool correctAnswer;
    public bool playerChoice;
    public static QuestionPopUp Instance { get; private set; }
    private List<int> askedQuestions = new List<int>();
    private int totalQuestions = 14;
    private int correctCount = 0;

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
    public void DisplayQuestion()
    {
        if (askedQuestions.Count >= totalQuestions)
        {
            EvaluatePerformance();
            return;
        }

        TextMeshProUGUI Question = question.GetComponent<TextMeshProUGUI>();
        int randomInt = GetUniqueQuestionIndex();

        popUpPanel.SetActive(true);
        ToggleCursorState(true);
        ToggleFPSController(false);

        switch (randomInt)
        {
            case 1:
                Question.text = "50% is 1/2?";
                correctAnswer = true;
                break;
            case 2:
                Question.text = "Is 90° a right angle?";
                correctAnswer = true;
                break;
            case 3:
                Question.text = "60KG is 600,000 grams?";
                correctAnswer = false;
                break;
            case 4:
                Question.text = "1 Litre is 1000 millilitres?";
                correctAnswer = true;
                break;
            case 5:
                Question.text = "Is 1/2 > 1/4?";
                correctAnswer = true;
                break;
            case 6:
                Question.text = "Is 1000mm 1M?";
                correctAnswer = true;
                break;
            case 7:
                Question.text = "Are these multiples of 9: 9,36,47,90";
                correctAnswer = true;
                break;
            case 8:
                Question.text = "Is 4 parts out of a whole 5 equal to 5/4?";
                correctAnswer = false;
                break;
            case 9:
                Question.text = "Is 10,000,000 a million?";
                correctAnswer = false;
                break;
            case 10:
                Question.text = "Is there 1 face on a sphere?";
                correctAnswer = false;
                break;
            case 11:
                Question.text = "Does a cone have 1 face and 1 curved surface?";
                correctAnswer = true;
                break;
            case 12:
                Question.text = "If I had €20 and spent 60c, 20c and €5 does that mean I have €14.60 left?";
                correctAnswer = false;
                break;
            case 13:
                Question.text = "If I saved up €1 everyday for 2 weeks would I have €14 total after 2 weeks?";
                correctAnswer = true;
                break;
            case 14:
                Question.text = "Is South 180 degrees of North?";
                correctAnswer = true;
                break;
            default:
                Question.text = "Invalid question index.";
                correctAnswer = false;
                break;
        }
    }
    private void EvaluatePerformance()
    {
        // Logic to load different scenes based on the player's performance
        string sceneToLoad = correctCount >= 10 ? "WinScene" : "StaticMaze";
        SceneManager.LoadScene(sceneToLoad);
    }
    private int GetUniqueQuestionIndex()
    {
        int randomInt;
        do
        {
            randomInt = UnityEngine.Random.Range(1, 15); 
        } while (askedQuestions.Contains(randomInt));

        askedQuestions.Add(randomInt);
        return randomInt;
    }
    public void OnButtonClicked(bool playerChoice)
    {
        popUpPanel.SetActive(false);
        ToggleCursorState(false);
        ToggleFPSController(true);

        if (playerChoice == correctAnswer)
        {
            correctCount++; // Increment correct answer count
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }

        // After responding, check if all questions have been asked and evaluate performance
        if (askedQuestions.Count >= totalQuestions)
        {
            EvaluatePerformance();
        }

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
    }


}
