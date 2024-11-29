using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//강의실 화면에서 클릭시 각 게임 가져오기.
public class MainGameDirector : MonoBehaviour
{
    public GameObject clock;
    public GameObject pencil;
    public GameObject test_paper;
    public GameObject door;
    
    public GameObject Square;
    public static bool isFirstRun = true; //처음 실행 여부 확인.
    private float SquareDisplayTime = 0f; //처음 설명 박스 표시되는 시간.
    public UnityEngine.UI.Text text;
    private float messageDisplayTime = 0f; // 메시지가 표시되는 시간
    private bool isMessageVisible = false; // 메시지가 현재 표시 중인지 여부

    //clear 확인 변수.
    public static bool clock_clear = false;
    public static bool pencil_clear = false;
    public static bool test_paper_clear = false;

    void Start(){
      if(isFirstRun){
        Square.SetActive(true);
        SquareDisplayTime = Time.time + 5f; //현재 시간 + 5초 후 Square 숨김.
        isFirstRun = false;
      }
    }

    void Update()
    {
        // 시간이 지났고 Square가 현재 표시 중이라면 숨김
        if (!isFirstRun && Time.time > SquareDisplayTime)
        {
            Square.SetActive(false);
        }
        // 메시지가 표시 중이고 시간이 지났는지 확인
        if (isMessageVisible && Time.time >= messageDisplayTime)
        {
            text.gameObject.SetActive(false); // 메시지 숨김
            isMessageVisible = false;
        }


        //미니게임 찾아서 불러오기.
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
          // 마우스 클릭 위치를 word 좌표로 저장.
          Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

          //Collider2D.OverlapPoint()로 오브젝트가 클릭된 범위 내에 있는지 확인.
          //Clock_clear : 시간맞추기 게임 clear 여부.
          if(!clock_clear && clock.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            SceneManager.LoadScene("ClockGame");
          }
          //pencil_clear : 같은그림맞추기 게임 clear 여부.
          else if(!pencil_clear && pencil.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            SceneManager.LoadScene("ItemGameScene");
          }
          //test_paper_clear : F피하기 게임 clear여부.
          else if(!test_paper_clear && test_paper.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            SceneManager.LoadScene("GameExplain");
          //게임 오브젝트 클릭했으나 이미 클리어한 경우
          }else if (clock.GetComponent<Collider2D>().OverlapPoint(clickPosition) || 
                 pencil.GetComponent<Collider2D>().OverlapPoint(clickPosition) || 
                 test_paper.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
              ShowMessage("이미 클리어한 게임이야 다른 게임을 찾아봐!");
          }


          //문 클릭 시 퀴즈맞추기 최종 게임.
          if(door.GetComponent<Collider2D>().OverlapPoint(clickPosition)){
            if(clock_clear && pencil_clear && test_paper_clear){//3개 모두 클리어 시
              SceneManager.LoadScene("LastGameScene"); 
            }else{ //하나라도 클리어 못했을 경우.
              ShowMessage("아직 종강할 때가 아니야~! 숨겨진 게임을 더 찾아봐!");
            }
          }
        }
    }

    void ShowMessage(string message){
      text.text = message;
      text.gameObject.SetActive(true);
      isMessageVisible = true; 
      messageDisplayTime = Time.time + 3f; // 현재 시간 + 3초 후에 메시지 숨김
    }
}
