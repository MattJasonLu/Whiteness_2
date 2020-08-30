using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	/// <summary>
	/// 玩家
	/// </summary>
	[SerializeField]
	private GameObject m_Role;
	/// <summary>
	/// 敌人
	/// </summary>
	[SerializeField]
	private GameObject m_Enemy;
	Vector3 offset;
	GameObject player;
	GameState m_State;
	Vector3 m_OriginPos;
	Vector3 m_OriginRot;
	Vector3 m_RoleOriginPos;
	Vector3 m_RoleOriginRot;
	Vector3 m_EnemyOriginPos;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start() { 
		offset = transform.position - player.transform.position;
		m_State = GameState.Move;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_State == GameState.Move)
		{
			transform.position = player.transform.position + offset;
			if (Input.GetKeyDown (KeyCode.F1))
			{
				m_OriginPos = transform.position;
				m_OriginRot = transform.localEulerAngles;
				m_RoleOriginPos = m_Role.transform.position;
				m_RoleOriginRot = m_Role.transform.Find("Anim").localEulerAngles;
				m_EnemyOriginPos = m_Enemy.transform.position;
				m_State = GameState.Battle;
			}
		} 
		else if (m_State == GameState.Battle)
		{
			ChangeBattleView();
			if (Input.GetKeyDown (KeyCode.F2))
			{
				ChangeOriginView();
				m_State = GameState.Move;
			}
		}
	}

	/// <summary>
	/// 更改战斗视角
	/// </summary>
	public void ChangeBattleView()
	{
		StartCoroutine(Change());
	}

	IEnumerator Change()
	{
		// 人物变更战斗状态
		m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().SetTrigger("Battle");

		// 镜头向前移动，人物后退
		if (Mathf.Abs(transform.position.z - m_OriginPos.z) < 0.7f)
		{
			Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.7f);
			transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);
		}
		if (Mathf.Abs(m_Role.transform.position.x - m_RoleOriginPos.x) < 1.5f)
		{
			Vector3 newPos = new Vector3(m_Role.transform.position.x - 1.5f, m_Role.transform.position.y, m_Role.transform.position.z);
			m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, newPos, Time.deltaTime * 2);
		}
		if (Mathf.Abs(m_Enemy.transform.position.y - m_EnemyOriginPos.y) < 3)
		{
			Vector3 newPos = new Vector3(m_Enemy.transform.position.x, m_Enemy.transform.position.y - 3, m_Enemy.transform.position.z);
			m_Enemy.transform.position = Vector3.Lerp(m_Enemy.transform.position, newPos, Time.deltaTime * 2);
		}
		//yield return new WaitForSeconds(0.3f);
		// 镜头和人物绕着X轴旋转
		if (Mathf.Abs(transform.localEulerAngles.x - m_OriginRot.x) < 20)
		{
			transform.Rotate(Vector3.left * Time.deltaTime * 40);
		}
		if (Mathf.Abs(m_Role.transform.Find("Anim").localEulerAngles.x - m_RoleOriginRot.x) < 20)
		{
			m_Role.transform.Find("Anim").Rotate(Vector3.left * Time.deltaTime * 40);
		}
		yield return new WaitForSeconds(0.3f);
	}

	/// <summary>
	/// 更改原始视角
	/// </summary>
	public void ChangeOriginView()
	{
		transform.position = m_OriginPos;
		transform.localEulerAngles = m_OriginRot;
		m_Role.transform.position = m_RoleOriginPos;
		m_Role.transform.Find("Anim").localEulerAngles = m_RoleOriginRot;
		m_Enemy.transform.position = m_EnemyOriginPos;
		m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().ResetTrigger("Battle");
		m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().SetTrigger("Idle");
	}
	
}

public enum GameState
{
	Move,
	Battle,
}
