using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BowlingScore : MonoBehaviour
{

    public GameObject[] pins;
    public TMP_Text scoreText;
    public GameObject Restartbutton;
    public GameObject ExitButton;
    public GameObject cam;

    public int score = 0;
    public Transform camPos;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (Restartbutton.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {      
            SceneManager.LoadScene("Bowling");
        } 
                        
        if (ExitButton.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {										
            SceneManager.LoadScene("Lobby"); 
        }

        cam.transform.position = camPos.position;

    }
}
