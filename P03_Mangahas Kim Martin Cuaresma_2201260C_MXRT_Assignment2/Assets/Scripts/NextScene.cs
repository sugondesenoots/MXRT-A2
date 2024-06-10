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
     
    //Simple script to handle going into the next script using the button in the Start Screen
}
