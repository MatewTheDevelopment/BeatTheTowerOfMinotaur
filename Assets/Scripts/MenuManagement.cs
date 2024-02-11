using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    [SerializeField] private Animation fade;

    [SerializeField] private AnimationClip fadeOpen, fadeClose;

    private int index;

    private void Awake()
    {
        fade.Play(fadeOpen.name);

        Invoke("FadeExecute", fade.GetClip(fadeOpen.name).length);
    }

    public void LevelExecute(int levelIndex)
    {
        fade.gameObject.SetActive(true);

        index = levelIndex;

        fade.Play(fadeClose.name);

        Invoke("SceneLoad", fade.GetClip(fadeClose.name).length);
    }

    public void ExitExecute()
    {
        Application.Quit();
    }

    private void FadeExecute()
    {
        fade.gameObject.SetActive(false);
    }

    private void SceneLoad()
    {
        SceneManager.LoadScene(index);
    }
}
