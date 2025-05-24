using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    public Button quitButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(() =>
        {
            GameManager.Instance.OnQuit();
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
