using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private enum WeaponType {RANGED, MELEE}

    [SerializeField] private WeaponType type;
    [SerializeField] private int damage = 1;

    [SerializeField] private float COOLDOWN = 0;

    [SerializeField] private float ATTACK_TIME = 1;

    [SerializeField] private GameObject projectile;
    [SerializeField] private bool owned = false;

    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hitSound;
    private Transform player;

    private Collider2D area;

    private SpriteRenderer sr;

    private float delta = 0;

    private Animator anim;

    private AudioSource audioSource;

    

    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(!player)
        {
            Debug.LogWarning("PLAYER IS NULL");
        }

        area = GetComponentInChildren<Collider2D>();
        if(!area)
        {
            Debug.LogWarning("AREA IS NULL");
        }

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if(transform.parent)
        {
            owned = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != player && !owned)
        {
            return;
        }

        HandleAttack();
    }


    private void HandleAttack()
    {
        delta += Time.deltaTime;

        if(delta < COOLDOWN && !attacking)
        {
            return;
        }

        if(!attacking)
        {
            delta = 0;
        }
        
        switch(type)
        {
            case WeaponType.RANGED:
                RangedAttack();
                break;
            
            case WeaponType.MELEE:
                MeleeAttack();
                break;
            
            default:
                break;
        }
    }

    private void RangedAttack()
    {
        if(delta > ATTACK_TIME)
        {
            attacking = false;
        }
    }

    private void MeleeAttack()
    {
        if (!attacking)
        {
            attacking = true;
            sr.enabled = true;
            area.enabled = true;
            anim.SetTrigger("Attack");
            audioSource.PlayOneShot(attackSound);
        }
       


        if(delta > ATTACK_TIME)
        {
            attacking = false;
            sr.enabled = false;
            area.enabled = false;
        }
    }

    private void IncreaseDamage(int num)
    {
        damage += num;
    }


    public bool GetOwned()
    {
        return owned;
    }

    public int GetDamage()
    {
        return damage;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(owned && transform.parent && other.gameObject != transform.parent)
        {
            if(other.tag.Equals("Enemy"))
            {
                Enemy e = other.GetComponent<Enemy>();
                if(!e) return;
                e.DecreaseHealth(damage);
                audioSource.PlayOneShot(hitSound);
            }

            if(other.tag.Equals("Player"))
            {
                Player p = other.GetComponent<Player>();
                if(!p) return;
                p.ApplyDamage(damage);
                audioSource.PlayOneShot(hitSound);
            }

            
        }

        if(other.tag.Equals("Player") && !owned)
        {
            Player p = other.GetComponent<Player>();
            if(!p.AddWeapon(gameObject.name))
            {
                Weapon[] weapons = other.GetComponentsInChildren<Weapon>();
                foreach(var w in weapons)
                {
                    if(w.name.Equals(gameObject.name))
                    {
                        w.IncreaseDamage(damage);
                    }
                }
                Destroy(gameObject);
                return;
            }
            transform.position = other.transform.position;
            transform.parent = other.transform;
            owned = true;
            sr.enabled = false;
            area.enabled = false; 
        }

        
    }

    

    




    
}
