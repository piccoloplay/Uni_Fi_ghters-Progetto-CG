using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JABscript : MonoBehaviour
{
    public bool alive = true;
    float a = 0;
    float b = 0;
    float c = 0;
    public Image currentHealth;
    public Text ratioHp;

    private float maxHp = 150;
    private float currentHp = 150;

    public void UpdateHealthBar()
    {
        float ratio = currentHp / maxHp;
        currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioHp.text = (ratio * 100).ToString("0") + "%";
    }
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        movement();
        fight();

    }

    public void movement()
    {
        if (Input.GetKeyDown("a"))
        {
            a += .5f;
            transform.position = new Vector3(a, b, c);
        }
        if (Input.GetKeyDown("d"))
        {
            a -= .5f;
            transform.position = new Vector3(a, b, c);
        }
        if (Input.GetKeyDown("w"))
        {
            c += .5f;
            transform.position = new Vector3(a, b, c);
        }
        if (Input.GetKeyDown("s"))
        {
            c -= .5f;
            transform.position = new Vector3(a, b, c);
        }


    }

    public void movementAnimation()
    {
        
            
        
    }


    public void fight()
    {
        if (Input.GetKeyDown("1"))
        {
            animator.Play("Jab", -1, 0f);
        }
        if (Input.GetKeyDown("2"))
        {
            animator.Play("Hikick", -1, 0f);
        }
        if (Input.GetKeyDown("3"))
        {
            animator.Play("Punch", -1, 0f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("collisione");
    }


    public void OnMouseDown()
    {
        Debug.Log("ou");
        currentHp = currentHp - 25f;
        if(alive) UpdateHealthBar();
        if (currentHp <= 0) alive = false;

    }
}
