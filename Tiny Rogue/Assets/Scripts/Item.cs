using UnityEngine;

public class Item : MonoBehaviour
{
    private enum ItemType {EXP, HEALTH}

    [SerializeField] private ItemType type;

    [SerializeField] private int amount = 1;

    [SerializeField] private float speed = 1;

    [SerializeField] private float waitTime = 1;

    private Transform player;
    
    private float dt = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player)
        {
            Destroy(gameObject);
        }

        HandleMove();
    }


    private void HandleMove()
    {
        if(!player)
        {
            return;
        }
        if(dt < waitTime)
        {
            dt += Time.deltaTime;
            return;
        }
        Vector2 vd = player.position - transform.position;
        transform.Translate(vd.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform != player)
        {
            return;
        }

        Player p = player.GetComponent<Player>();

        switch(type)
        {
            case ItemType.EXP:
                p.ApplyExp(amount);
                break;
            case ItemType.HEALTH:
                p.ApplyHealth(amount);
                break;
        }

        
        Destroy(gameObject,0.01f);
    }





}
