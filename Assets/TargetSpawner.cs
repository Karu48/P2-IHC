using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetSpawner : MonoBehaviour
{

    public GameObject[] targetPrefab;
    public int maxTargets = 3;
    public int activeTargets = 0;
    public int score = 0;   

    public TMP_Text scoreText;

    public float time = 60;
    public GameObject RestartButton;
    public GameObject ExitButton;
	public GameObject ReadyButton;
    bool Ingame = false;
	bool ready = false;
    float startTimer = 3;

    void Start()
    {
		RestartButton.SetActive(false);
		ExitButton.SetActive(false);

        foreach (GameObject target in targetPrefab)
        {
            target.SetActive(false);
        }
    }

    void Update()
    {
        if (Ingame)
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

                RestartButton.SetActive(true);
                ExitButton.SetActive(true);

                if (RestartButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
                {      
                    SceneManager.LoadScene("Shooting");
                } 
                                
                if (ExitButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
                {										
                    SceneManager.LoadScene("Lobby"); 
                }
            }
            else
            {
                while (activeTargets < maxTargets)
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
        }
        else
        {
            if (ready)
            {
                startTimer = startTimer - Time.deltaTime;
                scoreText.text = "Game Starts in: " + startTimer.ToString("F0");
                if (startTimer <= 0)
                {
                    Ingame = true;
                    ready = false;
                    startTimer = 3;
                    maxTargets = 3;
                    scoreText.text = "Score: " + score + "\nTime: " + time.ToString("F2");
                }
            }
            else
            {
                    if (ReadyButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
                    {
                        ready = true;
                        ReadyButton.SetActive(false);
                    }
            }
        }
    }

    public void hitTarget(GameObject target)
    {
        target.SetActive(false);
        activeTargets--;
        score++;    
    }
}
