using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    public static Objective instance;

    public float m_timeToComplete;

    public CanvasGroup canvasGroup;

    public GameObject endScreen;
    public GameObject winScreen;

    public TextMeshProUGUI gameTimer, finalscore;


    private void Start()
    {
        instance = this;
        m_timeToComplete = 80f;
    }
    private void Update()
    {
        m_timeToComplete -= Time.deltaTime;

        gameTimer.text = m_timeToComplete.ToString();

        if (m_timeToComplete <= 0) //if lose
        {
            GameManager.instance.gameOver = true;
            EndScreen();
        }

        if (KillCount.instance.killCount >= KillCount.instance.maxKillCount) //if objective completed
        {
            GameManager.instance.gameOver = true;
            WinScreen();
        }
    }
    public void EndScreen()
    {
        endScreen.SetActive(true);

        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, Time.fixedDeltaTime);

        finalscore.text = "Score : " + "\n" + KillCount.instance.killCount + " / " + KillCount.instance.maxKillCount;
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);

        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 255, Time.fixedDeltaTime);

        Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.fixedDeltaTime * 10); //slowdown time to lerp in endscreen.
    }
}
