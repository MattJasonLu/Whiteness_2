﻿using System.Collections;
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
		m_OriginPos = transform.position;
		m_OriginRot = transform.localEulerAngles;
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
			// 人物变更战斗状态
			m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().SetTrigger("Battle");

			// 镜头向前移动
			if (Mathf.Abs(transform.position.z - m_OriginPos.z) < 0.5f)
			{
				Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
				transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);
			}
			// 人物后退
			if (Mathf.Abs(m_Role.transform.position.x - m_RoleOriginPos.x) < 1.5f)
			{
				Vector3 newPos = new Vector3(m_Role.transform.position.x - 1.5f, m_Role.transform.position.y, m_Role.transform.position.z);
				m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, newPos, Time.deltaTime * 2);
			}
			// 敌人入场
			if (Mathf.Abs(m_Enemy.transform.position.y - m_EnemyOriginPos.y) < 3)
			{
				Vector3 newPos = new Vector3(m_Enemy.transform.position.x, m_Enemy.transform.position.y - 3, m_Enemy.transform.position.z);
				m_Enemy.transform.position = Vector3.Lerp(m_Enemy.transform.position, newPos, Time.deltaTime * 2);
			}
			
			// 镜头和人物绕着X轴旋转
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
			// 记录战斗时摄像机的位置和旋转角度
			m_BattlePos = transform.position;
			m_BatlleRot = transform.localEulerAngles;
			m_FocusPos = new Vector3(m_BattlePos.x + 0.2f, m_BattlePos.y, m_BattlePos.z - 0.3f);
			m_FocusRot = Quaternion.Euler(m_BatlleRot.x + 5f, m_BatlleRot.y, m_BatlleRot.z);
		}
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

	/// <summary>
	/// 聚焦镜头到人物
	/// </summary>
	public void FocusRole()
	{
		// 镜头向前移动
		transform.position = Vector3.Lerp(transform.position, m_FocusPos, Time.deltaTime * 1);
		// 镜头旋转
		transform.rotation = Quaternion.Lerp(transform.rotation, m_FocusRot, Time.deltaTime * 5);
		Debug.Log(Quaternion.Angle(m_FocusRot, transform.rotation));
		if (Quaternion.Angle(m_FocusRot, transform.rotation) < 1)
        {
            transform.rotation = m_FocusRot;                  
        }
	}
	
}

public enum GameState
{
	Move,
	Battle,
}
