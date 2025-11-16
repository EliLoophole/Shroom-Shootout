using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicCamera : MonoBehaviour
{

    private Vector3 targetPos;
    private Camera cam;

    private Transform tform;

    [SerializeField]
    private float zoomAmount = 1.0f;
    private float defaultSize;

    //Camera Follow Mouse
    public float followStrength = 0.1f;  
    public float maxOffset = 3f;         

    [SerializeField]
    private Transform target;

    public float followSpeed = 1.0f;
    public float zoomSpeed = 1.0f;

    void Start()
    {
        tform = this.transform;
        cam = this.GetComponent<Camera>();
        defaultSize = cam.orthographicSize;
    }

    void Update()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        mouseScreenPos.x = Mathf.Clamp(mouseScreenPos.x, 0f, Screen.width);
        mouseScreenPos.y = Mathf.Clamp(mouseScreenPos.y, 0f, Screen.height);

        Vector3 mouseViewport = cam.ScreenToViewportPoint(mouseScreenPos);


        Vector3 offset = new Vector3(
            (mouseViewport.x - 0.5f) * 2f,
            (mouseViewport.y - 0.5f) * 2f,
            0f
        ) * maxOffset * followStrength;

        if(Mathf.Abs((defaultSize / zoomAmount) - cam.orthographicSize) > 0.01)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, (defaultSize / zoomAmount) + 0.1f, zoomSpeed * Time.deltaTime);
        }
        
        if(target != null)
        {
            targetPos = new Vector3(target.position.x,target.position.y,-10f);
            targetPos += offset;
            this.transform.position = Vector3.Lerp(tform.position,targetPos,followSpeed * Time.deltaTime);            
        }
        else
        {
             this.transform.position = Vector3.Lerp(tform.position,new Vector3(0.0f,0.0f,-10.0f),followSpeed * Time.deltaTime);                
        }

    }

    public void FocusOn(Transform focusTarget, float focusZoom)
    {
        target = focusTarget;
        zoomAmount = focusZoom;
    }
}
