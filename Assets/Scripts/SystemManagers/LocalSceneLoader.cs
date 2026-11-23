using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private void Awake(){
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
    public void Quit()
    {
        Application.Quit();
    }


    // simple version first : 
    public void Load(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    // loading with transition here...

    
}
