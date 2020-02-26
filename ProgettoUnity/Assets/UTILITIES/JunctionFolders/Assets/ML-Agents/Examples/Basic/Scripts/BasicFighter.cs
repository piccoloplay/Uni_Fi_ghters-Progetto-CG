using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public class BasicFighter : Agent
{
    /// variabili che ci devono stare
    private BasicAcademy m_Academy;
    public float timeBetweenDecisionsAtInference;
    private float m_TimeSinceDecision;
    // serve per trovare la Accademy nella scena
    public override void InitializeAgent()
    {
        m_Academy = FindObjectOfType(typeof(BasicAcademy)) as BasicAcademy;
    }


    private void WaitTimeInference()
    {
        if (!m_Academy.GetIsInference())
        {
            RequestDecision();
        }
        else
        {
            if (m_TimeSinceDecision >= timeBetweenDecisionsAtInference)
            {
                m_TimeSinceDecision = 0f;
                RequestDecision();
            }
            else
            {
                m_TimeSinceDecision += Time.fixedDeltaTime;
            }
        }
    }

    public void FixedUpdate()
    {
        WaitTimeInference();
    }





    /////////////////////////////////////////////////////////////
    ///
    /// azione dell'agente
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
    }


    
    public override void AgentReset()
    {
       
    }

    public override void CollectObservations()
    {
        AddVectorObs( 20);
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //return new float[] { 2 };
            return new float[] { 2 };
        }
        if (Input.GetKey(KeyCode.A))
        {
            return new float[] { 1 };
        }
        return new float[] { 0 };
    }

}
