using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class PopUp : MonoBehaviour
{
    [SerializeField] private float initFontSize = 1;
    [SerializeField] private float minFontSize = 0;

    [SerializeField] private float moveSpeed = 1;

    [SerializeField] private float shrinkSpeed = 1;

    private TextMeshProUGUI popUp;

    private Vector2 dir = Vector2.zero;

    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        popUp = GetComponentInChildren<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
        popUp.fontSize = initFontSize;
        dir = new Vector2(Random.Range(-2.0f,2.0f), 1);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleShrink();
    }

    private void HandleMovement()
    {
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        Debug.Log(transform.position);
    }

    private void HandleShrink()
    {
        popUp.fontSize = Mathf.Lerp(popUp.fontSize, minFontSize, shrinkSpeed * Time.deltaTime);
        if(popUp.fontSize <= minFontSize + 0.001f)
        {
            Destroy(gameObject);
        }
    }
}
