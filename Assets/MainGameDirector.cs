using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//강의실 화면에서 클릭시 각 게임 가져오기.
public class MainGameDirector : MonoBehaviour
{
    GameObject clock;
    GameObject pencil;
    GameObject test_paper;
    GameObject door;

    //clear 확인 변수.
    public static bool clock_clear = false;
    public static bool pencil_clear = false;
    public static bool test_paper_clear = false;

    void Start(){
      //미니 게임을 실행하기 위한 오브젝트 불러오기.
      this.clock = GameObject.Find("clock_main");
      this.pencil = GameObject.Find("pencil");
      this.test_paper = GameObject.Find("test_paper");
      this.door = GameObject.Find("door");
    }

    void Update()
    {
        //미니게임 찾아서 불러오기.
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
          // 마우스 클릭 위치를 word 좌표로 저장.
          Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

          //Collider2D.OverlapPoint()로 오브젝트가 클릭된 범위 내에 있는지 확인.
          if(!clock_clear && clock.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            //해당 Scene에서 clear시 GameDirector.clock_clear = true로 변환해주기.
            Debug.Log("clock clicked!");
            // SceneManager.LoadScene("");
          }
          //pencil_clear == 같은그림맞추기 게임 clear 여부.
          if(!pencil_clear && pencil.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            Debug.Log("Pencil clicked!");
            SceneManager.LoadScene("ItemGameScene");
          }
          //test_paper_clear : F피하기 게임 clear여부.
          if(!test_paper_clear && test_paper.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            Debug.Log("test_paper clicked!");
          }
          
          //퀴즈맞추기 최종 게임.
          // if(door.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
          //   if(clock_clear && pencil_clear && test_paper_clear){
          //     SceneManager.LoadScene("lastGame"); //앞 3게임이 모두 클리어할 경우만 scene전환.
          //   }else{ //하나라도 아니면 못넘어감.
          //     Debug.Log("아직 종강할 때가 아니야!~");
          //   }
          // }
        }

        // 모든 게임 클리어시 마지막 게임으로 이동.
        // if(clock_clear && pencil_clear && test_paper_clear){
        //   SceneManager.LoadScene("LastGame");
        // }
    }
}
