using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public class Drop
{
    public GameObject obj;
    public int amount;
    [Range(0, 1)]
    public float rate;
}


public class Enemy : MonoBehaviour
{
    [SerializeField] private int MAX_HEALTH = 1;
    [SerializeField] private GameObject damageText;
    [SerializeField] private List<Drop> drops = new List<Drop>();

    [SerializeField] private int DAMAGE = 1;
    private int health = 0;

    private EnemySpawner enemySpawner;


    private float damageTime = 1;
    private float cooldown = -1;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        health = MAX_HEALTH;
        enemySpawner = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
        col = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleLife();
        HandleAttack();
    }

    


    private void HandleLife()
    {
        if(health <= 0)
        {
            SpawnDrops();
            Destroy(gameObject);
        }

        if(health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
    }


    private void HandleAttack()
    {
        if(cooldown != -1)
        {
            cooldown += Time.deltaTime;
            if(cooldown > damageTime)
            {
                cooldown = -1;
            }
        }
    }

    public void DecreaseHealth(int num)
    {
        if(health <= 0)
        {
            return;
        }
        health -= num;
        CreatePopUp(Color.red, num);
    }

    public void IncreaseHealth(int num)
    {
        health += num;
        CreatePopUp(Color.green, num);
    }


    private void CreatePopUp(Color c, int num)
    {
        GameObject newPop = Instantiate(damageText, transform.position - new Vector3(0.5f,0.5f), Quaternion.identity);
        TextMeshProUGUI damage = newPop.GetComponentInChildren<TextMeshProUGUI>();
        damage.color = c;
        damage.text = num.ToString();
    }

    private void SpawnDrops()
    {
        foreach(var d in drops)
        {
            float result = UnityEngine.Random.Range(0.0f, 1.0f);
            if(result < d.rate || d.rate == 1)
            {
                for(int i = 0; i < d.amount; i++)
                {
                    Vector2 spawnPos = UnityEngine.Random.insideUnitCircle;
                    spawnPos = spawnPos.normalized;
                    Instantiate(d.obj, transform.position + (Vector3) spawnPos, Quaternion.identity).name = d.obj.name;
                }
                
            }
        }
    }



    private void OnDestroy() 
    {
        enemySpawner.DecreaseCount();
    }

    private int Attack()
    {
        if(cooldown == -1)
        {
            cooldown = 0;
            return DAMAGE;
        }
        else
        {
            return 0;
        }
    }


    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag.Equals("Player"))
        {
            other.GetComponent<Player>().ApplyDamage(Attack());
        }
    }

    
    
}
