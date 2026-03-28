using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //https://arainablog.tistory.com/110
    //https://blog.naver.com/padamu1/222615809459
    public GameObject ItemPrefabs; // 생성할 프리팹
    public Vector3 minRange = new Vector3(-44, 1, -16); // 생성 범위 최소좌표값
    public Vector3 maxRange = new Vector3(12, 1, 48);   // 생성 범위 최대좌표값
    private float delta = 0f; // 생성 간격을 계산하는 변수
    private float span = 2.0f; // 생성 간격 (초 단위)
    void Update()
    {
        delta += Time.deltaTime;
        // 설정된 시간 간격(span)마다 아이템 생성
        if (delta > span)
        {
            delta = 0f;
            // 랜덤한 위치 계산
            float x = Random.Range(minRange.x, maxRange.x);
            float y = Random.Range(minRange.y, maxRange.y); //없으면 랜덤한 y에서 생성
            float z = Random.Range(minRange.z, maxRange.z);
            Vector3 spawnPosition = new Vector3(x, y, z);
            // 아이템 생성
            Instantiate(ItemPrefabs, spawnPosition , Quaternion.identity); // 기본 회전값 0 
            // span 점진적으로 증가
            if (span > 1)
            {
                if (span < 5)//5초 이상으로는 안가게
                {
                    span *= 1.002f;
                }
            }
        }
    }
}
