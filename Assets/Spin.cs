using UnityEngine;

public class Spin : MonoBehaviour {
    
    public float rotationSpeed = 10f; // Adjust to control how fast the cube rotates
    public Vector3 randomRotationDirection;

    private void Start()
    {
        // Set an initial random direction for rotation
        SetRandomRotationDirection();
    }

    private void Update()
    {
        // Rotate the cube around the chosen random direction
        transform.Rotate(randomRotationDirection * rotationSpeed * Time.deltaTime);
    }

    private void SetRandomRotationDirection()
    {
        // Generate a random direction vector
        randomRotationDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized; // Normalize to ensure consistent speed
    }

    // Optional: Call this method if you want to change direction periodically or on some event
    public void ChangeRandomDirection()
    {
        SetRandomRotationDirection();
    }
}
