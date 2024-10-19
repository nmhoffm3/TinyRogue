using System.Collections.Generic;
using UnityEngine;

public class LevelUpHandler : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel;

    [SerializeField] private Transform choicePanel;

    [SerializeField] private GameObject selectButton;

    [SerializeField] private List<GameObject> weapons;


    private Player p;

    private int curLevel = 0;

    private bool chosen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        levelUpPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(curLevel != p.GetLevel())
        {
            curLevel = p.GetLevel();
            Time.timeScale = 0;
            levelUpPanel.SetActive(true);
            GenerateWeapons();
        }

        if(chosen)
        {
            Time.timeScale = 1;
            levelUpPanel.SetActive(false);
            chosen = false;
        }
    }

    public void SetChosen(bool c)
    {
        chosen = c;
    }



    private void GenerateWeapons()
    {
        for(int i = 0; i < choicePanel.childCount; i++)
        {
            Destroy(choicePanel.GetChild(i).gameObject);
        }
        
        

        for(int i = 0; i < 2; i++)
        {
            GameObject newButton = Instantiate(selectButton);
            newButton.GetComponent<WeaponSelectButton>().SetParam(weapons[Random.Range(0,weapons.Count)], this);
            newButton.transform.SetParent(choicePanel);
        }
    }
}
