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

    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.hpGauge = GameObject.Find("hpGauge");
    }



    void Update()
    {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1"); 

        if (this.time <= clear ) {
            SceneManager.LoadScene("FHintScene");
            MainGameDirector.test_paper_clear = true;
        }
    }

    

    public void isGameOver() {
        if( this.hpGauge.GetComponent<Image>().fillAmount < 0.1f){
            SceneManager.LoadScene("DieScene");
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
}
