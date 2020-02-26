using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class FighterAI1 : Agent
{
    [Header("Specific to Basic")]
    private BasicAcademy m_Academy;
    private Animator animator;
    private MoveTable decider;
    ///////////////////
    public float timeBetweenDecisionsAtInference;
    private float m_TimeSinceDecision;
    public bool training=true;
    private float CenterPos = -0.5f;
    private float maxRadius = -2.5f;
    private float movementForward=0.25f;
    private float movementBack=-0.25f;
    private float stepKick = 0.1f;
    private float stepKickSide = -0.2f;
    public int iterazioni = 0;


    
    public GameObject enemy;




    // serve per trovare la Accademy nella scena
    public override void InitializeAgent()
    {

        setMovement();
        //GetComponent<CollisionDetection>().Input = stepKick*0.5f;
        m_Academy = FindObjectOfType(typeof(BasicAcademy)) as BasicAcademy;
        animator = GetComponent<Animator>();
        decider = new MoveTable();
    }


    public void setMovement()
    {
        if (GetComponent<CollisionDetection>().playerOne)
        {
            movementForward *= -1f;
            movementBack *= -1f;
            maxRadius *= -1f;
            CenterPos *= -1f;
            stepKick *= -1f;
        }
    }

    public override void CollectObservations()
    {
        //  i dati osservati e' meglio se vengono normalizzati
        AddVectorObs(GetComponent<CollisionDetection>().currentHp/150);  //tiene d'occhio la vita normalizzata
        AddVectorObs(enemy.GetComponent<CollisionDetection>().currentHp/150); // tiene d'occhio la vita dell'avversario normalizzata
        AddVectorObs(transform.position.x/maxRadius); // tiene d'occhio la propria posizione
        AddVectorObs(enemy.transform.position.x / maxRadius); // tiene d'occhio la posizione dell'avversario

        //AddVectorObs(iterazioni, 20);
    }




    // azione dell'agente
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (GetComponent<CollisionDetection>().currentHp > 0) // controllo per vedere se l'agente e' vivo
        {
            ActionMovement(vectorAction);
           
            if (Winning())
            {
                
                ActionFightOffensive(vectorAction,decider.Offense);


            }
            else
            {
               
                ActionFightDefensive(vectorAction,decider.Offense);
            }
        }
        else
        {
            AddReward(-10);
            if(training) GetComponent<CollisionDetection>().Reset();
            Done();
        }
        
            
        
        
        if (enemy.gameObject.GetComponent<CollisionDetection>().currentHp <= 0)
        {
            AddReward(1);
            Done();
        }
        else
        {
            AddReward(0.5f);
        }
        iterazioni++;
        if (iterazioni == 20)
        {
            Done();
        }
       
    }

    



    private void ActionFightDefensive(float[] vectorAction, Dictionary<int,string>offense)
    {
        
        
        var attack = (int)vectorAction[1];
        switch (attack)
        {
            case 0:


                ChangePosition(movementBack);
                ActivateGuard();
                // animator.Play(offense[0], 0, -1);
                ChangePosition(movementForward);
                AddReward(1f);
                    break;
               

            case 1:
                DeActivateGuard();
                //ChangePosition(stepKickSide);
                ChangePosition(stepKick);
                animator.Play(offense[2], 0, -1); //calcio circolare
                ChangePosition(stepKick);
                AddReward(0.1f);
                break;
            case 2:
                ActivateGuard();
                ChangePosition(stepKick*-1);
                DeActivateGuard();
                animator.Play(offense[0], 0, -1); // pugno
                //animator.Play(offense[1], 0, -1); // calcio laterale
                AddReward(0.49f);
                break;

            
            
        }

    }

    

    private void ActionFightOffensive(float[] vectorAction,Dictionary<int,string>offense)
    {
        
        var attack = (int)vectorAction[1];
        switch (attack)
        {
            case 0:
                    DeActivateGuard();
                    animator.Play(offense[0], 0, -1);
                
                ChangePosition(stepKick);
                    AddReward(0.5f);
               
                break;
                               

            case 1:
                DeActivateGuard();
               
                animator.Play(offense[1], 0, -1);
                ChangePosition(stepKick);
                AddReward(1f);
               
                break;
            case 2:
                DeActivateGuard();
                animator.Play(offense[2], 0, -1);
                ChangePosition(stepKick);
                AddReward(0.5f);
                break;
        }
    }

    public void ActionMovement(float[]vectorAction)
    {
        var actionMovement = (int)vectorAction[0];
        switch (actionMovement)
        {
            case 1:
                ChangePosition(movementForward);
                //animator.Play("forward", 0, -1);
                
                break;
            case 2:
                ChangePosition(movementBack);
                //animator.Play("backward", 0, -1);
                break;
        }

        if (Mathf.Abs(transform.position.x) >= 2.5)
        {
            transform.position = new Vector3(maxRadius + movementForward, 0, 0);
            //animator.Play("forward", 0, -1);
            AddReward(-1);
            Done();
        }
        if (Mathf.Abs(transform.position.x) <= 0.36)
        {
            transform.position = new Vector3(CenterPos + movementBack, 0, 0);
            //animator.Play("backward", 0, -1);
            AddReward(-1);
            Done();
        }
        if (Mathf.Abs(transform.position.x)  <= .5f && Mathf.Abs(transform.position.x)  > 0.37)
        {
            AddReward(1);

        }
        else
        {
            AddReward(-0.05f);
        }
        
        //iterazioni++;
        //if (iterazioni == 20)
        //{
        //    Done();
        //}
    }
    public void ChangePosition(float input)
    {
        Vector3 newPosition = new Vector3(transform.position.x + input, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.1f);
        
    }

    

    public bool Winning()
    {
        return GetComponent<CollisionDetection>().currentHp >= enemy.GetComponent<CollisionDetection>().currentHp;
    }


    public void ActivateGuard()
    {
        GetComponent<CollisionDetection>().ActivateGuard();
        
    }
    public void DeActivateGuard()
    {
        GetComponent<CollisionDetection>().DeActivateGuard();
       
    }
    
    public override void AgentReset()
    {
       

        iterazioni = 0;

    }
    // metodo che serve per controllare il personaggio manualmente
   
    public override float[] Heuristic()
    {
        int result = UnityEngine.Random.Range(0, 3);
        return new float[] { result};
    }



    



    public override void AgentOnDone()
    {
        if (Mathf.Abs(transform.position.x) >= Mathf.Abs(maxRadius))
        {
           
        }
        if (Mathf.Abs(transform.position.x) <= Mathf.Abs(CenterPos))
        {
          
        }

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
//************************************************************************************
public class MoveTable
{
    
    private Dictionary<int, string> offense = new Dictionary<int, string>();
    private Dictionary<int, string> defense = new Dictionary<int, string>();

    public Dictionary<int, string> Offense { get => offense; set => offense = value; }
    public Dictionary<int, string> Defense { get => defense; set => defense = value; }

    public MoveTable()
    {
        Offense.Add(0, "pugno");
        Offense.Add(1, "calcioLaterale");
        // Offense.Add(2, "calcioBasso");// guardia
        //idle_Tiramis
        Offense.Add(2, "calcio");
        Defense.Add(0, "idle_Tiramis");
        Defense.Add(1, "pugno");
        Defense.Add(2, "calcioLaterale");
    }





    public string getAnimationOffense(int input)
    {
        return Offense[input];
    }

    public string getAnimationDefense(int input)
    {
        return Defense[input];
    }
}
