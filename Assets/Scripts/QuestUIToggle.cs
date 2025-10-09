using UnityEngine;

public class QuestUIToggle : MonoBehaviour
{
    public Animator questPanelAnimator;

    private bool isQuestPanelOpen = false;

    public void ToggleQuestPanel()
    {
        isQuestPanelOpen = !isQuestPanelOpen;
        questPanelAnimator.SetBool("isOpen", isQuestPanelOpen);
    }
}
