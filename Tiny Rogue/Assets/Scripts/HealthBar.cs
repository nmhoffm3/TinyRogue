using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI healthDisp;
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
        int hp = player.GetHealth();
        int max = player.GetMaxHealth();
        float calc = (float) hp / max;
        healthDisp.text = $"{hp} / {max}";
        bar.fillAmount = calc;
    }
}
