using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool m_bIsBungee = false;

    private Vector3 m_attachedBodyinicialPos;

    float startTime;
    float ReleaseTime;
    float maxHoldTime = 2.0f;

    private Vector3 m_vForce;
    private Vector3 m_vPrevVel;

    // Start is called before the first frame update
    void Start()
    {
        m_fMass = m_attachedBody.mass;

        m_fSpringConstant = CalculateSpringConstant();

        m_vRestPos = anchorTransform.position;

        m_attachedBodyinicialPos = m_attachedBody.position;
    }

    Vector3 calculateForce(float time)
    {

        float HoldComponent = Mathf.Clamp01(time / maxHoldTime);


        return -m_fSpringConstant * (m_vRestPos - m_attachedBody.transform.position) -
            m_fDampingConstant * (m_attachedBody.velocity - m_vPrevVel) * HoldComponent;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ReleaseTime = Time.time;

            m_vForce = calculateForce((ReleaseTime - startTime));

            m_attachedBody.AddForce(m_vForce, ForceMode.Acceleration);

            m_vPrevVel = m_attachedBody.velocity;
        }
    }

    private float CalculateSpringConstant()
    {
        // k = F / dX
        // F = m * a
        // k = m * a / (xf - xi)

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

  
}
