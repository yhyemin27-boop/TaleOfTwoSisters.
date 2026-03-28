using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    private Rigidbody rb; // Rigidbody 참조
    private CameraRot cameraRot; // 카메라 회전 참조

    public FixedJoystick joystick;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cameraRot = GetComponentInChildren<CameraRot>();
    }

    void FixedUpdate()
    {
        // 이동 처리
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        // 카메라의 방향 기준으로 이동 방향 계산
        Vector3 moveDirection = cameraRot.transform.forward * moveVertical + cameraRot.transform.right * moveHorizontal;

        // Rigidbody에 이동 힘 적용
        //rb.AddForce(moveDirection * speed * 0.5f);
        // Rigidbody에 이동 힘 적용  
        rb.velocity = (moveDirection * speed);
    }



    //red/ blue 유령에 부딫쳤을떄 실행 메소드
    private void OnTriggerEnter(Collider other)
    {

        GameObject gameDirectorObject = GameObject.Find("GameDirector");
        GameDirector gameDirector = gameDirectorObject.GetComponent<GameDirector>();

        if (gameDirector == null)
            return;
        if (other.gameObject.tag == "RedGhosts")
        {
            gameDirector.Die();
        }
        else if (other.gameObject.tag == "BlueGhost")
        { 
            gameDirector.BlueDam();
        }
    }
}
