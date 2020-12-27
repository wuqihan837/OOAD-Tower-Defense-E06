using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 30.0f;
    public float panBorderThickness = 10.0f;

    public float scrollSpeed = 5.0f;

    public float minY = 10.0f;
    public float maxY = 80.0f;

    public float minX = -85.0f;
    public float maxX = 25.0f;

    public float minZ = -15.0f;
    public float maxZ = 0.0f;

    public GameObject map;

    private static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    public static CameraController GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        doMovement = false;
    }

    void Update()
    {
        if (!doMovement)
            return;
    
        if(Time.timeScale < 0.4)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.unscaledDeltaTime, Space.World);
            //transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.unscaledDeltaTime, Space.World);
            //transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.unscaledDeltaTime, Space.World);
            //transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.unscaledDeltaTime, Space.World);
            //transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        if (scroll > 0)
        {
            map.GetComponent<bl_MiniMap>().ChangeHeight(false);
        }
        if(scroll < 0)
        {
            map.GetComponent<bl_MiniMap>().ChangeHeight(true);
        }

        pos.y -= scroll * 500 * scrollSpeed * Time.unscaledDeltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    public void SetCanMovement(bool isActive)
    {
        doMovement = isActive;
    }
}
