using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxLayer : MonoBehaviour
{
    [Header("Parallax Factors")]
    [Range(-1f, 1f)]
    public float parallaxFactorX = 0.1f;  // Horizontal (lower = farther)
    [Range(-1f, 1f)]
    public float parallaxFactorY = 0.05f; // Vertical

    [Header("Zoom Scaling")]
    public bool scaleWithZoom = true;
    public float baseOrthoSize = 5f;      // Your default camera size

    private Camera cam;
    private Vector3 basePosition;
    private float startCamSize;

    void Start()
    {
        cam = Camera.main;
        basePosition = transform.position;
        startCamSize = cam.orthographicSize;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 offset = new Vector3(
            cam.transform.position.x * parallaxFactorX,
            cam.transform.position.y * parallaxFactorY,
            0f
        );

        if (scaleWithZoom)
        {
            float zoomScale = startCamSize / cam.orthographicSize;
            offset *= zoomScale;
        }

        transform.position = basePosition + offset;
    }
}