using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject bookUIPanel;
    public GameObject infoUIPanel;
    public GameObject hotbarUIPanel;

    [Header("Player Scripts")]
    public PlayerMovement playerMovementScript;

    private bool isBookOpen = false;

    void Start()
    {
        bookUIPanel.SetActive(false);
        infoUIPanel.SetActive(true);
        hotbarUIPanel.SetActive(true);
    }

    public void ToggleBookUI()
    {
        isBookOpen = !isBookOpen;

        bookUIPanel.SetActive(isBookOpen);

        infoUIPanel.SetActive(!isBookOpen);
        hotbarUIPanel.SetActive(!isBookOpen);

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = !isBookOpen;
        }

    }
}
