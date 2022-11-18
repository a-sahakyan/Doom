using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                GameObject hitObject = raycastHit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(raycastHit.point));
                }
            }
        }
    }

    void OnGUI()
    {
        int size = 500;
        float posX = _camera.pixelWidth / 2;
        float poxY = _camera.pixelHeight / 2;
        GUI.Label(new Rect(posX, poxY, size, size), "*");
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
