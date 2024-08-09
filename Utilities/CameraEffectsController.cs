using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using PixelCrushers.DialogueSystem.Demo;

public class CameraEffectsController : MonoBehaviour
{
    private SmoothCameraWithBumper smoothCamera;
    private Transform player;
    private bool instensityDirection = true;
    private int intensityTarget;
    private Bloom bloom;
    private SpriteRenderer bloomSprite;
    private Vector3 endCameraPosition;

    void Start()
    {
        bloom = transform.GetChild(0)
            .gameObject.GetComponent<PostProcessVolume>()
            .profile.GetSetting<Bloom>();
        // lensDistortion = transform.GetChild(0)
        //     .gameObject.GetComponent<PostProcessVolume>()
        //     .profile.GetSetting<LensDistortion>();
        smoothCamera = GetComponent<SmoothCameraWithBumper>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bloomSprite = transform.GetChild(0)
            .gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        SetSmoothCamMove();
        // if (lensDistortion.active)
        // {
        //     if (lensDistortion.intensity > 40 || lensDistortion.intensity < -40)
        //     {
        //         lensIntensirtyDirection = !lensIntensirtyDirection;
        //     }
        //     var intensityLens = instensityDirection ? 50 : -50;
        //     Debug.Log(lensDistortion.intensity);
        //     lensDistortion.intensity.Interp(lensDistortion.intensity, intensityLens, 2 * Time.deltaTime);
        // }

    }
    private void CheckBloomDirection()
    {
        var bloomShouldSwitchDirection = bloom.intensity >= 200 || bloom.intensity <= 150;

        if (bloomShouldSwitchDirection)
        {
            instensityDirection = !instensityDirection;
            intensityTarget = instensityDirection ? 230 : 120;
        }
    }

    private void SetSmoothCamMove()
    {
        var playerBelowBloomThreshold = player.transform.position.y <= PlatformerBorderController.bottomY + 2.5f;
        if (playerBelowBloomThreshold)
        {
            smoothCamera.enabled = false;
            endCameraPosition = new Vector3(0, PlatformerBorderController.bottomY, smoothCamera.transform.position.z);
            smoothCamera.transform.position = Vector3.Lerp(smoothCamera.transform.position, endCameraPosition, 3 * Time.deltaTime);
            StartBloom();
        }
        else
        {
            smoothCamera.enabled = true;
            StopBloom();
        }
    }

    private void StartBloom()
    {
        bloomSprite.enabled = true;
        CheckBloomDirection();
        bloom.intensity.Interp(bloom.intensity, intensityTarget, 2 * Time.deltaTime);
    }
    private void StopBloom()
    {
        bloomSprite.enabled = false;
    }
}
