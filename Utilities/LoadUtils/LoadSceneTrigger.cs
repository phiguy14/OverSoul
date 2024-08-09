using UnityEngine;
using PixelCrushers;


public class LoadSceneTrigger : MonoBehaviour
{
    private string spawnPoint;
    public string sceneName;

    public void Start()
    {
        spawnPoint = this.gameObject.name;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            if (InputDeviceManager.DefaultGetButtonDown("Enter"))
                Submit();
    }

    public void Submit()
    {
        SceneManager.LoadScene(sceneName, transform.position);
    }
}
