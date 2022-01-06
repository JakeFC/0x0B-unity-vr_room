using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
	public XRNode inputController;
	public Camera cam;
	public GameObject camOffset;
	public SpriteRenderer transitionScreen;
	public float speed = 2;
	private bool fadeIn = false;
	private Vector2 inputAxis;
	private Vector2 secondaryInputAxis;

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputController);
		device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
		InputDevice device2 = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

		if (device2.TryGetFeatureValue(CommonUsages.primary2DAxis, out secondaryInputAxis))
		{
			float turnSpeed = 30 * Time.deltaTime;
			camOffset.transform.Rotate(0, secondaryInputAxis.x * turnSpeed, 0);
		}

		if (fadeIn)
		{
			StartCoroutine("FadeIn");
		}

		if (transform.position.y > 0.7 || transform.position.y < 0)
		{
			Reset();
		}
    }

	private void FixedUpdate()
	{
		Quaternion headYaw = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
		Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

		gameObject.transform.Translate(direction * Time.fixedDeltaTime * speed);

		gameObject.transform.Translate(Vector3.up * -1f * Time.fixedDeltaTime);
	}

	private IEnumerator FadeIn()
	{
		Color imageColor = transitionScreen.color;
        float fadeAmount;

		fadeIn = false;

		imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, 1);

		while (imageColor.a > 0)
        {
            fadeAmount = imageColor.a - 2f * (Time.deltaTime);

            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
            transitionScreen.color = imageColor;
            yield return null;
        }
	}

	public void StartFade()
	{
		fadeIn = true;
	}
	private void Reset()
	{
		fadeIn = true;
		transform.position = new Vector3(0f, 0.2f, 0f);
	}
}
