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
    public GameObject Pon;
    public GameObject Poff;
    public GameObject QLeft;
    private int Qno = 14;
    int randomInt;
    private float sceneChangeTimestamp;
    private Dictionary<int, string> questionTopics = new Dictionary<int, string>();
    public GameObject results;
    public GameObject Image;

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
        currentSceneName = SceneManager.GetActiveScene().name;
        Pon.gameObject.SetActive(false);
        Poff.gameObject.SetActive(false);
        Image.SetActive(false);
        sceneChangeTimestamp = Time.time;
    }

    public void QuestionPon()
    {
        Pon.gameObject.SetActive(true);
        StartCoroutine(Wait1S());
    }
    public void QuestionPoff()
    {
        Poff.gameObject.SetActive(true);
        StartCoroutine(Wait1S());
    }

    private IEnumerator Wait1S()
    {
        yield return new WaitForSeconds(1f);
        Pon.gameObject.SetActive(false);
        Poff.gameObject.SetActive(false);
    }
    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        float timeSpentOnPreviousScene = Time.time - sceneChangeTimestamp;
        Debug.Log($"Time spent on {currentSceneName}: {timeSpentOnPreviousScene} seconds");

        // Update the current scene name and timestamp
        currentSceneName = scene.name;
        sceneChangeTimestamp = Time.time;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ResetValues()
    {
        correctCount = 0;
        totalQuestions = 15;
        askedQuestions.Clear();
        Qno = 14;
        TextMeshProUGUI qLeft = QLeft.GetComponent<TextMeshProUGUI>();
        qLeft.text = Qno + "/14 Questions Left";
    }
    public void DisplayQuestion()
    {
        if (askedQuestions.Count >= totalQuestions)
        {
            EvaluatePerformance();
            return;
        }

        TextMeshProUGUI Question = question.GetComponent<TextMeshProUGUI>();
        randomInt = GetUniqueQuestionIndex();

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
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 2:
                    Question.text = "Is 100,000 > 1,000,000?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 3:
                    Question.text = "Are these multiples of 2: 2,4,6,8,9";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Multiplication";
                    break;
                case 4:
                    Question.text = "Is 1/4 0.25?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Division";
                    break;
                case 5:
                    Question.text = "Is 10 the nearest whole number to 9.5?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Decimals";
                    break;
                case 6:
                    Question.text = "Is 5-2.5=2.5?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Decimals";
                    break;
                case 7:
                    Question.text = "Does 255 round up to 250 to the nearest ten?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Rounding";
                    break;
                case 8:
                    Question.text = "10c is €0.1?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Money";
                    break;
                case 9:
                    Question.text = "I have €10 and I buy 2 things worth of 75c do I have €8.50 change left?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Subtraction";
                    break;
                case 10:
                    Question.text = "120 seconds is 3 minutes?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Time";
                    break;
                case 11:
                    Question.text = "Is 2 hours 130 minutes?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Time";
                    break;
                case 12:
                    Question.text = "Are there 3 sides to a triangle?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Shapes";
                    break;
                case 13:
                    Question.text = "A Cube has 7 faces?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Shapes";
                    break;
                case 14:
                    Question.text = "Is a cone a 2d shape?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Shapes";
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
                    questionTopics[randomInt] = "Multiplication";
                    break;
                case 2:
                    Question.text = "420 ÷ 2 = 220?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Division";
                    break;
                case 3:
                    Question.text = "Is 15/25 simplified is 3/5?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Fractions";
                    break;
                case 4:
                    Question.text = "Are the first 3 common multiples of 3 and 6: 6,12,30?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Multiplication";
                    break;
                case 5:
                    Question.text = "Is 5/3 equal to 15/9?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Fractions";
                    break;
                case 6:
                    Question.text = "Is 10cl = 100ml?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Capacity";
                    break;
                case 7:
                    Question.text = "Is 56 ÷ 1000 = 0.056 kg?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Weight";
                    break;
                case 8:
                    Question.text = "If there are 7 bones to share between 2 puppies would each puppy get 3 bones and 1 would be the remainder?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Division";
                    break;
                case 9:
                    Question.text = "Is 19 ÷ 5 = 3 R 3?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Division";
                    break;
                case 10:
                    Question.text = "If a car can hold 5 children and there are 23 children going to the picnic, would the car have to make 4 trips total?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Division";
                    break;
                case 11:
                    Question.text = "Are there 366 days in a normal year?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Time";
                    break;
                case 12:
                    Question.text = "Is North 360° from North?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 13:
                    Question.text = "Is half a right angle 54°?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 14:
                    Question.text = "The three angles in any triangle always add up to 180°";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
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
                    questionTopics[randomInt] = "Fractions";
                    break;
                case 2:
                    Question.text = "Is 90° a right angle?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 3:
                    Question.text = "60KG is 600,000 grams?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Mass";
                    break;
                case 4:
                    Question.text = "1 Litre is 1000 millilitres?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Capacity";
                    break;
                case 5:
                    Question.text = "Is 1/2 > 1/4?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Fractions";
                    break;
                case 6:
                    Question.text = "Is 1000mm 1M?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 7:
                    Question.text = "Are these multiples of 9: 9,36,45,90";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Multiplication";
                    break;
                case 8:
                    Question.text = "Is 4 parts out of a whole 5 equal to 5/4?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Fractions";
                    break;
                case 9:
                    Question.text = "Is 10,000,000 a million?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 10:
                    Question.text = "Is there 1 face on a sphere?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Shapes";
                    break;
                case 11:
                    Question.text = "Does a cone have 1 face and 1 curved surface?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Shapes";
                    break;
                case 12:
                    Question.text = "If I had €20 and spent 60c, 20c and €5 does that mean I have €14.60 left?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Subtraction";
                    break;
                case 13:
                    Question.text = "If I saved up €1 everyday for 2 weeks would I have €14 total after 2 weeks?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Multiplication";
                    break;
                case 14:
                    Question.text = "Is South 180 degrees of North?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
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
                    questionTopics[randomInt] = "Angles";
                    break;
                case 2:
                    Question.text = "A Rectangular Pyramid has 5 faces?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Shapes";
                    break;
                case 3:
                    Question.text = "Is 9 a square number?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 4:
                    Question.text = "Is 49 the square number of 8?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 5:
                    Question.text = "If a €70 phone had a 50% off would the new price be €35?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Division";
                    break;
                case 6:
                    Question.text = "Are there 4 months which have 30 days in them?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Time";
                    break;
                case 7:
                    Question.text = "Is 9:00 p.m. = 21:00?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Time";
                    break;
                case 8:
                    Question.text = "If a box is 8cm wide by 2cm long would the area be 16cm squared?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Area";
                    break;
                case 9:
                    Question.text = "Are there 2 weeks in a fortnight?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Time";
                    break;
                case 10:
                    Question.text = "Does an Octagonal Prism have 25 edges?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 11:
                    Question.text = "An equilateral triangle ahs 3 equal sides?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 12:
                    Question.text = "Is 10% of 650=65?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Percentages";
                    break;
                case 13:
                    Question.text = "3/4Kg =850g?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Mass";
                    break;
                case 14:
                    Question.text = "1dl =1000ml?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Capacity";
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
                    questionTopics[randomInt] = "Angles";
                    break;
                case 2:
                    Question.text = "The angles around a point add up to 360?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 3:
                    Question.text = "Is the mean of 50,100,150,200 equal to 125?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 4:
                    Question.text = "Is an acute angle 30°?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 5:
                    Question.text = "Is  6:00 pm equal to 20:00";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Time";
                    break;
                case 6:
                    Question.text = "Would the area of a square 2m by 2m be 6 meters squared";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Area";
                    break;
                case 7:
                    Question.text = "Is 3·4 × 3 = (3 × 3) + (0·4 × 3)?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 8:
                    Question.text = "Is 543-234 = 308?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Subtraction";
                    break;
                case 9:
                    Question.text = "Is 1,000,000 × 0 = 1";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Multiplication";
                    break;
                case 10:
                    Question.text = "The angles on a straight line add up to 190°";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 11:
                    Question.text = "If I look Noth and turn 90 degrees to the left would that mean im now looking to the East?";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Angles";
                    break;
                case 12:
                    Question.text = "Is 1kg of feathers the same weight as 1000 grams of Heavy Metal?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Mass";
                    break;
                case 13:
                    Question.text = "Is the average of 10,100,1000,10000, 100000 equal to 1000";
                    correctAnswer = false;
                    questionTopics[randomInt] = "Numbers";
                    break;
                case 14:
                    Question.text = "If i left home at 8:00 am , got the bus at 8:20 am and arrived to school at 8:45am, would that be a 25 minute bus ride to school?";
                    correctAnswer = true;
                    questionTopics[randomInt] = "Time";
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
        if (counter < 2)
        {
            string sceneToLoad = DetermineNextScene();

            currentSceneName = SceneManager.GetActiveScene().name;
            ResetValues();
            counter++;
            StartCoroutine(LoadSceneAfterDelay(sceneToLoad));
        }
        else
        {
            StartCoroutine(AnotherDelay());
            SceneManager.LoadScene("EndScene");
        }
    }

    private IEnumerator AnotherDelay()
    {
        yield return new WaitForSeconds(1f);
        DisplayTopics();
    }
    private string DetermineNextScene()
    {
       /* if (currentSceneName == "Easy")
            return correctCount >= 10 ? "EndScene" : "EndScene";*/
        if (currentSceneName == "MediumEasy")
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
        yield return new WaitForSeconds(0.5f);
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
            Qno--;
            TextMeshProUGUI qLeft = QLeft.GetComponent<TextMeshProUGUI>();
            qLeft.text = Qno+"/14 Questions Left";
        }
        else
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            Qno--;
            TextMeshProUGUI qLeft = QLeft.GetComponent<TextMeshProUGUI>();
            qLeft.text = Qno + "/14 Questions Left";
            Debug.Log(currentSceneName + ""+ randomInt);
        }

        // After responding, check if all questions have been asked and evaluate performance
        if (askedQuestions.Count >= totalQuestions)
        {
            EvaluatePerformance();
        }

    }
    private void DisplayTopics()
    {
        if (currentSceneName == "EndScene")
        {
            Image.SetActive(true);
            TextMeshProUGUI topicText = results.GetComponent<TextMeshProUGUI>();

            topicText.text = "Incorrect Topics:\n";

            HashSet<string> uniqueTopics = new HashSet<string>();

            foreach (var entry in questionTopics)
            {
                // Check if the topic is not already in the set
                if (uniqueTopics.Add(entry.Value))
                {
                    topicText.text += $"{entry.Value}\n";
                }
            }

            // Display total time since the start of the application
            float totalTime = Time.time;
            topicText.text += $"\nTotal Time: {totalTime:F2} seconds";
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
