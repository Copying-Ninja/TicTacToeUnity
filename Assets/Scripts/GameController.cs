using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isXTurn;            // true: X turn, false: O turn
    public int turnAmount;          // amount of turns in single game
    public GameObject[] turnImages; // displays whos turn
    public Sprite[] playerImages;   // 0 -> X img , 1 -> O img
    public Button[] playableSpaces; // spaces which can be singned X or O (free spaces)
    public int[] markedSpaces;      // nums of spaces which was marked by X or O
    const int minTurnsToWin = 4;
    public Text winnerText;         
    public GameObject[] winningLines;
    public GameObject winnerPanel;
    public int xPlayerScore = 0;
    public int oPlayerScore = 0;
    public Text xPlayerScoreText;
    public Text oPlayerScoreText;
    public GameObject mainMenuPanel;
    public GameObject choicePanel;

    public bool isVSComputer;
    public int pcSide; // 0 -> X, 1 -> O


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
        if(isVSComputer)
        {
            GameVsComputer();
        }
    }

    void GameSetup()
    {
        isXTurn = true;
        turnAmount = 0;
        turnImages[0].SetActive(true);
        turnImages[1].SetActive(false);
        foreach(var ps in playableSpaces)
        {
            ps.interactable = true;
            ps.GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
        foreach (var vl in winningLines)
        {
            vl.SetActive(false);
        }
        winnerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int GetPlayerNum()
    {
        return isXTurn ? 0 : 1;
    }

    public void PlayableButton(int clickedButton)
    {
        playableSpaces[clickedButton].image.sprite = playerImages[GetPlayerNum()];
        playableSpaces[clickedButton].interactable = false;

        markedSpaces[clickedButton] = GetPlayerNum() + 1;
        turnAmount++;

        if(turnAmount > minTurnsToWin)
        {
            WinnerCheck();
            if(turnAmount == 9 && !WinnerCheck())
            {
                Draw();
            }
        }

        isXTurn = !isXTurn;
        turnImages[0].SetActive(isXTurn);
        turnImages[1].SetActive(!isXTurn);

    }

    bool WinnerCheck()
    {
        int winPos1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int winPos2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int winPos3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];

        int winPos4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int winPos5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int winPos6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];

        int winPos7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int winPos8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new List<int>() { winPos1, winPos2, winPos3, winPos4, winPos5, winPos6, winPos7, winPos8 };

        foreach(var wp in solutions )
        {
            if(wp == 3*(GetPlayerNum() + 1))
            {
                WinnerDisplay(solutions.IndexOf(wp));
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay( int winPosIndex)
    {
        winnerPanel.gameObject.SetActive(true);
        if(isXTurn)
        {
            xPlayerScoreText.text = (++xPlayerScore).ToString();
            winnerText.text = "Player X Wins !";
        }
        else
        {
            oPlayerScoreText.text = (++oPlayerScore).ToString();
            winnerText.text = "Player O Wins !";
        }
        winningLines[winPosIndex].SetActive(true);
    }

    public void PlayAgain()
    {
        GameSetup();      
    }

    public void Restart()
    {
        GameSetup();
        xPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScore = 0;
        oPlayerScoreText.text = "0";
    }

    void Draw()
    {
        winnerPanel.SetActive(true);
        winnerText.text = "DRAW";
    }

    public void PlayVSPlayer()
    {
        mainMenuPanel.SetActive(false);
    }
    public void PlayVSComputer()
    {
        choicePanel.SetActive(true);
        isVSComputer = true;
    }

    public void XButtonClick()
    {
        pcSide = 1;
        mainMenuPanel.SetActive(false);
    }
    public void OButtonClick()
    {
        pcSide = 0;
        mainMenuPanel.SetActive(false);
    }
    void GameVsComputer()
    {
        if(pcSide == 0)
        {
            //ComputerMove();
        }
        else
        {
            //ComputerMove();
        }
    }
/*    void ComputerMove()
    {     
        var range = Enumerable.Range(0, 9).Where(i => !markedSpaces.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(-1, 9 - markedSpaces.Count());
        PlayableButton(range.ElementAt(index));
    }*/
}
