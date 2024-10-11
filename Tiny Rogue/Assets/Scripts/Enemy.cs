using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;


public class Enemy : MonoBehaviour
{
    [SerializeField] private int MAX_HEALTH = 1;
    [SerializeField] private GameObject damageText;
    [SerializeField] private List<GameObject> drops = new List<GameObject>();
    [SerializeField] private List<float> dropRates = new List<float>();

    [SerializeField] private int DAMAGE = 1;
    private int health = 0;

    private EnemySpawner enemySpawner;


    private float damageTime = 1;
    private float cooldown = -1;
    // Start is called before the first frame update
    void Start()
    {
        health = MAX_HEALTH;
        enemySpawner = GameObject.Find("GameManager").GetComponent<EnemySpawner>();

        if(drops.Count != dropRates.Count)
        {
            Debug.LogError("Drops and Drop Count lists are not of equal length!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleLife();
        HandleAttack();
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
        health -= num;
        GameObject newPop = Instantiate(damageText, transform.position - new Vector3(0.5f,0.5f), Quaternion.identity);
        TextMeshProUGUI damage = newPop.GetComponentInChildren<TextMeshProUGUI>();
        damage.color = Color.red;
        damage.text = num.ToString();
    }

    public void IncreaseHealth(int num)
    {
        health += num;
        GameObject newPop = Instantiate(damageText, transform.position - new Vector3(0.5f,0.5f), Quaternion.identity);
        TextMeshProUGUI damage = newPop.GetComponentInChildren<TextMeshProUGUI>();
        damage.color = Color.green;
        damage.text = num.ToString();
    }



    private void OnDestroy() 
    {
        enemySpawner.DecreaseCount();    
    }

    public int Attack()
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
