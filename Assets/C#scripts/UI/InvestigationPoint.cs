using UnityEngine;

public class InvestigationPoint : MonoBehaviour
{
    public GameObject InvestigationPanel;

    private void Awake()
    {
        InvestigationPanel.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvestigationPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvestigationPanel.SetActive(false);
        }
    }
}
