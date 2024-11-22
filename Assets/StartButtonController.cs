using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    private void OnMouseDown(){
      SceneManager.LoadScene("MainScene");
    }
}
