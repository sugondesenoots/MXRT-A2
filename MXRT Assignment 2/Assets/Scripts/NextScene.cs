using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public int sceneIndex;
    public Button nextSceneBtn;

    public void WaitForNextSceneClick()
    {
        nextSceneBtn.onClick.AddListener(GoToNextScene); 
    }
     
    void GoToNextScene()
    {
        SceneManager.LoadScene(1);
        nextSceneBtn.onClick.RemoveAllListeners();
    }
}
