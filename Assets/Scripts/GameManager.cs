using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region SingleTon
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    #endregion


    [Header("현재스코어 텍스트")] [SerializeField] private Text scoreText;
    [Header("최대스코어 텍스트")] [SerializeField] private Text bestScore;

    [Header("똥 오브젝트")] [SerializeField] private GameObject poop = null;
    [Header("시작패널 오브젝트")] [SerializeField] private GameObject panel = null;

    [Header("똥 생성 위치")] [SerializeField] private Transform poopTrn = null;

    [HideInInspector] public bool isStopTrigger = true;
    private int score;

    public void GameOver()
    {
        isStopTrigger = false;
        StopCoroutine(CreatePoopRoutine());

        if (score >= PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);
    }

    public void GameStart()
    {
        score = 0;
        scoreText.text = $"SCORE : {score}";
        isStopTrigger = true;
        StartCoroutine(CreatePoopRoutine());
        panel.SetActive(false);
    }

    public void Score()
    {
        if (isStopTrigger)
        {
            score++;
        }
        scoreText.text = $"SCORE : {score}";
    }

    private IEnumerator CreatePoopRoutine()
    {
        while (isStopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void CreatePoop()
    {
        Vector3 _pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, 5.0f));
        GameObject obj = Instantiate(poop, _pos, Quaternion.identity);
        obj.transform.parent = poopTrn.transform;
    }
}
