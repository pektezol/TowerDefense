using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    private float targetAspect = 1.77777778f;  // 16:9 aspect ratio
    public float targetWidth = 12.0f;  // Width you want to be visible in world units

    private Camera cam;
    private float initialOrthographicSize;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        initialOrthographicSize = cam.orthographicSize;
    }

    private void Update()
    {
        AdjustCameraSize();
    }

    private void Start()
    {
        AdjustAspectRatio();
        AdjustCameraSize();
    }

    private void AdjustAspectRatio()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }

    private void AdjustCameraSize()
    {
        float targetAspect = targetWidth / initialOrthographicSize;
        float windowAspect = (float)Screen.width / Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            cam.orthographicSize = initialOrthographicSize / scaleHeight;
        }
    }
}