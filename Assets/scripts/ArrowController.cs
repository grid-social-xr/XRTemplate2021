using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject[] destinations;
    public GameObject ArrowMesh;

    private int currentDestinationIndex = 0;
    private int nextDestinationIndex = 1;
    private Transform targetTransform;

    void Start()
    {
        targetTransform = destinations[0].transform;
        currentDestinationIndex = 0;
        nextDestinationIndex = 1;
    }

    void LateUpdate()
    {
        if (cameraTransform == null || targetTransform == null)
        {
            ArrowMesh.SetActive(false);
            return;
        }

        // Get the direction vector from the camera to the target
        Vector3 targetDirection = targetTransform.position - cameraTransform.position;

        // Calculate the rotation to point the arrow in the target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Set the rotation of the arrow to point in the target direction
        transform.rotation = targetRotation;

        // Enable the ArrowMesh child object
        ArrowMesh.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination"))
        {
            // Disable the current destination object
            other.gameObject.SetActive(false);

            // Check if there are more destinations to collect
            if (nextDestinationIndex < destinations.Length)
            {
                // Update the target transform to the next destination
                targetTransform = destinations[nextDestinationIndex].transform;

                // Update the current and next destination indexes
                currentDestinationIndex = nextDestinationIndex;
                nextDestinationIndex++;

                // Debug.Log the current and next destination indexes for testing
                Debug.Log("Current destination index: " + currentDestinationIndex + ", Next destination index: " + nextDestinationIndex);
            }
            else
            {
                // There are no more destinations to collect
                // Set the target transform to null to stop pointing the arrow
                targetTransform = null;
            }
        }
    }
}
