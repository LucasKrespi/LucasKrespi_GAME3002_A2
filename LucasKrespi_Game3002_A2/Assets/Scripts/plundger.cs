using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plundger : MonoBehaviour
{
    [SerializeField]
    private float m_fSpringConstant;
    [SerializeField]
    private float m_fDampingConstant;
    [SerializeField]
    private Vector3 m_vRestPos;
    [SerializeField]
    private Transform anchorTransform;
    [SerializeField]
    private float m_fMass;
    [SerializeField]
    private Rigidbody m_attachedBody = null;
    [SerializeField]
    private Image forceBar;
    private bool hasLanched = false;
    AudioManager audioManager;


    private Vector3 m_attachedBodyinicialPos;

    float startTime;
    float ReleaseTime;
    float maxHoldTime = 2.0f;

    private Vector3 m_vForce;
    private Vector3 m_vPrevVel;

 
    void Start()
    {
        m_fMass = m_attachedBody.mass;

        m_fSpringConstant = CalculateSpringConstant();

        m_vRestPos = anchorTransform.position;

        m_attachedBodyinicialPos = m_attachedBody.position;

        hideForce();

        audioManager = GameObject.Find("GM").GetComponent<AudioManager>();
    }

    Vector3 calculateForce(float time)
    {
  
        return (-m_fSpringConstant * (m_vRestPos - m_attachedBody.transform.position) -
            m_fDampingConstant * (m_attachedBody.velocity - m_vPrevVel)) * calculateHoldComponent(time);
    }
    float calculateHoldComponent(float time)
    {
        return Mathf.Clamp01(time / maxHoldTime);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !hasLanched)
        {
            startTime = Time.time;
           
        }
        if (Input.GetKey(KeyCode.Space) && !hasLanched)
        {
            showForce(Time.time - startTime);
        }
        if (Input.GetKeyUp(KeyCode.Space) && !hasLanched)
        {
            ReleaseTime = Time.time;

            m_vForce = calculateForce((ReleaseTime - startTime));

            hideForce();

            m_attachedBody.AddForce(m_vForce, ForceMode.Acceleration);
            hasLanched = false;
            m_vPrevVel = m_attachedBody.velocity;
            audioManager.Play("plundger");
        }
    }

    private float CalculateSpringConstant()
    {

        float fDX = (m_vRestPos - m_attachedBody.transform.position).magnitude;

        if (fDX <= 0f)
        {
            return Mathf.Epsilon;
        }

        return (m_fMass * Physics.gravity.y) / (fDX);
    }

    private void FixedUpdate()
    {
       if(m_attachedBody.position.z >= m_attachedBodyinicialPos.z + 5)
        {
            m_attachedBody.position = m_attachedBodyinicialPos;
            m_attachedBody.velocity = new Vector3(0,0,0);
        }
    }

    void showForce(float time)
    {
        forceBar.fillAmount = time / maxHoldTime;
    }
    void hideForce()
    {
        forceBar.fillAmount = 0;
    }

}
