using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    private void OnMouseDown(){
      //게임 실행에 필요한 요소 초기화.
      MainGameDirector.isFirstRun = true;
      MainGameDirector.clock_clear = false;
      MainGameDirector.test_paper_clear = false;
      MainGameDirector.pencil_clear = false;
      
      //씬 로드.
      SceneManager.LoadScene("MainScene");
      
    }
}
