using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GPSController : MonoBehaviour
{
    public float targetLatitude;
    public float targetLongitude;
    public Text distanceText;

    private float currentLatitude;
    private float currentLongitude;

    private IEnumerator Start()
    {
        // Set target coordinates for Alice Jennings Archibald Park
        // Alice Jennings Archibald Park in New Brunswick, NJ. (40.4841199, -74.4694864)
        targetLatitude = 40.4841199f;
        targetLongitude = -74.4694864f;
        // Check if the device supports location services
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services not enabled.");
            yield break;
        }

        // Start location service
        Input.location.Start();

        // Wait until location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Check if location service failed to start
        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Location service failed to start.");
            yield break;
        }

        // Update GPS coordinates every second
        while (true)
        {
            currentLatitude = Input.location.lastData.latitude;
            currentLongitude = Input.location.lastData.longitude;

            float distance = CalculateDistance(currentLatitude, currentLongitude, targetLatitude, targetLongitude);
            distanceText.text = $"{distance} meters";

            yield return new WaitForSeconds(1);
        }
    }

    private void OnDestroy()
    {
        // Stop location service when the script is destroyed
        Input.location.Stop();
    }

    private float CalculateDistance(float lat1, float lon1, float lat2, float lon2)
    {
        float R = 6371e3f; // Earth's radius in meters
        float lat1Rad = lat1 * Mathf.Deg2Rad;
        float lat2Rad = lat2 * Mathf.Deg2Rad;
        float deltaLat = (lat2 - lat1) * Mathf.Deg2Rad;
        float deltaLon = (lon2 - lon1) * Mathf.Deg2Rad;

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
                  Mathf.Cos(lat1Rad) * Mathf.Cos(lat2Rad) *
                  Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return R * c;
    }
}
