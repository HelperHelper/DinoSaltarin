using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; protected set; }
    [Header("Propiedades publicas del GameManager")]
    public float score;
    public TextMeshProUGUI scoreText;
    public Transform startingPoint;
    public float lerpSpeed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        score = 0;
        PlayerController.Instance.gameOver = true;
        StartCoroutine(PlayIntro());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.Instance.gameOver)
        { 
            score++;
            scoreText.text = score.ToString();
        }
    }

    public IEnumerator PlayIntro()
    {
        Vector3 startPos = PlayerController.Instance.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        PlayerController.Instance.GetComponent<Animator>().SetFloat("Speed_Multiplier",
        0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            PlayerController.Instance.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }

        PlayerController.Instance.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        PlayerController.Instance.gameOver = false;
    }
}
