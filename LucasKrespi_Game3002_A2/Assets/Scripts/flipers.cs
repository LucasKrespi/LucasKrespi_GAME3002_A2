using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipers : MonoBehaviour
{
    
    public float flipperPower = 10000f;
    public float fliperDampper = 150f;
    public float restPos;
    public float hitPos;
    private AudioManager audioManager;


    private HingeJoint hinge;
    public string inputName;
    void Start()
    {
        audioManager = GameObject.Find("GM").GetComponent<AudioManager>();
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    }

    // Update is called once per frame
    void Update()
    {
        //create the spring and set damper and power
        JointSpring spring = new JointSpring();
        spring.spring = flipperPower;
        spring.damper = fliperDampper;


        //get input based on the input name created on the projects settings
        if (Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = hitPos;
            
        }
        else
        {
            spring.targetPosition = restPos;
          
        }
        //get imput for sounds
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Semicolon))
        {
            audioManager.Play("flipper");
        }
        //esc to leave game
        if (Input.GetKey(KeyCode.Escape))
        {
            
            Application.Quit();
           
        }

        hinge.spring = spring;
        hinge.useLimits = true;

    }
}
