using System.Numerics;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
	/// <summary>
	/// 原始视角位置
	/// </summary>
	Vector3 m_OriginPos;
	Vector3 m_OriginRot;
	Vector3 m_RoleOriginPos;
	Vector3 m_RoleOriginRot;
	Vector3 m_EnemyOriginPos;
	/// <summary>
	/// 战斗用视角位置
	/// </summary>
	Vector3 m_BattlePos;
	Vector3 m_BatlleRot;

	Vector3 m_FocusPos;
	Quaternion m_FocusRot;
	/// <summary>
	/// 标识是否移动到位
	/// </summary>
	private bool m_isOver;

	/// <summary>
	/// 摄像机起始位置
	/// </summary>
	[SerializeField]
	private Transform m_OriginCamTrans;
	/// <summary>
	/// 摄像机准备位置
	/// </summary>
	[SerializeField]
	private Transform m_ReadyCamTrans;
	/// <summary>
	/// 摄像机战斗位置
	/// </summary>
	[SerializeField]
	private Transform m_BattleCamTrans;
	/// <summary>
    /// PostProcess for adjusting lens
    /// </summary>
    [SerializeField]
    private GameObject m_Volmue;


	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start() { 
		offset = transform.position - player.transform.position;
		m_State = GameState.Move;
	}

	void FixedUpdate() {
		if (m_State == GameState.Move)
		{
			transform.position = player.transform.position + offset;
			
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
    /// 进入战斗
    /// </summary>
    public void OnClickIntoBattle()
    {
		m_OriginPos = m_OriginCamTrans.position;
		m_OriginRot = m_OriginCamTrans.localEulerAngles;
		m_RoleOriginPos = m_Role.transform.position;
		m_RoleOriginRot = m_Role.transform.Find("Anim").localEulerAngles;
		m_EnemyOriginPos = m_Enemy.transform.position;
		m_State = GameState.Battle;
		m_isOver = false;
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
		if (!m_isOver)
		{
			// Character changes combat status
			m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().SetTrigger("Battle");

			// The lens moves forward
			if (Mathf.Abs(transform.position.z - m_OriginPos.z) < 0.5f)
			{
				Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
				transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);
			}
			// The role moves backward
			if (Mathf.Abs(m_Role.transform.position.x - m_RoleOriginPos.x) < 1.5f)
			{
				Vector3 newPos = new Vector3(m_Role.transform.position.x - 1.5f, m_Role.transform.position.y, m_Role.transform.position.z);
				m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, newPos, Time.deltaTime * 2);
			}
			// The enemy into the field
			if (Mathf.Abs(m_Enemy.transform.position.y - m_EnemyOriginPos.y) < 2.9f)
			{
				Vector3 newPos = new Vector3(m_Enemy.transform.position.x, m_Enemy.transform.position.y - 2.9f, m_Enemy.transform.position.z);
				m_Enemy.transform.position = Vector3.Lerp(m_Enemy.transform.position, newPos, Time.deltaTime * 2);
			}
			
			// The camera and the characters revolve around the X-axis
			if (Mathf.Abs(transform.localEulerAngles.x - m_OriginRot.x) < 20)
			{
				transform.Rotate(Vector3.left * Time.deltaTime * 40);
			}
			if (Mathf.Abs(m_Role.transform.Find("Anim").localEulerAngles.x - m_RoleOriginRot.x) < 20)
			{
				m_Role.transform.Find("Anim").Rotate(Vector3.left * Time.deltaTime * 40);
			}
			yield return new WaitForSeconds(1f);
			m_isOver = true;
		} 
	}

	/// <summary>
	/// Changing the original perspective
	/// </summary>
	public void ChangeOriginView()
	{
		transform.position = m_OriginCamTrans.position;
		transform.localEulerAngles = m_OriginCamTrans.localEulerAngles;
		m_Role.transform.position = m_RoleOriginPos;
		m_Role.transform.Find("Anim").localEulerAngles = m_RoleOriginRot;
		m_Enemy.transform.position = m_EnemyOriginPos;
		m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().ResetTrigger("Battle");
		m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().SetTrigger("Idle");
	}

	/// <summary>
	/// Focus the camera on the character
	/// </summary>
	public void FocusRole()
	{
		// The lens moves forward before attack
		transform.position = Vector3.Lerp(transform.position, m_BattleCamTrans.position, Time.deltaTime * 3);
		transform.rotation = Quaternion.Slerp(transform.rotation, m_BattleCamTrans.rotation, Time.deltaTime * 3);
		// Character and enemy angles follow
		m_Role.transform.Find("Anim").localEulerAngles = m_BattleCamTrans.localEulerAngles;
		m_Enemy.transform.Find("Anim").localEulerAngles = m_BattleCamTrans.localEulerAngles;
	}

	/// <summary>
	/// Unfocus the camera on the character after attack
	/// </summary>
	public void UnfocusRole()
	{
		// The lens move backward
		transform.position = Vector3.Lerp(transform.position, m_OriginCamTrans.position, Time.deltaTime * 3);
		transform.rotation = Quaternion.Slerp(transform.rotation, m_OriginCamTrans.rotation, Time.deltaTime * 3);
		// Role moves backward
		m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, m_RoleOriginPos, Time.deltaTime * 3);
		// Character and enemy angles follow
		m_Role.transform.Find("Anim").localEulerAngles = m_RoleOriginRot;
		m_Enemy.transform.Find("Anim").localEulerAngles = m_EnemyOriginPos;
	}
}

public enum GameState
{
	Move,
	Battle,
}
