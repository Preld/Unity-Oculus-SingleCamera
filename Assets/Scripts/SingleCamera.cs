/*
 * To use SingleCamera class, Dont make display in OVRManager. 
 * So, occur two errors.
 */

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Ovr;

public class SingleCamera : MonoBehaviour {

	private Camera mainCamera;

	void Start () {
		mainCamera = this.camera;
		OVRManager.capiHmd.RecenterPose();
	}

	void Update () {
		OVRPose eye = GetEyePose(OVREye.Left);
		//OVRPose rightEye = OVRManager.display.GetEyePose(OVREye.Right);

		mainCamera.transform.localRotation = eye.orientation;
		//mainCamera.transform.localPosition = 0.5f * (leftEye.position + rightEye.position);
	}

	//Dont make display in OVRManager. So, this method is needed.
	public OVRPose GetEyePose(OVREye eye)
	{
		return OVR_GetRenderPose(Time.frameCount, (int)eye).ToPose();
	}
	
	private const string LibOVR = "OculusPlugin";
	[DllImport(LibOVR, CallingConvention = CallingConvention.Cdecl)]
	private static extern Posef OVR_GetRenderPose(int frameIndex, int eyeId);
}
