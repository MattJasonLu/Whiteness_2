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
}
