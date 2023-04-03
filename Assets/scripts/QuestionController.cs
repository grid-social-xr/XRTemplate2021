
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public Canvas gameMenuCanvas;
    public GameObject questionPanel;
    public GameObject guidePanel;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Menu triggered");
            gameMenuCanvas.enabled = true;
            questionPanel.SetActive(true);
            guidePanel.SetActive(false);

        }
    }
}
