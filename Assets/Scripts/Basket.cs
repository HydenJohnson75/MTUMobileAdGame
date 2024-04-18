using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float boundaryX = 2.5f;

    void Update()
    {

    }
    
    public void MoveLeft()
    {

        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}