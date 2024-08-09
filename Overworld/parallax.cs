using UnityEngine;

public class parallax : MonoBehaviour
{
    public float parallaxEffect;
    private float spriteLen, origin;
    private GameObject cam;
    private Vector3 position;

    void Start(){
        position= transform.position;
        spriteLen = this.GetComponent<SpriteRenderer>().bounds.size.x;
        origin = transform.position.x;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate(){
        float distance = cam.transform.position.x * parallaxEffect;
        position = new Vector3(origin + distance, position.y, position.z);
    }
}
