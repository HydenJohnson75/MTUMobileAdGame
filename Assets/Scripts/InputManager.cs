using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private bool isLeftButtonHeld;
    private bool isRightButtonHeld;
    [SerializeField]
    private GameObject basket;
    private Basket basketScript;

    public void OnMoveLeft(InputValue value)
    {

        isLeftButtonHeld = Convert.ToBoolean(value.Get<float>());

        if (isLeftButtonHeld)
        {
            print("Left Button is held");
        }
    }

    public void OnMoveRight(InputValue value)
    {
        isRightButtonHeld = Convert.ToBoolean(value.Get<float>());

        if (isRightButtonHeld)
        {
            print("Right Button is held");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        basketScript = basket.GetComponent<Basket>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeftButtonHeld)
        {
            basketScript.MoveLeft();
        }
        else if (isRightButtonHeld)
        {
            basketScript.MoveRight();
        }
    }
}
