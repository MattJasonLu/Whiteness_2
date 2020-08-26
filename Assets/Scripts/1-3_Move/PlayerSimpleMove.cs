using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMove : MonoBehaviour {

	public float speed = 5f;
	// 映射的3D物体
	GameObject mappingObj;
	Animator animator;

	// Use this for initialization
	void Start () {
		mappingObj = transform.parent.gameObject;
		animator = GetComponent<Animator>();
		SetPlayerPos();
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		Vector3 mappingObjTargetPos = mappingObj.transform.position + new Vector3(-h, 0, 0) + new Vector3(0, 0, -v);
		mappingObj.transform.position = Vector3.Lerp(mappingObj.transform.position, mappingObjTargetPos, speed * Time.deltaTime);
		animator.SetInteger("Vertical", (int) v);
		animator.SetInteger("Horizontal", (int) h);
	}

	void SetPlayerPos()
	{
		// 设置玩家的位置
		PlayerPrefs.DeleteAll();
		string playerPos = PlayerPrefs.GetString("PlayerPos");
		if (playerPos != null && playerPos != "")
		{
			string[] pos = playerPos.Split(',');
			mappingObj.transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
		}
		// 设置摄像机的位置
		string cameraPos = PlayerPrefs.GetString("CameraPos");
		if (cameraPos != null && cameraPos != "")
		{
			string[] pos = cameraPos.Split(',');
			Camera.main.transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
		}
	}
}
