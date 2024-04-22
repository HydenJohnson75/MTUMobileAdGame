using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Item : MonoBehaviour
{
    [SerializeField]
    private int foodValue;
    [SerializeField]
    private float maxRotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        ApplyRandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyRandomRotation()
    {
        // Generate random rotation angles around each axis
        float randomX = Random.Range(-180f, 180f);
        float randomY = Random.Range(-180f, 180f);
        float randomZ = Random.Range(-180f, 180f);

        // Combine into a random rotation
        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, randomZ);

        // Apply the rotation to the GameObject
        transform.rotation = randomRotation;

        // Optionally, add a random rotation speed
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

    public int GetFoodValue()
    {
        return foodValue;
    }
}
