using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public class FighterAI : Agent
{
    [Header("Specific to Basic")]
    private BasicAcademy m_Academy;
    
    private DeciderMove decider;

    public float timeBetweenDecisionsAtInference;
    private float m_TimeSinceDecision;

    private Animator animator;
    public int turni = 0;

    public float distanceEnemy;
    public GameObject selfFighter;
    public GameObject enemy;
    // serve per trovare la Accademy nella scena
    public override void InitializeAgent()
    {
        m_Academy = FindObjectOfType(typeof(BasicAcademy)) as BasicAcademy;
        animator = GetComponent<Animator>();
        decider = new DeciderMove();
    }

    public override void CollectObservations()
    {
        AddVectorObs(turni, 20);
    }




    // azione dell'agente
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
        var coordinates = (int)vectorAction[1];
        float coordinata=0;
        switch (coordinates)
        {
            
            case 1:
                coordinata = 0.35f;

                break;
            case 2:
                coordinata = -0.3f;
                break;
            
        }
        selfFighter.transform.position=new Vector3(selfFighter.transform.position.x + coordinata, selfFighter.transform.position.y, selfFighter.transform.position.z);
        MoveToEnemy(coordinata);
        //distanceEnemy = Mathf.Abs(selfFighter.transform.position.x) - Mathf.Abs(enemy.transform.position.x);
        //distanceEnemy = Mathf.Abs(selfFighter.transform.position.x - enemy.transform.position.x);
        if (distanceEnemy<=1)
        {
            AddReward(1f);
            Done();
            
        }
        else
        {
            AddReward(-0.05f);
           
        }


        ////////////////////////////////////////////
        var input = (int)vectorAction[0];

        string movement = decider.getAnimation(input);
        
        switch (input)
        {
            case 0:
                // Debug.Log(input);
                DeActivateGuard();
                animator.Play(movement, 0);
              
                AddReward(1);
               
                break;
            case 1:
                DeActivateGuard();
                animator.Play(movement, 0);
                
                AddReward(-0.01f);
               // Debug.Log(input);
                
                break;
            case 2:
                ActivateGuard();
                animator.Play(movement, 0);
                AddReward(-0.01f);
               // Debug.Log(input);
               
                break;
            case 3:
                
                ActivateGuard();
                AddReward(1f);
                break;
        }

        turni++;
        if (turni ==20)
        {
            Done();
        }

       
    }


 


    public void ActivateGuard()
    {
        selfFighter.GetComponent<CollisionDetection>().ActivateGuard();
        Debug.Log("guardiaAttiva");
    }
    public void DeActivateGuard()
    {
        selfFighter.GetComponent<CollisionDetection>().DeActivateGuard();
        Debug.Log("guardiaDisattiva");
    }
    // reset
    // viene chiamato per la epoca
    public override void AgentReset()
    {
       // distanceEnemy = Mathf.Abs(selfFighter.transform.position.x-enemy.transform.position.x);

        turni = 0;

    }
    // metodo che serve per controllare il personaggio manualmente
    // ricordati che cosa ha detto il frasconi sulla codifica dei colori 
    public override float[] Heuristic()
    {
        int result = Random.Range(0, 3);
        return new float[] { result};
    }



    public void MoveToEnemy(float input)
    {
        Vector3 posEnemy = new Vector3(selfFighter.transform.position.x+input,0,0);
        selfFighter.transform.position= Vector3.MoveTowards(selfFighter.transform.position, posEnemy, 0.1f);



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

public class DeciderMove
{
    
    private Dictionary<int, string> map = new Dictionary<int, string>();

    public DeciderMove()
    {
        map.Add(0, "pugno");
        map.Add(1, "calcio");
        map.Add(2, "idle_Tiramis");// guardia
        
        
    }

    public string getAnimation(int input)
    {
        return map[input];
    }
}
