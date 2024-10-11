using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float m_Speed = 1;

    [SerializeField] private float stopDist = 1;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!player)
        {
            Debug.LogWarning("PLAYER IS NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(!player)
        {
            Debug.Log("Player is NULL");
            return;
        }
        if(Vector2.Distance(player.transform.position, transform.position) > stopDist)
        {
            Vector2 md = player.transform.position - transform.position;
            md = md.normalized * m_Speed * Time.deltaTime;
            transform.Translate(md);
        }
        
    }
}
