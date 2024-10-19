using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private enum WeaponType {RANGED, MELEE}

    [SerializeField] private WeaponType type;
    [SerializeField] private int damage = 1;
    [SerializeField] private int RANGE = 1;
    [SerializeField] private float COOLDOWN = 0;

    [SerializeField] private float ATTACK_TIME = 1;

    [SerializeField] private GameObject projectile;
    [SerializeField] private bool owned = false;

    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hitSound;

    private Collider2D area;

    private SpriteRenderer sr;

    private float delta = 0;

    private Animator anim;

    private AudioSource audioSource;

    

    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        

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

        if(owned)
        {
            delta = Random.Range(0, COOLDOWN);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!owned)
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
        
        if(!attacking)
        {
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.parent.position, RANGE, Vector2.zero);
            if(enemies.Length > 0)
            {
                //Debug.Log("Enemies Hit");
                Transform tar = null;
                foreach(var hit in enemies)
                {
                    if(!hit.transform.tag.Equals(transform.parent.tag))
                    {
                        tar = hit.transform;
                        break;
                    }
                }
                if(tar == null)
                {
                    //Debug.Log("Enemy is NULL");
                    return;
                }
                
                foreach(var e in enemies)
                {
                    if(Vector2.Distance(transform.position, tar.position) > Vector2.Distance(transform.position, e.transform.position))
                    {
                        if(!e.transform.tag.Equals(transform.parent.tag))
                        {
                            tar = e.transform;
                        }
                        
                    }
                }
                attacking = true;
                sr.enabled = true;
                area.enabled = true;
                anim.SetTrigger("Attack");
                audioSource.PlayOneShot(attackSound);
                Projectile newPro = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                newPro.SetTarget(tar);
                newPro.SetDamage(damage);
                newPro.SetOrigin(GetComponent<Weapon>());
                if(!transform.parent.tag.Equals("Player"))
                {
                    newPro.isEnemyOwned();
                }
            }
        }

        if(delta > ATTACK_TIME)
        {
            attacking = false;
            sr.enabled = false;
            area.enabled = false;
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

    
    override public string ToString()
    {
        switch(type)
        {
            case WeaponType.MELEE:
                return $"{gameObject.name}\nType:	MELEE\nDmg:	{damage}\nCooldown:	{COOLDOWN} sec";
            case WeaponType.RANGED:
                return $"{gameObject.name}\nType:	RANGED\nRange:	{RANGE}\nDmg:	{damage}\nCooldown:	{COOLDOWN} sec\nPierce:  {projectile.GetComponent<Projectile>().isPierce()}";
            default:
                return "No WeaponType";
        }
        
    }


    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(owned && transform.parent && !other.tag.Equals(transform.parent.tag) && type == WeaponType.MELEE)
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
