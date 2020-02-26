using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public class BasicAgent2 : Agent
{
    [Header("Specific to Basic")]
    private BasicAcademy m_Academy;
    
    private Decider decider;

    public float timeBetweenDecisionsAtInference;
    private float m_TimeSinceDecision;

    private Animator animator;
    public int posx = 0;

    // serve per trovare la Accademy nella scena
    public override void InitializeAgent()
    {
        m_Academy = FindObjectOfType(typeof(BasicAcademy)) as BasicAcademy;
        animator = GetComponent<Animator>();
        decider = new Decider();
    }

    public override void CollectObservations()
    {
        AddVectorObs(posx, 20);
    }




    // azione dell'agente
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        var input = (int)vectorAction[0];

        string movement = decider.getAnimation(input);
        //StartCoroutine(doTheAnimation(movement));
        //StopCoroutine(doTheAnimation(movement));
        switch (input)
        {
            case 0:
                Debug.Log(input);
                animator.Play(movement, 0);
               // StartCoroutine(doTheAnimation(movement));
                //StopCoroutine(doTheAnimation(movement));
                AddReward(1);
                //Done();
                break;
            case 1:
                animator.Play(movement, 0);
                //StartCoroutine(doTheAnimation(movement));
                //StopCoroutine(doTheAnimation(movement));
                AddReward(-0.01f);
                Debug.Log(input);
                //Done();
                break;
            case 2:
                //StartCoroutine(doTheAnimation(movement));
                //StopCoroutine(doTheAnimation(movement));
                animator.Play(movement, 0);
                AddReward(-0.01f);
                Debug.Log(input);
                //Done();
                break;
        }

        posx++;
        if (posx ==20)
        {
            Done();
        }
    }


    public IEnumerator doTheAnimation(string input)
    {
        animator.Play(input, 0, -1);
        yield return new WaitForSeconds(0.4f);
        
    }



    // reset
    // viene chiamato per la epoca
    public override void AgentReset()
    {
        posx = 0;
    }
    // metodo che serve per controllare il personaggio manualmente
    // ricordati che cosa ha detto il frasconi sulla codifica dei colori 
    public override float[] Heuristic()
    {
        int result = Random.Range(0, 3);
        return new float[] { result};
    }

    public override void AgentOnDone()
    {
    }

    public void FixedUpdate()
    {
        WaitTimeInference();
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
}

public class Decider
{
    
    private Dictionary<int, string> map = new Dictionary<int, string>();

    public Decider()
    {
        map.Add(0, "pugno");
        map.Add(1, "calcio");
        map.Add(2, "idle_Tiramis");
        
        
    }

    public string getAnimation(int input)
    {
        return map[input];
    }
}
