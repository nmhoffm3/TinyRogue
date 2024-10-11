using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int MAX_HEALTH = 1;
    
    [SerializeField] private GameObject damageText;

    
    
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        HandleLife();
        Debug.Log(health.ToString());
    }


    private void HandleLife()
    {
        if(health < 1)
        {
            Destroy(gameObject);
        }

        if(health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }

    }

    public void ApplyDamage(int num) 
    {
            if(num > 0)
            {
                health -= num;
                GameObject newPop = Instantiate(damageText, transform.position - new Vector3(0.5f,0.5f), Quaternion.identity);
                TextMeshProUGUI damage = newPop.GetComponentInChildren<TextMeshProUGUI>();
                damage.color = Color.red;
                damage.text = num.ToString();
            }
    }



}
