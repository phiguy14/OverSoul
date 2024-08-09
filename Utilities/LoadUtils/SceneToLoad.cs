using UnityEngine;

public class SceneToLoad : MonoBehaviour
{
    public string sceneName;
    public string gameObjectName;
    public Vector3 position;

    private void Awake()
    {
        position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene(sceneName + "@" + gameObjectName, position);
    }
}
