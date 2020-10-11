using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMove : MonoBehaviour {
	Vector3 m_Offset;
	GameState m_State;
    /// <summary>
    /// Post processing
    /// </summary>
    [SerializeField]
    private Volume m_Volume;
	private float m_TimeTracker = 0.0f;
	/// <summary>
	/// 玩家
	/// </summary>
	[SerializeField]
	private GameObject m_Role;
	/// <summary>
	/// Role2
	/// </summary>
	[SerializeField]
	private GameObject m_Role2;
	/// <summary>
	/// Role3
	/// </summary>
	[SerializeField]
	private GameObject m_Role3;
	/// <summary>
	/// 敌人
	/// </summary>
	[SerializeField]
	private GameObject m_Enemy;
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
	/// 人物起始位置
	/// </summary>
	[SerializeField]
	private Transform m_OriginRoleTrans;
	/// <summary>
	/// 人物准备位置
	/// </summary>
	[SerializeField]
	private Transform m_ReadyRoleTrans;
	/// <summary>
	/// The ready trans of 2th role
	/// </summary>
	[SerializeField]
	private Transform m_ReadyRole2Trans;
	/// <summary>
	/// The ready trans of 3th role
	/// </summary>
	[SerializeField]
	private Transform m_ReadyRole3Trans;
	[SerializeField]
	private Transform m_ReadyRoleAnimTrans;
	/// <summary>
	/// 人物战斗位置
	/// </summary>
	[SerializeField]
	private Transform m_BattleRoleTrans;
	[SerializeField]
	private Transform m_BattleRoleAnimTrans;
	/// <summary>
	/// 人物向前移动的位置
	/// </summary>
	private Vector3 m_ForwardRolePos;
	/// <summary>
	/// 敌人起始位置
	/// </summary>
	[SerializeField]
	private Transform m_OriginEnemyTrans;
	/// <summary>
	/// 敌人准备位置
	/// </summary>
	[SerializeField]
	private Transform m_ReadyEnemyTrans;
	[SerializeField]
	private Transform m_ReadyEnemyAnimTrans;
	/// <summary>
	/// 敌人战斗位置
	/// </summary>
	[SerializeField]
	private Transform m_BattleEnemyTrans;
	[SerializeField]
	private Transform m_BattleEnemyAnimTrans;
	private bool m_IsSlowMotion = false;
	private bool m_IsShake = false;

	private Vector3 m_BeforeShakeCamPos;

	void Awake()
	{

	}

	// Use this for initialization
	void Start() 
	{ 
		m_ForwardRolePos = m_BattleRoleTrans.position + new Vector3(0.5f, 0, 0);
		m_Offset = transform.position - m_Role.transform.position;
		m_State = GameState.Move;
	}

	void FixedUpdate() 
	{
		if (m_State == GameState.Move)
		{
			transform.position = m_Role.transform.position + m_Offset;
		}
		else if (m_State == GameState.Ready)
		{
			ChangeBattleView();
		}
		else if (m_State == GameState.Battle)
		{
			FocusRole();
		}
		else if (m_State == GameState.Over)
		{
			UnfocusRole();
		}
		
		if (m_IsSlowMotion)
		{
			Time.timeScale = 0.4f;
		}
		else
		{
			Time.timeScale = 1f;
		}

		if (m_IsShake)
		{
			Camera.main.transform.localPosition = m_BeforeShakeCamPos + Random.insideUnitSphere * 0.04f;
		}
	}

	/// <summary>
    /// 进入战斗
    /// </summary>
    public void OnClickIntoReady()
    {
		m_State = GameState.Ready;
    }

	/// <summary>
	/// 更改战斗视角
	/// </summary>
	public void ChangeBattleView()
	{
		m_TimeTracker = 0f;
		StartCoroutine(Change());
	}

	IEnumerator Change()
	{
		m_Role2.SetActive(true);
		m_Role3.SetActive(true);
		// Character changes combat status
		m_Role.transform.Find("Anim").GetComponentInChildren<Animator>().SetTrigger("Battle");

		// The lens moves forward
		transform.position = Vector3.Lerp(transform.position, m_ReadyCamTrans.position, Time.deltaTime * 3);
		// The role moves backward
		m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, m_ReadyRoleTrans.position, Time.deltaTime * 3);
		m_Role2.transform.position = Vector3.Lerp(m_Role2.transform.position, m_ReadyRole2Trans.position, Time.deltaTime * 3);
		m_Role3.transform.position = Vector3.Lerp(m_Role3.transform.position, m_ReadyRole3Trans.position, Time.deltaTime * 3);
		// The enemy into the field
		m_Enemy.transform.position = Vector3.Lerp(m_Enemy.transform.position, m_ReadyEnemyTrans.position, Time.deltaTime * 3);
		
		// The camera and the characters revolve around the X-axis
		transform.rotation = Quaternion.Slerp(transform.rotation, m_ReadyCamTrans.rotation, Time.deltaTime * 3);
		m_Role.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_ReadyRoleAnimTrans.rotation, Time.deltaTime * 3);
		m_Role2.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_ReadyRoleAnimTrans.rotation, Time.deltaTime * 3);
		m_Role3.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_ReadyRoleAnimTrans.rotation, Time.deltaTime * 3);
		m_Enemy.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_ReadyEnemyAnimTrans.rotation, Time.deltaTime * 3);
		yield return new WaitForSeconds(1f);
	}

	/// <summary>
	/// 人物受到点击
	/// </summary>
	public void OnClickRole()
	{
		m_TimeTracker = 0f;
		m_State = GameState.Battle;
	}

	/// <summary>
	/// Focus the camera on the character
	/// </summary>
	private void FocusRole()
	{
		// The lens moves forward before attack
		transform.position = Vector3.Lerp(transform.position, m_BattleCamTrans.position, Time.deltaTime * 3);
		transform.rotation = Quaternion.Slerp(transform.rotation, m_BattleCamTrans.rotation, Time.deltaTime * 3);
		m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, m_ForwardRolePos, Time.deltaTime * 3);
		// Character and enemy angles follow
		m_Role.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_BattleRoleAnimTrans.rotation, Time.deltaTime * 3);
		m_Enemy.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_BattleEnemyAnimTrans.rotation, Time.deltaTime * 3);
		// 镜头对人物聚焦
		m_Volume.profile.components[0].parameters[2].SetValue(new FloatParameter(Mathf.Lerp(m_Volume.profile.components[0].parameters[2].GetValue<float>(), 0.4f, Time.deltaTime * (m_TimeTracker += 0.1f) * 50)));
	}

	public void OnAttackOver()
	{
		m_TimeTracker = 0f;
		m_State = GameState.Over;
	}

	/// <summary>
	/// Unfocus the camera on the character after attack
	/// </summary>
	private void UnfocusRole()
	{
		// The lens moves forward before attack
		transform.position = Vector3.Lerp(transform.position, m_ReadyCamTrans.position, Time.deltaTime * 3);
		transform.rotation = Quaternion.Slerp(transform.rotation, m_ReadyCamTrans.rotation, Time.deltaTime * 3);
		m_Role.transform.position = Vector3.Lerp(m_Role.transform.position, m_ReadyRoleTrans.position, Time.deltaTime * 3);
		// Character and enemy angles follow
		m_Role.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_ReadyRoleAnimTrans.rotation, Time.deltaTime * 3);
		m_Enemy.transform.Find("Anim").rotation = Quaternion.Slerp(transform.rotation, m_ReadyEnemyAnimTrans.rotation, Time.deltaTime * 3);
		// 镜头回退
		m_Volume.profile.components[0].parameters[2].SetValue(new FloatParameter(Mathf.Lerp(m_Volume.profile.components[0].parameters[2].GetValue<float>(), 0.35f, Time.deltaTime * (m_TimeTracker += 0.1f) * 50)));
	}

	/// <summary>
	/// 慢镜头
	/// </summary>
	public void SlowMotion()
	{
		m_IsSlowMotion = true;
	}

	/// <summary>
	/// 正常镜头
	/// </summary>
	public void NormalMotion()
	{
		m_IsSlowMotion = false;
	}

	public void ShakeCamera()
	{
		m_BeforeShakeCamPos = Camera.main.transform.localPosition;
		StartCoroutine(ShakeCor());
	}

	IEnumerator ShakeCor()
	{
		m_IsShake = true;
		yield return new WaitForSeconds(0.5f);
		m_IsShake = false;
		Camera.main.transform.localPosition = m_BeforeShakeCamPos;
	}
}

public enum GameState
{
	Move,
	Ready,
	Battle,
	Over,
}
