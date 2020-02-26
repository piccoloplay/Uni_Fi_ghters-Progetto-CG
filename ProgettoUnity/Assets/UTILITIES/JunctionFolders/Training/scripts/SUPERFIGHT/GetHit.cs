using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetHit : MonoBehaviour
{
    public Animator animator;
    public GameObject enemy;


    public BoxCollider col;
    /// User Interface
    public bool alive = true;
    public Image currentHealth;
    public Text ratioHp;

    private  float maxHp = 150;
    private static float currentHp = 150;

    public void UpdateHealthBar()
    {
        float ratio = currentHp / maxHp;
        currentHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioHp.text = (ratio * 100).ToString("0") + "%";
    }

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        animator = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("3"))
        {
            animator.Play("DAMAGED00", -1, 0f);
        }
      
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fist")
        {
            animator.Play("DAMAGED00", -1, 0f);
            currentHp = currentHp - 5f;
            if (alive) UpdateHealthBar();
            if (currentHp <= 0) alive = false;
            if (currentHp <= 0)
            {
                animator.Play("knockdown_A", -1, 0f);

                Destroy(col);
            }
        }

        
    }
}
