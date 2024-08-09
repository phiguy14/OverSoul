using UnityEngine;

public class changescenePlatformer : MonoBehaviour
{

    // private SceneManager sceneManager;
    private void Start()
    {
        // sceneManager = GameObject.FindGameObjectWithTag("SceneLoader")
        //     .GetComponent<SceneManager>();
    }

    public void LoadScene(string name)
    {
        //sceneManager.LoadScene(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "platformer" 
        //&& collision.gameObject.tag=="Player")
        //LoadScene(PixelCrushers.SaveSystem.lastScene);                    
        //else
        Destroy(collision.gameObject);
    }
}
