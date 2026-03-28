using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BuleController : MonoBehaviour
{

    public Transform target; // 파랑색 유령의 목표
    public float moveSpeed = 4.5f; // 파랑색 유령의 이동 속도\
    LayerMask playerLayer; // 다른 오브젝트 충돌 방지

    public float detectionRadius = 13f; // 탐지 범위 설정
    private float lifetime = 7.0f; // 유령의 생명주기 

      // 오디오 관련 소스
    public AudioSource monsterAudio;

    private void Start()
    {
        monsterAudio = GetComponent<AudioSource>();

        playerLayer = 1 << 8; // 플레이어 레이어 지정
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // 타겟 갱신 함수 호출
        Destroy(gameObject, lifetime);//15초뒤 삭제
    }

    private void UpdateTarget()
    {
        // 탐지 범위 내의 플레이어 찾기
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        //https://bloodstrawberry.tistory.com/877
        //https://todah.tistory.com/40
        //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.RotateTowards.html
        if (detectedColliders.Length > 0)
        {
            foreach (Collider col in detectedColliders)
            {
                if (col.CompareTag("Player"))  // Player 태그를 가진 오브젝트인지 확인
                {
                    if (!monsterAudio.isPlaying) // 유령 소리가 재생 중이 아닐 때만 재생
                    {
                        monsterAudio.Play(); // 유령 소리 재생
                    }
                    target = col.transform;
                    return; // 첫 번째 Player 발견 시 나머지 탐색 종료
                }
            }
        }
        else
        {

            target = null; // 타겟이 없을 때 초기화
            if (monsterAudio.isPlaying)
            {
                monsterAudio.Stop(); // 유령 소리 멈춤
            }
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // 타겟을 향한 방향 계산
            Vector3 lookPlayer = target.position - transform.position;

            // 즉각적으로 타겟을 바라보도록 회전
            if (lookPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookPlayer); // 즉각적인 회전
            
            }

            // 타겟을 향해 이동
            Vector3 dir = lookPlayer.normalized;

            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
            //                                                              월드 좌표계로
        }
    }
    private void OnDrawGizmosSelected()
    {
        // 시각적으로 빨간색 유령의 탐지 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 탐지 범위 표시
    }
}
