using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float camSpeed = 1;

    private float stopDist = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (target == null)
        {
            Debug.LogWarning("CAMERA TARGET IS NULL");
            return;
        }
        if(Vector2.Distance(transform.position, target.position) > stopDist)
        {
            Vector2 md = target.position - transform.position;
            md = md.normalized * camSpeed * Time.deltaTime;
            transform.Translate(md);
        }
        
    }
}
