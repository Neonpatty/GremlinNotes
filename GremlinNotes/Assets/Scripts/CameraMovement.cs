using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    #region Variables



    [Header("Vertical Movement Varibles")]
    [SerializeField] float verticalSpeed = 2.0f;
    [SerializeField] float maxHeight = 10.0f;
    [SerializeField] float minHeight = 1.0f;
    [SerializeField] GameObject maxHeightPoint;
    [SerializeField] GameObject minHeightPoint;

    [Header("Zooom Movement Variables")]
    [SerializeField] float zoomSpeed = 2.0f;
    [SerializeField] float maxZoom = 10.0f;
    [SerializeField] float minZoom = 1.0f;
    [SerializeField] GameObject startPoint;


    float currentHeight;
    float currentZoom;

    [SerializeField] GameObject focusPoint;

    [SerializeField] CinemachineVirtualCamera vCam;
    CinemachineTransposer transposer;

    #endregion

    void Awake()
    {
        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Start()
    {
        maxHeight = maxHeightPoint.transform.position.y;
        minHeight = minHeightPoint.transform.position.y;

        transposer.m_FollowOffset.y = minHeight;
        transposer.m_FollowOffset.x = startPoint.transform.position.x;
    }

    void Update()
    {
        VerticalCameraMovement();
        ZoomCameraMovement();
    }

    void VerticalCameraMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        currentHeight += verticalInput * verticalSpeed * Time.deltaTime;
        currentHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);

        Vector3 newPosition = focusPoint.transform.localPosition;
        newPosition.y = currentHeight;
        focusPoint.transform.localPosition = newPosition;
    }

    void ZoomCameraMovement()
    {
        float ScrollInput = Input.GetAxis("Mouse ScrollWheel");

        currentZoom -= ScrollInput * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Vector3 newPosition = transposer.m_FollowOffset;
        newPosition.z = currentZoom;
        transposer.m_FollowOffset = newPosition;
    }
}