using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject weapon;

    [SerializeField] private Image img;

    [SerializeField] private TMP_Text info;

    private LevelUpHandler handler;

    private Weapon weaponData;
    private Transform p;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p = GameObject.Find("Player").transform;
        weaponData = weapon.GetComponent<Weapon>();

        img.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        info.text = weaponData.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        Instantiate(weapon, p.position, Quaternion.identity).name = weapon.name;
        handler.SetChosen(true);
    }

    public void SetParam(GameObject w, LevelUpHandler h)
    {
        weapon = w;
        handler = h;
    }

    
}
