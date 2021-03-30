using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallBehavior : MonoBehaviour
{
    private Rigidbody ball;
    public float m_fScore;
    private bool m_bIsReseting = false;
    AudioManager audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("GM").GetComponent<AudioManager>();
        m_fScore = 0.0f;
        ball = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if Ball is out of the board
        if(ball.transform.position.magnitude > 50.0f)
        {
            StartCoroutine(ResetBall());
        }

       
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        m_fScore += 5.0f;
        if(collision.gameObject.tag == "bActive") //collision with Active bumper
        {
            ReflectProjectile(ball, collision.contacts[0].normal, 2.0f);
            m_fScore += 50.0f;
           
        }

        if (collision.gameObject.tag == "bPassive")//collision with passive bumper
        {
            ReflectProjectile(ball, collision.contacts[0].normal, 0.9f);
            m_fScore += 25.0f;
            
        }

        if (collision.gameObject.tag == "bBashToy")//collision with Bashtoy
        {
            m_fScore += 1500.0f;
            audioManager.Play("bashtoy");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!m_bIsReseting)
        {
            m_bIsReseting = true;
            StartCoroutine(ResetBall());
        }
    }

    private void ReflectProjectile(Rigidbody ball, Vector3 reflectVector, float multiplier)
    {
        ball.velocity = Vector3.Reflect(ball.velocity, reflectVector) * multiplier;
    }

    IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(2.0f);

        ball.transform.position = new Vector3(7.73f, 1.24f, 16.39f);
        ball.velocity = Vector3.zero;
        m_fScore -= 1000;
        m_bIsReseting = false;
    }
   
}

