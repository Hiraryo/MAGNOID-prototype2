using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //Inspector
    [SerializeField] private string _nextSceneName;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(_nextSceneName);
    }
}
