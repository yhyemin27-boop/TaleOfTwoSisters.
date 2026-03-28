using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public Image myImage; // UI Image 컴포넌트
    private float fillAmount = 1.0f; // 현재 게이지 상태 (초기값 1로 설정)
    public GameObject GameOverText; // Game Over 텍스트 오브젝트
    public float totalTime = 60.0f; // 총 시간 설정 (1 이상)

    // 먹는 사운드
    private AudioSource audioSource;
    //엔딩 사운드
    public AudioClip ENDClip;

    // 플레이어의 AudioSource를 참조
    private AudioSource playerAudioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 플레이어의 AudioSource 찾기 (플레이어 태그 기반으로 찾음)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAudioSource = player.GetComponent<AudioSource>();
        }

        GameOverText.SetActive(false);
    }

    void Update()
    {
        // 피 관련 업데이트
        if (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime / totalTime; // 매 초마다 줄어들기
            fillAmount = Mathf.Max(fillAmount, 0); // 최소치를 0으로 지정 (0 이하로 안 깎이게)

            if (myImage != null)
                myImage.fillAmount = fillAmount;
        }

        // HP가 0일 때 처리
        if (fillAmount <= 0 && GameOverText != null)
        {
            GameOverText.SetActive(true);

            // 플레이어 AudioSource 끄기
            if (playerAudioSource != null)
            {
                playerAudioSource.Stop();
            }

            // ENDClip 반복 재생
            if (audioSource != null && ENDClip != null && !audioSource.isPlaying)
            {
                audioSource.clip = ENDClip;
                audioSource.Play();
            }
        }
    }

    // fillAmount를 회복하는 메서드
    public void RecoverHp(float recoveryAmount)
    {
        fillAmount = Mathf.Min(fillAmount + recoveryAmount, 1.0f); // 최대값 1로 제한

        if (myImage != null)
        {
            audioSource.Play();
            myImage.fillAmount = fillAmount; // HP 업데이트
        }
    }

    // 플레이어 사망 처리
    public void Die()
    {
        fillAmount = 0; // HP를 0으로 설정
        if (myImage != null)
            myImage.fillAmount = fillAmount; // HP 업데이트
    }

    // 블루 유령 데미지 처리
    public void BlueDam()
    {
        fillAmount = Mathf.Max(fillAmount / 2, 0); // 현재 피의 절반으로 깎기
        if (myImage != null)
            myImage.fillAmount = fillAmount; // HP 업데이트
    }

    // 게임 재시작
    public void ReStart()
    {
        SceneManager.LoadScene("Game");
    }
}
