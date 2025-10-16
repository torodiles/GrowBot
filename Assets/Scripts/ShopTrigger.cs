using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopManager shopManager;
    public UIManager UImanager;
    public GameObject shopUIPanel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopManager.OpenShop();
            shopUIPanel.SetActive(true);
            UImanager.HideGameUI();
        }
    }
}
