using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearDirector : MonoBehaviour
{
    public UnityEngine.UI.Text text; //UI 텍스트를 드래그 앤 드롭
    float countdownTime = 2f;//2초 카운트다운.
    float currentTime; //남은 시간.

    void Start(){
      text = GameObject.Find("text").GetComponent<Text>();
      currentTime = countdownTime; //시작시간 설정.
    }
    void Update()
    {
        if(currentTime >0){
           // 델타타임을 사용하여 카운트다운 시간 업데이트
            currentTime -= Time.deltaTime;

            // 카운트다운 텍스트 갱신
            text.text = Mathf.Ceil(currentTime) +"초 후 강의실로 이동합니다. "; // Ceil을 사용하여 정수로 표시
        }else{
          SceneManager.LoadScene("MainScene");
        }
    }
}
