using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordMoveController : MonoBehaviour
{
  // tile 오브젝트들을 저장할 리스트
  private List<GameObject> tiles = new List<GameObject>();
  // 글자의 초기 위치를 저장할 리스트
  private List<Vector3> initialTilePositions = new List<Vector3>();
  // 정답 확인을 위한 리스트 생성.
  public List<GameObject> currentAnswers = new List<GameObject>();
  //정답 리스트.
  public List<string> correctAnswers = new List<string>();
  // 현재 타일 인덱스를 나타내는 변수, 처음에는 0으로 설정
  private int currentTileIndex = 0;
  public Text text;

  void Start(){
    correctAnswers =  new List<string> {"ㄱ","ㅣ","ㅁ","ㅅ","ㅏ","ㅇ","ㄱ","ㅠ","ㄴ"};
    // 태그가 "tile"인 모든 게임 오브젝트를 찾아 배열로 가져옴
    GameObject[] foundTiles = GameObject.FindGameObjectsWithTag("tile");
    tiles.AddRange(foundTiles); // tiles 리스트에 찾은 타일들을 추가
    tiles.Sort((a, b) => string.Compare(a.name, b.name));
    text.enabled = false; //시작시에 text 숨기기.
  }

  void Update()
  {
    if(Input.GetMouseButtonDown(0)){
      // 마우스 위치를 월드 좌표로 변환
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      // 마우스 위치 주변의 객체를 감지
      Collider2D collider = Physics2D.OverlapCircle(mousePosition, 0.5f);
      // tiles 리스트가 비어 있지 않으면
      if(tiles.Count >0 && collider.CompareTag("word")){
        //감지된 객체 현재 위치 저장.
        initialTilePositions.Add(collider.transform.position);
        
        // 감지된 객체의 위치를 현재 타일의 위치로 이동
        collider.transform.position = tiles[currentTileIndex].transform.position;
        Debug.Log(initialTilePositions[0]+" | "+tiles[currentTileIndex].transform.position);
        // currentTileIndex를 증가시켜서 다음 타일로 이동, 리스트 끝에서 다시 처음으로 돌아옴
        currentTileIndex = (currentTileIndex+1) % tiles.Count;
        //이동한 객체 이름 정답 리스트에 저장.
        currentAnswers.Add(collider.gameObject);
      }
      if(collider.gameObject.name == "submit_Button"){
        Debug.Log("submint clicked");
        CheckAnswer();
      }
    }
  }

  public void CheckAnswer(){
    bool isCorrect = false;
    if(currentAnswers.Count == correctAnswers.Count){
      for (int i = 0; i<correctAnswers.Count; i++){
        if(currentAnswers[i].name == correctAnswers[i]){
          Debug.Log(currentAnswers[i] +" "+ correctAnswers[i]);
          isCorrect = true;
        }else{
          isCorrect = false;
          break;
        }
      }
    }
    if(isCorrect){
      Debug.Log("정답");
      SceneManager.LoadScene("FinishScene");
    }else{
      Debug.Log("틀림");
      text.enabled = true;
      Invoke("ResetTiles", 1f);
    }

  }

  private void ResetTiles(){
    for(int i = 0; i< currentAnswers.Count; i++){
      Debug.Log(currentAnswers[i].transform.position +"  "+ currentAnswers[i].name);
      currentAnswers[i].transform.position = initialTilePositions[i];
    }
    currentAnswers.Clear();
    initialTilePositions.Clear();
    currentTileIndex = 0;
    text.enabled = false;
  }

}
