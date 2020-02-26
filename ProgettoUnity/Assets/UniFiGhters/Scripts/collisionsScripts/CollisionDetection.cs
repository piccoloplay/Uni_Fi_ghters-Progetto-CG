using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionDetection : MonoBehaviour
{
    public Animator animator;
    private float HpMax = 150;
    public float currentHp = 150;
    public Image currentHealth;
    public Text ratioHp;
    public GameObject guard;
    private bool defenseOn=false;
    private float input=-0.25f;
    public bool playerOne=true;
    public List<Transform> box = new List<Transform>();

    public bool DefenseOn { get => defenseOn; set => defenseOn = value; }
    public float Input { get => input; set => input = value; }

    public void UpdateHealthBar(float damage)
    {

        animator.Play("DAMAGE", -1, 0f);
        HitTransition();
        currentHp -= damage;
        float ratio = currentHp / HpMax;
        
        if (currentHp <= 0)
        {
            currentHealth.rectTransform.localScale = new Vector3(0, 1, 1);
            ratioHp.text = "DEAD";
            animator.Play("knockdown_A", -1, 0f);
        }
        else
        {
            currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
            ratioHp.text = (ratio * 100).ToString("0") + "%";
        }
    }


    public void Reset()
    {
        currentHp = 150;
        UpdateHealthBar(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (playerOne)
        {
            input *= -1;
        }
        animator = GetComponent<Animator>();
        foreach(Transform child in transform)
        {
            if (child.tag == "Fist"|| child.tag == "Kick")
            {
                box.Add(child);
            }


        }
    }

    
    public void HitTransition()
    {
        Vector3 destination = new Vector3(transform.position.x + Input, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position,destination, 0.5f);



    }



    public void DeactivateOffense()
    {
        foreach(Transform child in box)
        {
            //child.GetComponent<BoxCollider>().isTrigger = false;
            child.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void ActivateOffense()
    {
        foreach (Transform child in box)
        {
            //child.GetComponent<BoxCollider>().isTrigger = true;
            child.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void ActivateGuard()
    {
        DeactivateOffense();
        defenseOn = true;
        guard.GetComponent<Renderer>().enabled = true;

    }
    public void DeActivateGuard()
    {
        ActivateOffense();
        defenseOn = false;
        guard.GetComponent<Renderer>().enabled = false;
    }

}


