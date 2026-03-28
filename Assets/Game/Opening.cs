using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public GameObject GameSTART; // 오타 수정
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        if (GameSTART != null)
        {
            GameSTART.SetActive(false); // 시작 시 비활성화
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 처리
        if (Input.GetMouseButtonDown(0))
        {
            GameSTART.SetActive(true); // 활성화
        }
    }

    public void START()
    {
        SceneManager.LoadScene(1); // 1번 씬 로드
    }
}
