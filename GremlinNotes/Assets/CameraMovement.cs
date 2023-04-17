using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    #region Variables

    [Header("Vertical Movement Varibles")]
    [SerializeField] float verticalSpeed = 2.0f;
    [SerializeField] float maxHeight = 10.0f;
    [SerializeField] float minHeight = 1.0f;

    [Header("Zooom Movement Variables")]
    [SerializeField] float zoomSpeed = 2.0f;
    [SerializeField] float maxZoom = 10.0f;
    [SerializeField] float minZoom = 1.0f;

    private float currentHeight;
    private float currentZoom;

    private CinemachineVirtualCamera vCam;
    private CinemachineTransposer transposer;

    #endregion
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();

        transposer.m_FollowOffset.y = minHeight;
        transposer.m_FollowOffset.z = maxZoom;
    }

    void Update()
    {
        VerticalCameraMovement();
        ZoomCameraMovement();
    }

    void VerticalCameraMovement()
    {
        // Get the vertical input
        float verticalInput = Input.GetAxis("Vertical");


        // Calculate the new height
        currentHeight += verticalInput * verticalSpeed * Time.deltaTime;
        currentHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);

        // Update the camera position
        Vector3 newPosition = transposer.m_FollowOffset;
        newPosition.y = currentHeight;
        transposer.m_FollowOffset = newPosition;
    }

    void ZoomCameraMovement()
    {
        float ScrollInput = Input.GetAxis("Mouse ScrollWheel");

        currentZoom += ScrollInput * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Vector3 newPosition = transposer.m_FollowOffset;
        newPosition.z = currentZoom;
        transposer.m_FollowOffset = newPosition;
    }
}