using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class GameManager : MonoBehaviour
{
    public needle_short hourNeedle;
    public needle_long minuteNeedle;
    public UnityEngine.UI.Text resultText; // UI 텍스트
    public UnityEngine.UI.Text hint;// UI 텍스트
    private bool resultChecked = false;
    private bool gameStarted = false;

    void Update()
    {
        if ((hourNeedle.isRunning || minuteNeedle.isRunning) && !gameStarted)
        {
            gameStarted = true;
        }

        if (gameStarted && !hourNeedle.isRunning && !minuteNeedle.isRunning && !resultChecked)
        {
            CheckIfWin();
        }
    }

    void CheckIfWin()
    {
        if (hourNeedle.inTargetHourRange && minuteNeedle.inTargetMinuteRange)
        {
            resultText.text = "축하합니다! 쉬는 시간이에요!";
            Debug.Log("축하합니다! 목표 시간에 멈췄습니다.");

            // 씬 이름에 따라 다음 씬으로 이동
            if (SceneManager.GetActiveScene().name == "ClockGame")
            {
                Invoke("LoadNextGameScene", 2f); // ClockGameScene2로 전환
            }
            else if (SceneManager.GetActiveScene().name == "ClockGame2")
            {
                Invoke("LoadHintScene", 2f); // ClockHintScene으로 전환
            }
        }
        else
        {
            resultText.text = "아쉽습니다. 다시 도전해볼까요?";
            Debug.Log("아쉽습니다. 목표 시간에 멈추지 못했습니다.");
            Invoke("ResetGame", 2f); // 2초 후 게임 초기화
        }

        resultChecked = true;
    }

    void LoadNextGameScene()
    {
        SceneManager.LoadScene("ClockGame2");
    }

    void LoadHintScene()
    {
        SceneManager.LoadScene("ClockHintScene"); 
    }

    void ResetGame()
    {
        hourNeedle.ResetNeedle();
        minuteNeedle.ResetNeedle();
        resultChecked = false;
        gameStarted = false;
        resultText.text = "화면을 눌러 시계를 멈추세요!"; // 초기 메시지 숨김
        
        // 씬 이름에 따라 다른 힌트
        if (SceneManager.GetActiveScene().name == "ClockGame")
        {
            hint.text = "hint .oO(쉬는 시간은 1시 15분부터 1시 30분까지야)";
        }
        else if (SceneManager.GetActiveScene().name == "ClockGame2")
        {
            hint.text = "hint .oO(다음 쉬는 시간은 2시 45분부터 3시 00분까지야)";
        }
    }
}
