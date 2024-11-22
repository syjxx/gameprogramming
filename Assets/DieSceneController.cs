using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void REStartButton()
    {
        SceneManager.LoadScene("FGameScene"); // 전환하고자 하는 화면인 B의 이름을 ""에 넣어준다.
    }
}
