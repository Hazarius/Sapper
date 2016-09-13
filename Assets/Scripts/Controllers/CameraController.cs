using UnityEngine;

class CameraController : MonoBehaviour
{
	float zoom = 0;
	float zoomSpeed = 5;
	float cameraYpos = 15;

	void Update()
	{
		if (Input.GetMouseButton(2))
		{
			Camera.main.transform.position = Camera.main.transform.position + new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y"));
		}
		zoom = Input.GetAxis("Mouse ScrollWheel");
		if (zoom != 0)
		{
			cameraYpos -= zoom * zoomSpeed;
			cameraYpos = Mathf.Clamp(cameraYpos, 0, 30);
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, cameraYpos, Camera.main.transform.position.z);
		}
	}
}