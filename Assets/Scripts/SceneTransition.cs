using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionTime = 1f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MoveToScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    IEnumerator LoadSceneWithFade(string sceneName)
    {
        transitionAnimator.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);

        transitionAnimator.SetTrigger("Start");
    }
}
