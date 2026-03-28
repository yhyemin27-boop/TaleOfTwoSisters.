using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    //https://zheldajdajd.tistory.com/14
    //https://parksh3641.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-C-%EB%AA%A8%EB%B0%94%EC%9D%BC-%EC%8A%A4%EC%99%80%EC%9D%B4%ED%94%84-3D-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%9A%8C%EC%A0%84%EC%8B%9C%ED%82%A4%EA%B8%B0-%EA%B0%84%EB%8B%A8-%EA%B5%AC%ED%98%84
    //https://github.com/EmmaPrats/Camera-Rotation-Tutorial
    private float mouseSpeed = 0.1f; // 회전 속도
    private float rotationY = 0f; // 좌우 회전 값
    private Vector3 lastMousePosition; // 이전 마우스 위치
    private bool isDragging = false; // 드래그 상태 확인

    private Transform playerTransform; // 플레이어 오브젝트의 Transform을 가져오기 위한 변수

    void Start()
    {
        playerTransform = transform.parent;
    }

    void Update()
    {
        // 마우스 버튼이 눌렸을 때
        if (Input.GetMouseButtonDown(0)) // 0: 왼쪽 버튼
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }
        // 마우스 버튼을 떼었을 때
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때 회전 처리
        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; // 마우스 이동량 계산
            rotationY +=delta.x * mouseSpeed;

            // 카메라 회전 적용
            this.transform.localEulerAngles = new Vector3(0,rotationY, 0);

            // 플레이어 오브젝트도 동일한 회전값 적용
            playerTransform.localEulerAngles = new Vector3(0,rotationY, 0);
            //localEulerAngles == 로컬 좌표계에서의 회전(오일러 각도)
            lastMousePosition = Input.mousePosition; // 현재 마우스 위치 업데이트
        }
    }
}