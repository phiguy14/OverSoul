using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;
    public static GameObject go;
    public static AudioSource[] audioSources = new AudioSource[5];

    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject helperObject = new GameObject("CoroutineHelper");
                _instance = helperObject.AddComponent<CoroutineHelper>();
                for (int i = 0; i < audioSources.Length; i++)
                {
                    audioSources[i] = helperObject.AddComponent<AudioSource>();
                }

                go = helperObject;
                DontDestroyOnLoad(helperObject);
                Debug.Log("CoroutineHelper instance created and added to the scene.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CoroutineHelper Awake: Instance is set.");
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            Debug.Log("CoroutineHelper Awake: Duplicate instance destroyed.");
        }
    }
}