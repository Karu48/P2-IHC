using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    public GameObject[] targetPrefab;
    public int maxTargets = 3;
    public int activeTargets = 0;
    public int score = 0;   

    public TMP_Text scoreText;

    public float time = 60;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject target in targetPrefab)
        {
            target.SetActive(false);
        }

        for (int i = 0; i < maxTargets; i++)
        {
            int randomIndex = Random.Range(0, targetPrefab.Length);
            while (targetPrefab[randomIndex].activeSelf)
            {
                randomIndex = (randomIndex + 1) % targetPrefab.Length;
            }
            targetPrefab[randomIndex].SetActive(true);
            activeTargets++;
        }
    }

    void Update()
    {
        time = time - Time.deltaTime;
        scoreText.text = "Score: " + score + "\nTime: " + time.ToString("F2");

        if (time <= 0)
        {
            scoreText.text = "Game Over\nScore: " + score;
            foreach (GameObject target in targetPrefab)
            {
                target.SetActive(false);
            }
        }   
    }

    public void hitTarget(GameObject target)
    {
        target.SetActive(false);
        activeTargets--;
        score++;
        if (activeTargets < maxTargets)
        {
            int randomIndex = Random.Range(0, targetPrefab.Length);
            while (targetPrefab[randomIndex].activeSelf || targetPrefab[randomIndex] == target)
            {
                randomIndex = (randomIndex + 1) % targetPrefab.Length;
            }
            targetPrefab[randomIndex].SetActive(true);
            activeTargets++;
        }
    }
}
