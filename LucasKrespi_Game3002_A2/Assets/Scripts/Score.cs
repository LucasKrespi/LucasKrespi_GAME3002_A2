using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Score : MonoBehaviour
{
    private BallBehavior ball;
    private TextMeshProUGUI score;
 
    
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Sphere").GetComponent<BallBehavior>();
        score = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "SCORE: " + ball.m_fScore;

    }

}
