using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Item : MonoBehaviour
{
    [SerializeField]
    private int maxRotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        ApplyRandomRotation();
        ApplyRandomDrag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyRandomRotation()
    {
        float randomX = Random.Range(-180f, 180f);
        float randomY = Random.Range(-180f, 180f);
        float randomZ = Random.Range(-180f, 180f);

        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, randomZ);

        transform.rotation = randomRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomRotationSpeed = new Vector3(
                Random.Range(-maxRotationSpeed, maxRotationSpeed),
                Random.Range(-maxRotationSpeed, maxRotationSpeed),
                Random.Range(-maxRotationSpeed, maxRotationSpeed)
            );
            rb.angularVelocity = randomRotationSpeed;
        }
    }

    void ApplyRandomDrag()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = Random.Range(2f, 3.7f);
        }
    }


}
