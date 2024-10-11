using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private float spawnTime = 1;

    [SerializeField] private int spawnQuantity = 1;

    [SerializeField] private float spawnRange = 1;

    [SerializeField] private int spawnMax = 1;

    private GameObject player;

    private int spawnCount = 0;

    private float td = 0;

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
        HandleSpawning();
    }


    private void HandleSpawning()
    {
        td += Time.deltaTime;
        if(td >= spawnTime && spawnCount < spawnMax)
        {
            td = 0;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        int index = Random.Range(0, enemies.Count);
        GameObject choice = enemies[index];
        for (int i = 0; i < spawnQuantity; i++)
        {
            if(spawnCount >= spawnMax)
            {
                return;
            }
            Vector2 cir = Random.insideUnitCircle.normalized;
            Vector3 pos = new Vector3(cir.x, cir.y, 0) * spawnRange;
            Instantiate(choice, player.transform.position + pos, choice.transform.rotation).name = choice.name;
            spawnCount += 1;
            
        }
    }

    public void DecreaseCount()
    {
        spawnCount -= 1;
    }
}
