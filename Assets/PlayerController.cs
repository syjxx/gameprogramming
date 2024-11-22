using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveSpeed = 10f; // 캐릭터 이동 속도
    Rigidbody2D rigid2D;
    Vector2 moveInput;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
    }

    void Update()
    {
        // 키보드 입력 받기
        moveInput.x = Input.GetAxisRaw("Horizontal"); // 왼쪽, 오른쪽 화살표 키
        moveInput.y = Input.GetAxisRaw("Vertical");   // 위, 아래 화살표 키
        moveInput = moveInput.normalized; // 대각선 이동 시 속도 균일화

        if ( moveInput.x == 1) {
            transform.localScale = new Vector3( 0.6f,0.3f,1);
        } else if ( moveInput.x == -1){
            transform.localScale = new Vector3(-0.6f,0.3f,1);
        }
        
        
    }
 
    void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            rigid2D.velocity = moveInput * moveSpeed;
        }
        else
        {
            rigid2D.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other){

        GameObject director = GameObject.Find("GameDirector");
        

        if (other.gameObject.name == "redFPrefab(Clone)" || other.gameObject.name == "greenFPrefab(Clone)"){
            director.GetComponent<GameDirector>().DecreaseHp();
            Destroy(other.gameObject);
            director.GetComponent<GameDirector>().isGameOver();

        } else if ( other.gameObject.name == "heartPrefab(Clone)"){

            director.GetComponent<GameDirector>().IncreaseHp();
            Destroy(other.gameObject);
        }
    }

}