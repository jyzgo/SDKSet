using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MTUnity.Actions;

public class WinWindow : MonoBehaviour {

    public Text scoreText;
    public Text timeText;
    public Text moveText;

    public Text scoreTitle;
    public Text timeTitle;
    public Text moveTitle;
    public Text Congratulations;


    public void ShowScores()
    {
        int score=  LevelMgr.current._gameState.GetScore();
        int moves = LevelMgr.current._gameState.Moves;
        string time = LevelMgr.current._gameState.GetTime();

        var col = scoreText.color;
        col.a = 0f;
        scoreText.color = col;
        moveText.color = col;
        timeText.color = col;

        scoreTitle.color = col;
        timeTitle.color = col;
        moveTitle.color = col;
        Congratulations.color = col;

        scoreTitle.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));
        timeTitle.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));
        moveTitle.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));
        Congratulations.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));




        scoreText.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));
        moveText.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));
        timeText.gameObject.RunAction(new MTFontFadeTo(FADE_TIME, FADE_RATE));

        scoreText.text = score.ToString();
        moveText.text = moves.ToString();
        timeText.text = time;
        SoundManager.Current.PlayWinMusic();
    }

    const float FADE_TIME = 3f;
    const float FADE_RATE = 1f;
    public GameObject NewGameBtn;
    public GameObject ContinueBtn;

    public void SetChallangeWin(bool b)
    {
        ContinueBtn.SetActive(b);
        NewGameBtn.SetActive(!b);
    }

    public void Close()
    {
        if (ChallengeMgr.current.ChallengeActive)
        {
            ChallengeMgr.current.ShowChallenge();
        }
        gameObject.SetActive(false);
    }
}
