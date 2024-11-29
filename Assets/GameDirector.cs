using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    GameObject timerText;
    float time = 30.0f;
    float clear = 0.0f;

    GameObject hpGauge;

    public bool isGameRunning = false;

    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.hpGauge = GameObject.Find("hpGauge");
        ShowStartMessage();
    }



    void Update()
    {
        if (!isGameRunning)
        {
            // Space 키로 게임 시작
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGameRunning = true;
                HideStartMessage();
            }
            return;
        }

        this.time -= Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1"); 

        if (this.time <= clear ) {
            SceneManager.LoadScene("FHintScene");
            MainGameDirector.test_paper_clear = true;
        }
    }

    

    public void isGameOver() {
        if( this.hpGauge.GetComponent<Image>().fillAmount < 0.1f){
            SceneManager.LoadScene("FGameScene");
        }
    }

    public void DecreaseHp() {
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.2f;
    }

    public void IncreaseHp() {
        if (this.hpGauge.GetComponent<Image>().fillAmount < 1.0f ){
            this.hpGauge.GetComponent<Image>().fillAmount += 0.2f;
        }
    }

    void ShowStartMessage()
    {
        GameObject startMessage = GameObject.Find("StartMessage");
        if (startMessage != null)
        {
            startMessage.GetComponent<Text>().text = "Press SPACE to Start!";
        }
    }

    
    void HideStartMessage()
    {
        GameObject startMessage = GameObject.Find("StartMessage");
        if (startMessage != null)
        {
            startMessage.GetComponent<Text>().text = "";
        }
    }
}
