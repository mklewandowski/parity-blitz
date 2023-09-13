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
        "Imagine you and your friends are passing secret messages in class using a complex code of letters, and you want to make sure that neither of you messed up writing a message. You decide on a rule that the count of the letters in your message is always even.\n\nIf your message has an odd number of letters, you add an extra Z at the end. If it has an even number of letters, you don’t add anything.\n\nNow when you get the secret message, you can quickly count the letters to check. If the count is even, it matches the rule and you can be more confident the message is correct. If the count is odd, you know something went wrong when writing the message.",

        "<b><color=#EEAA8F>Parity bits</color></b> at the end of digital information work in a similar way as that <b><color=#EEAA8F>extra z</color></b>. Most things that you want to send or receive wirelessly these days, like a photo or a text, are encoded into binary format. This means the information is turned into a string of 0s and 1s.\n\nA parity bit at the end of a wireless signal helps the computer know it received the correct message. If the receiving computer uses <b><color=#EEAA8F>even parity</color></b> to check for errors, the rule is simple: the total number of 1s in the data (including the parity bit) should always be an even number.",

        "Let's say the data to send is: 1101.\n\n<b><color=#EEAA8F>Count the 1s:</color></b> In 1101, there are three 1s.\n<b><color=#EEAA8F>Determine the Parity Bit:</color></b> Since 3 is an odd number, and our rule is that we want an even number of 1s, the sender needs to add a parity bit with the value of 1 to make the total count even.\n<b><color=#EEAA8F>The Data to Send:</color></b> So, with the parity bit, the data becomes: <b><color=#EEAA8F>11011</color></b>.",

        "Data to send is: 1101.\n\nNow, on the receiving side...\n\n<b><color=#EEAA8F>Receive and Count:</color></b> The receiving computer gets the data 11011 and counts the 1s. There are four 1s.\n<b><color=#EEAA8F>Check the Parity Rule:</color></b> 4 is even, which is correct for the even parity rule. So, the receiving computer knows that there were no errors in this data transmission.",

        "<b><color=#EEAA8F>But what if an error occurs?</color></b>\nWhat if due to some interference, the third bit accidentally changes from 0 to 1 during transmission? Now the receiving computer gets the data <b><color=#EEAA8F>11111</color></b>.\n\n<b><color=#EEAA8F>Receive and Count:</color></b> The receiving computer counts five 1s.\n\n<b><color=#EEAA8F>Check the Parity Rule:</color></b> 5 is odd, which breaks our even parity rule. The  receiving computer now knows that there was an error in transmission!\nWhile a parity bit can help receivers detect an error in a transmission, it doesn't show where the error occurred or how to correct it. More advanced error-checking methods can help with those problems.",

        "<b><color=#EEAA8F>Now you are the computer receiving the data!</color></b> A parity bit has been added to a string of binary numbers. The sum of the digits should be an even number. Act fast and decide if the digital information is good or bad.\n\nReady?"
    };

    string[] tutorialStringsSP = {
        "Imagina que tú y tus amigos están pasando mensajes secretos en clase usando un código de letras complejo y quieres asegurarte de que ninguno de ustedes se equivocó al escribir un mensaje. Entonces decides poner una regla para que la cantidad de letras en sus mensajes siempre sea par.\n\nSi el mensaje tiene una cantidad de letras impar, se añade una “Z” extra al final. Si la cantidad de letras es par, no se agrega nada.\n\nAhora, cuando recibes el mensaje secreto, puedes contar las letras rápidamente para revisar. Si la cantidad es par, coincide con la regla y puedes tener más confianza en que el mensaje es correcto. Si la cantidad es impar, sabes que algo salió mal al escribir el mensaje.",

        "Los <b><color=#EEAA8F>bits de paridad</color></b> al final de la información digital funcionan de una forma similar a esa <b><color=#EEAA8F>“Z” adicional</color></b>. La mayor parte de lo que quieres enviar o recibir de forma inalámbrica en estos días, como una foto o un texto, están codificados en formato binario. Esto significa que la información se convierte en una secuencia de ceros y unos.\n\nUn bit de paridad al final de una señal inalámbrica ayuda a la computadora a saber que recibió el mensaje correcto. Si la computadora usa una <b><color=#EEAA8F>paridad par</color></b> para buscar errores, la regla es simple: la cantidad total de números 1 en los datos (incluyendo el bit de paridad) siempre debe ser par.",

        "Digamos que los datos a enviar son: 1101.\n\n<b><color=#EEAA8F>Cuenta los números 1:</color></b> En 1101, hay tres números 1.\n<b><color=#EEAA8F>Determina el bit de paridad:</color></b> Como 3 es un número impar y nuestra regla es que queremos una cantidad par de números 1, quien envíe los datos necesita agregar un bit de paridad con el valor de 1 para hacer que la cantidad total sea par.\n<b><color=#EEAA8F>Los datos a enviar:</color></b> Así que, con el bit de paridad, los datos se vuelven: <b><color=#EEAA8F>11011</color></b>.",

        "Digamos que los datos a enviar son: 1101.\n\nAhora, en el lado de quien los recibe...\n\n<b><color=#EEAA8F>Recibe y cuenta:</color></b> La computadora de destino obtiene los datos 11011 y cuenta los números 1. Hay cuatro.\n<b><color=#EEAA8F>Revisa la regla de paridad:</color></b> El número 4 es par, lo cual es correcto para la regla de paridad par, así que la computadora destino sabe que no hubo errores en esta transmisión de datos.",

        "<b><color=#EEAA8F>¿Pero qué tal si ocurre un error?</color></b>\n¿Qué tal si, debido a alguna interferencia, el tercer bit cambia accidentalmente de 0 a 1 durante la transmisión? Ahora la computadora de destino obtiene los datos <b><color=#EEAA8F>11111</color></b>.\n\n<b><color=#EEAA8F>Recibe y cuenta:</color></b> La computadora de destino cuenta cinco números 1.\n\n<b><color=#EEAA8F>Revisa la regla de paridad:</color></b> El número 5 es impar, así que no cumple nuestra regla de paridad. ¡La computadora de destino “sabe” que hubo un error en la transmisión!\nAunque un bit de paridad puede ayudar a los destinatarios a detectar un error en una transmisión, no muestra dónde está el error o cómo corregirlo. Los métodos avanzados de revisión de errores pueden ayudar con esos problemas.",

        "<b><color=#EEAA8F>¡Ahora TÚ eres la computadora que recibe los datos!</color></b> Se ha agregado un bit de paridad a una secuencia de números binarios. La suma de los dígitos debe ser un número par. Actúa rápido y decide si la información digital es buena o mala.\n\n¿Listo?"
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
                {
                    if (Globals.CurrentLanguage == Globals.Language.English)
                        HUDGameOverText.text = "OUT OF TIME!";
                    else
                        HUDGameOverText.text = "FUERA DE TIEMPO";                
                }
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
        tutorialNum = 0;
        ConfigureTutorial();
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
            ConfigureTutorial();
        }
    }

    void ConfigureTutorial()
    {
        // next tutorial
        if (Globals.CurrentLanguage == Globals.Language.English)
            HUDTutorialText.text = tutorialStrings[tutorialNum];
        else
            HUDTutorialText.text = tutorialStringsSP[tutorialNum];
        if (tutorialNum == tutorialStrings.Length - 1)
        {
            if (Globals.CurrentLanguage == Globals.Language.English)
                HUDTutorialButtonText.text = "Play";
            else
                HUDTutorialButtonText.text = "Jugar";                
        }
        else
        {
            if (Globals.CurrentLanguage == Globals.Language.English)
                HUDTutorialButtonText.text = "Next";
            else
                HUDTutorialButtonText.text = "Siguiente";            
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
            if (Globals.CurrentLanguage == Globals.Language.English)
                HUDGameOverText.text = "WRONG!";
            else
                HUDGameOverText.text = "INCORRECTO";
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
            if (Globals.CurrentLanguage == Globals.Language.English)
                HUDGameOverText.text = "WRONG!";
            else
                HUDGameOverText.text = "INCORRECTO";
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
        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
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
