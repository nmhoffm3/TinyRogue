
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool pierce;
    [SerializeField] private float lifeTime = 1;
    private Transform target;
    private int damage;
    private bool hit = false;
    private bool enemyOwned = false;
    private Weapon origin;
    private Vector2 dir = Vector2.zero;
    private void Start() 
    {
        dir = target.position - transform.position;
    }

    private void Update() 
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0 || (hit && !pierce))
        {
            Destroy(gameObject);
        }
        
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void SetTarget(Transform tar)
    {
        target = tar;
    }
    
    public void SetOrigin(Weapon w)
    {
        origin = w;
    }

    public void isEnemyOwned()
    {
        enemyOwned = true;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag.Equals("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            if(!e) return;
            e.DecreaseHealth(damage);
            origin.PlayHitSound();
            hit = true;
        }

        if(other.tag.Equals("Player") && enemyOwned)
        {
            Player p = other.GetComponent<Player>();
            if(!p) return;
            p.ApplyDamage(damage);
            hit = true;
        }
    }
    
}
