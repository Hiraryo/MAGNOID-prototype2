using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //Inspector
    [SerializeField] private string _nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeScene",1.5f);
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(_nextSceneName);
    }
}
