using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI fps;
    private float checkTime = 0.2f;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        fps = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > checkTime)
        {
            fps.text = $"FPS: {1 / Time.deltaTime}";
            counter = 0;
        }
            
        
    }
}
