using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    GameObject HUDTitle;
    [SerializeField]
    GameObject HUDIntroAndStart;
    [SerializeField]
    GameObject HUDTutorial;
    [SerializeField]
    TextMeshProUGUI HUDTutorialText;
    [SerializeField]
    TextMeshProUGUI HUDTutorialButtonText;

    [SerializeField]
    GameObject HUDGame;
    [SerializeField]
    TextMeshProUGUI HUDScore;
    [SerializeField]
    RectTransform HUDTimer;
    [SerializeField]
    TextMeshProUGUI BinaryNumber;
    [SerializeField]
    GameObject HUDCorrect;
    [SerializeField]
    GameObject HUDGameOver;
    [SerializeField]
    TextMeshProUGUI HUDGameOverText;
    [SerializeField]
    GameObject HUDReplay;
    [SerializeField]
    GameObject HUDPlayButtons;

    int numLength = 4;
    bool currentIsGood = true;
    int currentScore = 0;
    float gameTimer = 5f;
    bool isPlaying = false;
    float timerSizeMax = 380f;
    float gameTimerMax = 5f;

    float correctTimer = 0f;
    float correctTimerMax = 1f;

    int tutorialNum = 0;
    string[] tutorialStrings = {
        "Imagine you and your friends are passing secret notes in class using a complex code of letters, and you want to make sure that neither of you messes up the message. You decide on a rule that the count of the letters in your message is always an <b><color=#EEAA8F>even number.</color></b>\n\nIf your message has an odd number of letters, you add an extra <b><color=#EEAA8F>Z</color></b> at the end. If it has an even number of letters, you don’t add anything.\n\nNow, when your friend gets the note, they can quickly count the total letters. If the count is even it matches the rule and they know the message is correct. If the count is odd, they know something went wrong when writing the message.",

        "<b><color=#EEAA8F>Parity bits</color></b> at the end of digital information work the same way as that <b><color=#EEAA8F>extra Z</color></b>. Most things that you want to send or receive wirelessly these days, like a photo or a text, are encoded into binary format. This means the information is turned into a string of 0s and 1s.\n\nA parity bit at the end of a wireless signal helps the computer know it received the correct message. If the computer uses <b><color=#EEAA8F>even parity</color></b> to check for errors the rule is simple: the total number of 1s in the data (including the parity bit) should always be an even number.",

        "Let's say the data to send is: 1101.\n\n<b><color=#EEAA8F>Count the 1s:</color></b> In 1101, there are three 1s.\n<b><color=#EEAA8F>Determine the Parity Bit:</color></b> Since 3 is an odd number, and our rule is that we want an even number of 1s, the sender needs to add a parity bit with the value of 1 to make the total count even.\n<b><color=#EEAA8F>The Data to Send:</color></b> So, with the parity bit, the data becomes: 11011.",

        "Data to send is: 1101.\n\nNow, on the receiving side.\n\n<b><color=#EEAA8F>Receive and Count:</color></b> The computer gets the data 11011 and counts the 1s. There are four 1s.\n<b><color=#EEAA8F>Check the Parity Rule:</color></b> 4 is even, which is correct for the even parity rule. So, the computer knows that there were no errors in this data transmission.",

        "<b><color=#EEAA8F>But what if an error occurs?</color></b>\nWhat if due to some interference, the third bit accidentally changes from 0 to 1 during transmission? Now the receiver gets the data 11111.\n\n<b><color=#EEAA8F>Receive and Count:</color></b> The receiver counts five 1s.\n\n<b><color=#EEAA8F>Check the Parity Rule:</color></b> 5 is odd, which breaks our even parity rule. The receiver now knows that there was an error in transmission!\nWhile a parity bit can help receivers detect an error in a transmission, it doesn't tell a computer where the error occurred or how to correct it. More advanced error-checking methods can help with those problems.",

        "<b><color=#EEAA8F>Now you are the computer receiving the data!</color></b> A parity bit has been added to a string of binary numbers. The sum of the digits should be an even number. Act fast and decide if the digital information is good or bad.\n\nReady?"

    };

    [SerializeField]
    TextMeshProUGUI LanguageText;

    // Start is called before the first frame update
    void Start()
    {        
        Globals.LoadUserSettings();
        SelectLanguage(Globals.CurrentLanguage);

        audioManager = this.GetComponent<AudioManager>();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer < 0)
            {
                gameTimer = 0;
                HUDGameOverText.text = "OUT OF TIME!";
                GameOver();
            }
            HUDTimer.sizeDelta = new Vector2(timerSizeMax * gameTimer / gameTimerMax, HUDTimer.sizeDelta.y);
        }

        if (correctTimer > 0)
        {
            correctTimer -= Time.deltaTime;
            if (correctTimer < 0)
            {
                HUDCorrect.SetActive(false);
            }
        }
    }

    public void StartTutorial()
    {
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();
        HUDTutorial.GetComponent<MoveNormal>().MoveUp();
        audioManager.PlaySelectSound();
    }

    public void AdvanceTutorial()
    {
        tutorialNum++;
        if (tutorialNum >= tutorialStrings.Length)
        {
            // start game
            StartGame();
        }
        else
        {
            audioManager.PlayMenuSound();
            // next tutorial
            HUDTutorialText.text = tutorialStrings[tutorialNum];
            if (tutorialNum == tutorialStrings.Length - 1)
                HUDTutorialButtonText.text = "BEGIN";
        }
    }

    public void StartGame()
    {
        audioManager.PlaySelectSound();
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();

        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDReplay.GetComponent<MoveNormal>().MoveDown();
        currentScore = 0;
        numLength = 4;
        gameTimerMax = 5f;
        HUDScore.text = currentScore.ToString();
        GenerateNumber();
        HUDGame.GetComponent<MoveNormal>().MoveDown();
        HUDPlayButtons.GetComponent<MoveNormal>().MoveUp();
        isPlaying = true;
    }

    public void GenerateNumber()
    {
        string binaryNum = "";
        int binarySum = 0;
        BinaryNumber.text = "";
        for (int x = 0; x < numLength; x++)
        {
            int val = Random.Range(0, 2);
            if (val == 0)
            {
                binaryNum = binaryNum + "0";
            }
            else
            {
                binaryNum = binaryNum + "1";
                binarySum++;
            }
            BinaryNumber.text = x == numLength - 1
                ? BinaryNumber.text + "<color=#F8B195>" + val + "</color>"
                : BinaryNumber.text + val;
        }
        currentIsGood = binarySum % 2 == 0;
        gameTimer = gameTimerMax;
    }

    public void SelectGood()
    {
        if (currentIsGood)
        {
            CorrectAnswer();
        }
        else
        {
            HUDGameOverText.text = "WRONG!";
            GameOver();
        }
        HUDScore.text = currentScore.ToString();
    }
    public void SelectBad()
    {
        if (!currentIsGood)
        {
            CorrectAnswer();
        }
        else
        {
            HUDGameOverText.text = "WRONG!";
            GameOver();
        }
        HUDScore.text = currentScore.ToString();
    }

    public void CorrectAnswer()
    {
        audioManager.PlayCorrectSound();
        currentScore++;
        if (currentScore == 5 || currentScore == 10 || currentScore == 20 || currentScore == 40)
        {
            numLength++;
        }
        if (currentScore == 8 || currentScore == 16 || currentScore == 24 || currentScore == 32)
        {
            gameTimerMax = gameTimerMax - .5f;
        }
        HUDCorrect.transform.localScale = new Vector3(.1f, .1f, .1f);
        HUDCorrect.SetActive(true);
        HUDCorrect.GetComponent<GrowAndShrink>().StartEffect();
        correctTimer = correctTimerMax;
        GenerateNumber();
    }

    public void GameOver()
    {
        audioManager.PlayWrongSound();
        isPlaying = false;
        HUDGameOver.GetComponent<MoveNormal>().MoveDown();
        HUDReplay.GetComponent<MoveNormal>().MoveUp();
        HUDPlayButtons.GetComponent<MoveNormal>().MoveDown();
    }

    public void SelectHome()
    {
        audioManager.PlaySelectSound();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
        HUDGame.GetComponent<MoveNormal>().MoveUp();
        HUDPlayButtons.GetComponent<MoveNormal>().MoveUp();
    }

    public void ToggleLanguage()
    {
        audioManager.PlaySelectSound();
        if (Globals.CurrentLanguage == Globals.Language.English)
            SelectLanguage(Globals.Language.Spanish);
        else
            SelectLanguage(Globals.Language.English);
    }

    public void SelectLanguage(Globals.Language newLang)
    {
        Globals.CurrentLanguage = newLang;
        if (Globals.CurrentLanguage == Globals.Language.English)
            LanguageText.text = "ESPAÑOL";
        else
            LanguageText.text = "ENGLISH";

        TranslateText[] textObjects = GameObject.FindObjectsOfType<TranslateText>(true);
        for (int i = 0; i < textObjects.Length; i++)
        {
            textObjects[i].UpdateText();
        }

        TranslateImage[] imageObjects = GameObject.FindObjectsOfType<TranslateImage>(true);
        for (int i = 0; i < imageObjects.Length; i++)
        {
            imageObjects[i].UpdateImage();
        }

        Globals.SaveIntToPlayerPrefs(Globals.LanguageStorageKey, (int)newLang);
    }
}
