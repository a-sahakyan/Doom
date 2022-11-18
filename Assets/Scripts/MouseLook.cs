using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensetivityHor = 9.0f;
    public float sensetivityVert = 9.0f;

    public float minimumVert = -45f;
    public float maximumVert = 45f;

    private float _rotationX = 0;

    void Start()
    {
        Rigidbody rd = GetComponent<Rigidbody>();
        if(rd != null)
        {
            rd.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensetivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensetivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensetivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensetivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
