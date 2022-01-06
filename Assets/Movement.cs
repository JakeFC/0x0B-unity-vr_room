using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
	public XRNode inputController;
	public Camera cam;
	public float speed = 2;
	private Vector2 inputAxis;

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputController);
		device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

	private void FixedUpdate()
	{
		Quaternion headYaw = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
		Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

		gameObject.transform.Translate(direction * Time.fixedDeltaTime * speed);

		gameObject.transform.Translate(Vector3.up * -3f * Time.fixedDeltaTime);
	}
}
