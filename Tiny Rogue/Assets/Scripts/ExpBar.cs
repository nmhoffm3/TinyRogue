using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI expDisp;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player)
        {
            return;
        }
        int exp = player.GetExp();
        int lvl = player.GetLevel();
        int tar = player.GetTargetExp();
        float calc = (float) exp / tar;
        expDisp.text = $"Lvl {lvl}:   {exp} / {tar}";
        bar.fillAmount = calc;
    }
}