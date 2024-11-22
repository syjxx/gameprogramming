using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemClearDirector : MonoBehaviour
{
    void Start(){
      MainGameDirector.pencil_clear = true;
      Invoke("ChangeScene", 2f); //5초 뒤 씬 전환.
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
