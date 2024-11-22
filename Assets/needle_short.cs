using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needle_short : MonoBehaviour
{
    public bool isRunning = false;
    public bool inTargetHourRange = false;
    private float rotSpeed = 0;
    private float targetHourStartAngle = 360f - 30f; // 1시 시작 각도
    private float targetHourEndAngle = 360f - 60f;   // 1시와 2시 사이의 각도
    private Quaternion initialRotation; // 초기 회전 저장
    private bool resetComplete = true;  // 초기화 완료 여부 플래그

    public needle_long minuteNeedle; // 긴 바늘의 회전 속도를 참조

    void Start()
    {
        initialRotation = transform.rotation; // 초기 회전 저장

        // 씬 이름에 따라 목표 범위 설정
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame")
        {
            targetHourStartAngle = 360f - 30f; // 1시 시작
            targetHourEndAngle = 360f - 60f;  // 2시 끝
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2")
        {
            targetHourStartAngle = 360f - 60f; // 2시 시작
            targetHourEndAngle = 360f - 90f;  // 3시 끝
        }
    }

    void Update()
    {
        if (!resetComplete) return; // 초기화가 완료되지 않았다면 입력 처리 안 함

        if (minuteNeedle.isRunning)
        {
            if (rotSpeed != minuteNeedle.GetRotSpeed() / 12f) // 불필요한 연산 최소화
            {
                rotSpeed = minuteNeedle.GetRotSpeed() / 12f;
            }
            transform.Rotate(0, 0, rotSpeed); // 긴 바늘의 회전 속도를 기반으로 회전
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isRunning)
            {
                isRunning = true;
                Debug.Log("시간 바늘이 움직이기 시작했습니다.");
            }
            else
            {
                isRunning = false;
                CheckHourTargetRange();
            }
        }
    }

    void CheckHourTargetRange()
    {
        float currentAngle = transform.eulerAngles.z;
        if (currentAngle <= targetHourStartAngle && currentAngle >= targetHourEndAngle)
        {
            inTargetHourRange = true;
            Debug.Log("시간 바늘이 목표 범위에 있습니다.");
        }
        else
        {
            inTargetHourRange = false;
            Debug.Log("시간 바늘이 목표 범위에 있지 않습니다.");
            Debug.Log($"짧은 바늘 현재 각도: {currentAngle}, 목표 각도 범위: {targetHourStartAngle} ~ {targetHourEndAngle}");

        }
    }

    public void ResetNeedle()
    {
        transform.rotation = initialRotation; // 초기 위치로 회전
        isRunning = false;
        inTargetHourRange = false;
        rotSpeed = 0; // 회전 속도 초기화

        // 초기화 후 한 프레임 동안 클릭 입력을 무시
        StartCoroutine(EnableInputAfterReset());
    }

    private IEnumerator EnableInputAfterReset()
    {
        resetComplete = false; // 입력 비활성화
        yield return null; // 한 프레임 대기
        resetComplete = true; // 입력 활성화
    }
}