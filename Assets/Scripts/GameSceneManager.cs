using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
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

    int tutorialNum = 0;
    string[] tutorialStrings = {
        "Computers store data as binary numbers.\n\nBinary numbers are made up of 0s and 1s.\n\nThey looks like this:\n\n00101\n10110\n11001",
        "Computers send data back and forth across the world. Sometimes this data gets corrupted.\n\nHow does a computer tell if data is good or bad? How does a computer tell if data is corrupted?\n\nOne method is a parity bit.",
        "A parity bit is a 0 or 1 added to the end of a binary number. For example, using even parity, a 0 or 1 is added to the end of a binary number so that ALL of the digits add up to an EVEN number.\n\n0001 becomes 00011\n0011 becomes 00110",
        "When a computer receives data, it checks each binary number. If the digits add up to an even number, the data is likely still good. If the digits add up to an odd number, the data is bad.\n\n00011 is GOOD because...\n0+0+1+1=2 which is EVEN\n\n0010 is BAD because...\n0+0+1+0=1 which is ODD",
        "Now you try! A parity bit has been added to our binary numbers. The sum of the digits should be an even number. Act fast and decide if the information is good or bad.\n\nReady?"
    };


    // Start is called before the first frame update
    void Start()
    {
        HUDTitle.GetComponent<MoveNormal>().MoveDown();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTutorial()
    {
        HUDTitle.GetComponent<MoveNormal>().MoveUp();
        HUDIntroAndStart.GetComponent<MoveNormal>().MoveDown();
        HUDTutorial.GetComponent<MoveNormal>().MoveUp();
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
            // next tutorial
            HUDTutorialText.text = tutorialStrings[tutorialNum];
            if (tutorialNum == tutorialStrings.Length - 1)
                HUDTutorialButtonText.text = "BEGIN";
        }
    }

    public void StartGame()
    {
        HUDTutorial.GetComponent<MoveNormal>().MoveDown();
    }
}
