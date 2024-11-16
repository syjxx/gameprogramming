using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needle_long : MonoBehaviour
{
    public bool isRunning = false;
    public bool inTargetMinuteRange = false;
    private float rotSpeed = -10f; // 기본 회전 속도
    private float targetMinuteStartAngle = 360f - 90f; // 15분 시작 각도
    private float targetMinuteEndAngle = 360f - 180f;  // 30분 끝 각도
    private Quaternion initialRotation; // 초기 회전 저장
    private bool resetComplete = true;  // 초기화 완료 여부 플래그

    void Start()
    {
        initialRotation = transform.rotation; // 초기 회전 저장

        // 씬 이름에 따라 속도와 목표 범위 설정
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame")
        {
            rotSpeed = -5f; // 씬1: 느린 속도
            targetMinuteStartAngle = 360f - 90f; // 15분 시작
            targetMinuteEndAngle = 360f - 180f; // 30분 끝
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2")
        {
            rotSpeed = -10f; // 씬2: 빠른 속도
            targetMinuteStartAngle = 0f; // 45분 시작
            targetMinuteEndAngle = 90f;   // 정각
        }
    }

    void Update()
    {
        if (!resetComplete) return; // 초기화가 완료되지 않았다면 입력 처리 안 함

        if (Input.GetMouseButtonDown(0))
        {
            if (!isRunning)
            {
                isRunning = true;
                Debug.Log("분 바늘이 움직이기 시작했습니다.");
            }
            else
            {
                isRunning = false;
                CheckMinuteTargetRange();
            }
        }

        if (isRunning)
        {
            transform.Rotate(0, 0, rotSpeed); // 바늘 회전
        }
    }

    void CheckMinuteTargetRange()
    {
        float currentAngle = transform.eulerAngles.z;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame" && currentAngle <= targetMinuteStartAngle && currentAngle >= targetMinuteEndAngle)
        {
            inTargetMinuteRange = true;
            Debug.Log("분 바늘이 목표 범위에 있습니다.");
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2" && currentAngle >= targetMinuteStartAngle && currentAngle <= targetMinuteEndAngle)
        {
            inTargetMinuteRange = true;
            Debug.Log("분 바늘이 목표 범위에 있습니다.");
        }
        else
        {
            inTargetMinuteRange = false;
            Debug.Log("분 바늘이 목표 범위에 있지 않습니다.");
            Debug.Log($"긴 바늘 현재 각도: {currentAngle}, 목표 각도 범위: {targetMinuteStartAngle} ~ {targetMinuteEndAngle}");
        }
    }

    public void ResetNeedle()
    {
        transform.rotation = initialRotation; // 초기 위치로 회전
        isRunning = false;
        inTargetMinuteRange = false;

        // 씬 이름에 따라 초기화 시 속도 설정
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame")
        {
            rotSpeed = -5f; // 씬1: 느린 속도
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2")
        {
            rotSpeed = -10f; // 씬2: 빠른 속도
        }

        StartCoroutine(EnableInputAfterReset());
    }

    private IEnumerator EnableInputAfterReset()
    {
        resetComplete = false; // 입력 비활성화
        yield return null; // 한 프레임 대기
        resetComplete = true; // 입력 활성화
    }

    public float GetRotSpeed()
    {
        return rotSpeed;
    }
}
