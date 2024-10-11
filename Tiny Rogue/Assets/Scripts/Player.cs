using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Serializable]
    public class WeaponStack
    {
        public string name;
        public int stack;
        public WeaponStack(string newName, int newStack)
        {
            name = newName;
            stack = newStack;
        }        
    }

    [SerializeField] private int MAX_HEALTH = 1;
    
    [SerializeField] private GameObject damageText;

    [SerializeField] private int MAX_WEAPON_STACK = 5;
    
    private List<WeaponStack> weapons = new List<WeaponStack>();
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

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return MAX_HEALTH;
    }


    public bool AddWeapon(string newWeapon)
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            if(weapons[i].name.Equals(newWeapon))
            {
                if(weapons[i].stack >= MAX_WEAPON_STACK)
                {
                    return false;
                }
                weapons[i].stack++;
                return true;
            }
        }
        weapons.Add(new WeaponStack(newWeapon, 1));
        return true;
    }

}
