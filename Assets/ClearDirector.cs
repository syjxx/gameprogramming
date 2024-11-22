using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDirector : MonoBehaviour
{
    void Start(){
      Invoke("ChangeScene", 2f); //5초 뒤 씬 전환.
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
