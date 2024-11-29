using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordPuzzleDirector : MonoBehaviour
{
  // tile 오브젝트들을 저장할 배열
  private GameObject[] tiles;
  // "word" 오브젝트와 초기 위치를 저장할 딕셔너리
  private Dictionary<GameObject, Vector3> wordInitialPositions = new Dictionary<GameObject, Vector3>();
  //현재 정답 배열에 맞춰 선택된 단어들을 저장할 배열
  private GameObject[] currentAnswers;
  //정답 저장 배열
  public string[] correctAnswers;
  public GameObject hint; //hint 화면
  public Text hintText;//정답이 틀릴 경우 출력할 텍스트 UI
  

  void Start(){
    //정답 리스트 초기화
    correctAnswers = new string[]{"ㄱ","ㅣ","ㅁ","ㅅ","ㅏ","ㅇ","ㄱ","ㅠ","ㄴ"};
    // tile 오브젝트들을 "tile" 태그로 찾아 배열에 저장
    tiles = GameObject.FindGameObjectsWithTag("tile");
    //tiles 배열을 이름 순으로 정렬.
    System.Array.Sort(tiles, (a, b) => string.Compare(a.name, b.name));

    // 태그가 "word"인 모든 오브젝트를 찾아 오브젝트와 초기 위치를 저장
    GameObject[] foundWords = GameObject.FindGameObjectsWithTag("word");
    foreach (GameObject word in foundWords)
    {
        wordInitialPositions[word] = word.transform.position;
    }

    // currentAnswers 배열 초기화 (정답 배열의 크기에 맞춰)
    currentAnswers = new GameObject[correctAnswers.Length];

    //텍스트 ui를 비활성화(처음에는 보이지 않도록 설정).
    hintText.enabled = false;

    //hint 숨김
    hint.SetActive(false);
}

  void Update()
  {
    //마우스 왼쪽 버튼 클릭시
    if(Input.GetMouseButtonDown(0)){
      // 마우스 클릭 위치를 월드 좌표로 변환
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      // 마우스 클릭 위치 주변의 객체를 감지
      Collider2D collider = Physics2D.OverlapCircle(mousePosition, 0.5f);
      if(collider != null){
        // tiles 리스트가 비어 있지 않고 'word' 태그를 가졌는지 확인.
        if(tiles.Length >0 && collider.CompareTag("word")){
          HandleWordSelection(collider.gameObject); //word 선택 처리
        }
        //클릭된 객체가 제출 버튼인지 확인
        if(collider.gameObject.name == "submit_Button"){
          CheckAnswer(); //정답 확인
        }
      }
    }

    if(Input.GetKey(KeyCode.Space)){
      hint.SetActive(true);
    }else{
      hint.SetActive(false);
    }
  }

  //word 선택을 처리하는 함수.
  private void HandleWordSelection(GameObject word){
    // currentAnswers 배열에 이미 있는 객체인지 확인
    int index = System.Array.IndexOf(currentAnswers, word);

    // 이미 배열에 존재하는 경우
    if (index != -1)
    {
        // 배열에 있는 단어를 초기 위치로 되돌림
        word.transform.position = wordInitialPositions[word];
        currentAnswers[index] = null; //배열에서 해당 단어를 제거
    }
    else
    {
      // currentAnswers에 없으면 빈 자리를 찾아 단어를 추가
      for (int i = 0; i < currentAnswers.Length; i++)
      {
          if (currentAnswers[i] == null)
          {
              currentAnswers[i] = word; //배열에 단어 추가
              // tiles 배열의 해당 인덱스 위치로 객체 이동
              word.transform.position = tiles[i].transform.position;
              break;
          }
      }
    }
  }

  //정답을 확인하는 메서드
  public void CheckAnswer(){
    bool isCorrect = false;

    for (int i = 0; i<correctAnswers.Length; i++){
      //currentAnswer와 correctAnswers의 순서와 값이 동일한지 비교.
      if(currentAnswers[i].name == correctAnswers[i] && currentAnswers != null){
        isCorrect = true; //정답
      }else{
        isCorrect = false; //오답 발견되면 루프 종료
        break;
    }
    }

    //정답인 경우
    if(isCorrect){
      //정답시 'FinishScene'으로 씬 전환
      SceneManager.LoadScene("FinishScene"); 
    }else{
      //오답인 경우 텍스트 표시 및 타일 초기화
      hintText.enabled = true;
      Invoke("ResetWords", 2f); // 2초 후에 타일 재설정 함수 호출
    }
  }

  //글자를 초기 위치로 재설정하는 함수
  private void ResetWords(){
    // currentAnswers 배열에 있는 단어들을 초기 위치로 되돌림
    for(int i = 0; i< currentAnswers.Length; i++){
      if (currentAnswers[i] != null){
        currentAnswers[i].transform.position = wordInitialPositions[currentAnswers[i]];
      }
    }
    //currentAnswers 배열 초기화
    currentAnswers = new GameObject[correctAnswers.Length];
    hintText.enabled = false;
  }

}
