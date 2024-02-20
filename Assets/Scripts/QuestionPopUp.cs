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
    public int totalQuestions = 14;
    public int correctCount;
    string currentSceneName;
    int counter;
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
        Debug.Log(correctCount + "This one is in awake" + totalQuestions);
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
    }
    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ResetValues()
    {
        correctCount = 0;
        totalQuestions = 14;
        askedQuestions.Clear();
        Debug.Log("Values Reset: " + correctCount + " - " + totalQuestions);
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

        if (currentSceneName == "Easy")
        {
            switch (randomInt)
            {
                case 1:
                    Question.text = "Is one hundred fifty-one thousand in digits 151,000?";
                    correctAnswer = true;
                    break;
                case 2:
                    Question.text = "Is 100,000 > 1,000,000?";
                    correctAnswer = false;
                    break;
                case 3:
                    Question.text = "Are these multiples of 2: 2,4,6,8,9";
                    correctAnswer = false;
                    break;
                case 4:
                    Question.text = "Is 1/4 0.25?";
                    correctAnswer = true;
                    break;
                case 5:
                    Question.text = "Is 10 the nearest whole number to 9.5?";
                    correctAnswer = true;
                    break;
                case 6:
                    Question.text = "Is 5-2.5=2.5?";
                    correctAnswer = true;
                    break;
                case 7:
                    Question.text = "Does 255 round up to 250 to the nearest ten?";
                    correctAnswer = false;
                    break;
                case 8:
                    Question.text = "10c is €0.1?";
                    correctAnswer = true;
                    break;
                case 9:
                    Question.text = "I have €10 and I buy 2 things worth of 75c do I have €8.50 change left?";
                    correctAnswer = true;
                    break;
                case 10:
                    Question.text = "120 seconds is 3 minutes?";
                    correctAnswer = false;
                    break;
                case 11:
                    Question.text = "Is 2 hours 130 minutes?";
                    correctAnswer = false;
                    break;
                case 12:
                    Question.text = "Are there 3 sides to a triangle?";
                    correctAnswer = true;
                    break;
                case 13:
                    Question.text = "A Cube has 7 faces?";
                    correctAnswer = false;
                    break;
                case 14:
                    Question.text = "Is a cone a 2d shape?";
                    correctAnswer = false;
                    break;
                default:
                    Question.text = "Invalid question index.";
                    correctAnswer = false;
                    break;
            }
        }
        else if (currentSceneName == "MediumEasy")
        {
            switch (randomInt)
            {
                case 1:
                    Question.text = "7.36 x 10 =736?";
                    correctAnswer = true;
                    break;
                case 2:
                    Question.text = "420 ÷ 2 = 220?";
                    correctAnswer = false;
                    break;
                case 3:
                    Question.text = "Is 15/25 simplified is 3/5?";
                    correctAnswer = true;
                    break;
                case 4:
                    Question.text = "Are the first 3 common multiples of 3 and 6: 6,12,30?";
                    correctAnswer = false;
                    break;
                case 5:
                    Question.text = "Is 5/3 equal to 15/9?";
                    correctAnswer = true;
                    break;
                case 6:
                    Question.text = "Is 10cl = 100ml?";
                    correctAnswer = true;
                    break;
                case 7:
                    Question.text = "Is 56 ÷ 1000 = 0.056 kg?";
                    correctAnswer = true;
                    break;
                case 8:
                    Question.text = "If there are 7 bones to share between 2 puppies would each puppy get 3 bones and 1 would be the remainder?";
                    correctAnswer = true;
                    break;
                case 9:
                    Question.text = "Is 19 ÷ 5 = 3 R 3?";
                    correctAnswer = false;
                    break;
                case 10:
                    Question.text = "If a car can hold 5 children and there are 23 children going to the picnic, would the car have to make 4 trips total?";
                    correctAnswer = true;
                    break;
                case 11:
                    Question.text = "Are there 366 days in a normal year?";
                    correctAnswer = false;
                    break;
                case 12:
                    Question.text = "Is North 360° from North?";
                    correctAnswer = true;
                    break;
                case 13:
                    Question.text = "Is half a right angle 54°?";
                    correctAnswer = false;
                    break;
                case 14:
                    Question.text = "The three angles in any triangle always add up to 180°";
                    correctAnswer = true;
                    break;
                default:
                    Question.text = "Invalid question index.";
                    correctAnswer = false;
                    break;
            }
        }
        else if (currentSceneName == "Medium")
        {
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
                    Question.text = "Are these multiples of 9: 9,36,45,90";
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
                    correctAnswer = true;
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
        else if (currentSceneName == "HardMedium")
        {
            switch (randomInt)
            {
                case 1:
                    Question.text = "Is 190° a reflex angle?";
                    correctAnswer = true;
                    break;
                case 2:
                    Question.text = "A Rectangular Pyramid has 5 faces?";
                    correctAnswer = true;
                    break;
                case 3:
                    Question.text = "Is 9 a square number?";
                    correctAnswer = true;
                    break;
                case 4:
                    Question.text = "Is 49 the square number of 8?";
                    correctAnswer = false;
                    break;
                case 5:
                    Question.text = "If a €70 phone had a 50% off would the new price be €35?";
                    correctAnswer = true;
                    break;
                case 6:
                    Question.text = "Are there 4 months which have 30 days in them?";
                    correctAnswer = false;
                    break;
                case 7:
                    Question.text = "Is 9:00 p.m. = 21:00?";
                    correctAnswer = true;
                    break;
                case 8:
                    Question.text = "If a box is 8cm wide by 2cm long would the area be 16cm squared?";
                    correctAnswer = true;
                    break;
                case 9:
                    Question.text = "Are there 2 weeks in a fortnight?";
                    correctAnswer = true;
                    break;
                case 10:
                    Question.text = "Does an Octagonal Prism have 25 edges?";
                    correctAnswer = false;
                    break;
                case 11:
                    Question.text = "An equilateral triangle ahs 3 equal sides?";
                    correctAnswer = true;
                    break;
                case 12:
                    Question.text = "Is 10% of 650 65?";
                    correctAnswer = true;
                    break;
                case 13:
                    Question.text = "3/4Kg =850g?";
                    correctAnswer = false;
                    break;
                case 14:
                    Question.text = "1dl =1000ml?";
                    correctAnswer = false;
                    break;
                default:
                    Question.text = "Invalid question index.";
                    correctAnswer = false;
                    break;
            }
        }
        else if (currentSceneName == "Hard")
        {
            switch (randomInt)
            {
                case 1:
                    Question.text = "If the clock starts at 12 and the minute hand is rotated 90 degrees to the right would the minute hand be on the number 3?";
                    correctAnswer = true;
                    break;
                case 2:
                    Question.text = "The angles around a point add up to 360?";
                    correctAnswer = true;
                    break;
                case 3:
                    Question.text = "Is the mean of 50,100,150,200 equal to 125?";
                    correctAnswer = true;
                    break;
                case 4:
                    Question.text = "Is an acute angle 30°?";
                    correctAnswer = true;
                    break;
                case 5:
                    Question.text = "Is  6:00 pm equal to 20:00";
                    correctAnswer = false;
                    break;
                case 6:
                    Question.text = "Would the area of a square 2m by 2m be 6 meters squared";
                    correctAnswer = false;
                    break;
                case 7:
                    Question.text = "Is 3·4 × 3 = (3 × 3) + (0·4 × 3)?";
                    correctAnswer = true;
                    break;
                case 8:
                    Question.text = "Is 543-234 = 308?";
                    correctAnswer = false;
                    break;
                case 9:
                    Question.text = "Is 1,000,000 × 0 = 1";
                    correctAnswer = false;
                    break;
                case 10:
                    Question.text = "The angles on a straight line add up to 190°";
                    correctAnswer = false;
                    break;
                case 11:
                    Question.text = "If I look Noth and turn 90 degrees to the left would that mean im now looking to the East?";
                    correctAnswer = false;
                    break;
                case 12:
                    Question.text = "Is 1kg of feathers the same weight as 1000 grams of Heavy Metal?";
                    correctAnswer = true;
                    break;
                case 13:
                    Question.text = "Is the average of 10,100,1000,10000, 100000 equal to 1000";
                    correctAnswer = false;
                    break;
                case 14:
                    Question.text = "If i left home at 8:00 am , got the bus at 8:20 am and arrived to school at 8:45am, would that be a 25 minute bus ride to school?";
                    correctAnswer = true;
                    break;
                default:
                    Question.text = "Invalid question index.";
                    correctAnswer = false;
                    break;
            }
        }
    }
    private void EvaluatePerformance()
    {
        if (counter < 3)
        {
            string sceneToLoad = DetermineNextScene();

            currentSceneName = SceneManager.GetActiveScene().name;
            ResetValues();
            counter++;
            StartCoroutine(LoadSceneAfterDelay(sceneToLoad));
        }
        else
        {
            Debug.Log(counter);
            SceneManager.LoadScene("EndScene");
        }
    }

    private string DetermineNextScene()
    {
        if (currentSceneName == "Easy")
            return correctCount >= 10 ? "EndScene" : "EndScene";
        else if (currentSceneName == "MediumEasy")
            return correctCount >= 10 ? "Medium" : "Easy";
        else if (currentSceneName == "Medium")
            return correctCount >= 10 ? "HardMedium" : "MediumEasy";
        else if (currentSceneName == "HardMedium")
            return correctCount >= 10 ? "Hard" : "Medium";
        else if (currentSceneName == "Hard")
            return correctCount >= 10 ? "EndScene" : "EndScene";

        return "EndScene";
    }

    private IEnumerator LoadSceneAfterDelay(string sceneToLoad)
    {

        yield return new WaitForSeconds(1f);
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
            correctCount++;
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
