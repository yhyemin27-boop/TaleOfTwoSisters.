using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public float HpRecovery = 0.2f; // 회복될 HP 게이지( 낮으면 바꿀려고 )
    private float lifetime = 15.0f; // 아이템의 생명주기 (15초 후 삭제)

    void Start()
    { 
        Destroy(gameObject, lifetime);//15초뒤 삭제
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject gameDirectorObject = GameObject.Find("GameDirector");
        if (other.gameObject.CompareTag("Player")) // 게임 오브젝트가 플레이어 태그
        {
            GameDirector gameDirector = gameDirectorObject.GetComponent<GameDirector>();
            if (gameDirector != null)
            {  //게임 디렉터 스크립트가 null이 아니고 아이템이 Player태그하고 부딮쳤을 떄 실행
               //부딫침 로그
               Debug.Log("Triggered by: " + other.gameObject.name);
                
                gameDirector.RecoverHp(HpRecovery); // HP 회복
                Destroy(gameObject);
            }
        }
    }
}