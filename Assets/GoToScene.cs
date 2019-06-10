using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}