using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDirect : MonoBehaviour
{
    public GameObject GameOverText; // Game Over 텍스트 오브젝트
    public Text timeText;
    public Text BestTime;

    float surviveTime;
    float bestTime;

    void Start()
    {
        surviveTime = 0;
        GameOverText.SetActive(false);
        // 최고 기록 초기화
        bestTime = PlayerPrefs.GetFloat("BestTime", 0);

        if (BestTime != null)
        {
            BestTime.text = "Best Time: " + (int)bestTime;
        }

    }

    void Update()
    { 
        //https://blog.naver.com/freshofftheboat/222839381511
        //
        if (!GameOverText.activeSelf) // GameOverText가 비활성화되어 있을 때만 시간 증가
        {
            //닷지게임
            surviveTime += Time.deltaTime;
            if (timeText != null)
            {
                timeText.text = "Time: " + (int)surviveTime;
            }

            if (surviveTime > bestTime)
            {//닷지게임
                bestTime = surviveTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }
        }

        if (BestTime != null)
        {
            BestTime.text = "Best Time: " + (int)bestTime;
        }
    }
}
