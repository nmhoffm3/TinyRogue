using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float m_Speed = 0;

    private Vector2 md = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }



    private void HandleMovement()
    {
        md = md.normalized * m_Speed * Time.deltaTime;
        transform.Translate(md);
    }

    public void OnMove(InputAction.CallbackContext c)
    {
        if(c.performed)
        {
            md = c.ReadValue<Vector2>();
        }
        if(c.canceled)
        {
            md = Vector2.zero;
        }
    }

}
