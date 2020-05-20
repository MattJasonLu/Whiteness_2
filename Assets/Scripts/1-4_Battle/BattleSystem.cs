using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour {

	public static BattleSystem _instance;

	private BattleSystem()
	{
		_instance = this;
	}

	// 敌人区域
	//public int enemyZone = 0;
	public int unitMoveSpeed = 5;
	public int minEnemyLevel = 1;
	public int maxEnemyLevel = 3;
	// 位置
	public Transform currentActPlayerUnitPos;
	public Transform playerUnitPos_1;
	public Transform playerUnitPos_2;
	public Transform playerUnitPos_3;
	public Transform currentActEnemyUnitPos;
	public Transform enemyUnitPos_1;
	public Transform enemyUnitPos_2;
	public Transform enemyUnitPos_3;
	// 角色信息面板
	public GameObject rolePanel_1;
	public GameObject rolePanel_2;
	public GameObject rolePanel_3;
	// 选项面板
	private GameObject basicPanel;
	private GameObject magicPanel;
	private GameObject tacticsPanel;
	private GameObject comboPanel;
	private GameObject itemPanel;
	public GameObject attackAdditionPanel;
	// 伤害信息
	public GameObject hint;
	public GameObject enemyBtn;
	// UI部分
	public Canvas canvas;
	public Camera UICamera;
	// 敌人种子
	public GameObject[] enemyPrefabs;
	// 提示文字
	public GameObject notice;
	// 角色计算器
	public DBCalculator dbCalculator;
	// 结算面板
	private GameObject resultPanel;
	private GameObject roleNameText_1;
	private GameObject roleNameText_2;
	private GameObject roleNameText_3;
	private GameObject roleExpText_1;
	private GameObject roleExpText_2;
	private GameObject roleExpText_3;
	private GameObject lv_1;
	private GameObject lv_2;
	private GameObject lv_3;
	// 所有参战单元
	private List<GameObject> battleUnits;
	// 所有玩家单元
	private List<GameObject> playerUnits;
	// 所有敌人单元
	private List<GameObject> enemyUnits;
	// 剩余玩家单元
	private List<GameObject> remainPlayerUnits;
	// 剩余敌人单元
	private List<GameObject> remainEnemyUnits;
	// 当前行动单元
	private GameObject currentActUnit;
	[HideInInspector]
	public RoleUnit currentActUnitStatus;
	// 当前行动单元目标
	private GameObject currentActTargetUnit;
	private RoleUnit currentActTargetUnitStatus;
	// 是否等待玩家选择技能
	private bool isWaitForPlayerToChooseSkill;
	// 是否等待玩家选择目标
	private bool isWaitForPlayerToChooseTarget;
	// 是否点击了敌人
	private bool isClickedEnemy;
	// 是否出战
	private bool isUnitRunningToBattle;
	// 是否单元跑向目标
	private bool isUnitRunningToTarget;
	// 是否单元跑回原地
	private bool isUnitRunningBack;
	// 鼠标射线
	private Ray targetChooseRay;
	// 射线碰撞物体
	private RaycastHit targetHit;
	private Vector3 currentActUnitInitialPosition;
	private Vector3 currentActUnitTargetPosition;
	private Vector3 currentactUnitStopPosition;
	// 可获取的经验值
	private int totalExp;
	// 平均经验值
	private int averageExp;

	void Awake()
	{
		// 面板1信息
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		magicPanel = canvas.transform.Find("MagicPanel").gameObject;
		tacticsPanel = canvas.transform.Find("SkillPanel").gameObject;
		comboPanel = canvas.transform.Find("ComboPanel").gameObject;
		itemPanel = canvas.transform.Find("ItemPanel").gameObject;
		resultPanel = canvas.transform.Find("ResultPanel").gameObject;
		roleNameText_1 = canvas.transform.Find("ResultPanel/Panel/Role_1").gameObject;
		roleNameText_2 = canvas.transform.Find("ResultPanel/Panel/Role_2").gameObject;
		roleNameText_3 = canvas.transform.Find("ResultPanel/Panel/Role_3").gameObject;
		roleExpText_1 = canvas.transform.Find("ResultPanel/Panel/Exp_1").gameObject;
		roleExpText_2 = canvas.transform.Find("ResultPanel/Panel/Exp_2").gameObject;
		roleExpText_3 = canvas.transform.Find("ResultPanel/Panel/Exp_3").gameObject;
		lv_1 = canvas.transform.Find("ResultPanel/Panel/Lv_1").gameObject;
		lv_2 = canvas.transform.Find("ResultPanel/Panel/Lv_2").gameObject;
		lv_3 = canvas.transform.Find("ResultPanel/Panel/Lv_3").gameObject;
		basicPanel.SetActive(false);
		notice.SetActive(false);
		resultPanel.SetActive(false);
	}

	void Start()
	{
		Init();
		Invoke("InitAfter", 1f);
	}

	void Update()
	{
		if (isWaitForPlayerToChooseSkill)
		{
			if (!basicPanel.activeSelf && !attackAdditionPanel.activeSelf && !magicPanel.activeSelf &&
				!tacticsPanel.activeSelf && !comboPanel.activeSelf && !itemPanel.activeSelf)
			{
				basicPanel.SetActive(true);
			}
		}

		if (isWaitForPlayerToChooseTarget)
		{
			if (basicPanel.activeSelf || attackAdditionPanel.activeSelf || magicPanel.activeSelf ||
				tacticsPanel.activeSelf || comboPanel.activeSelf || itemPanel.activeSelf)
			{
				basicPanel.SetActive(false);
				attackAdditionPanel.SetActive(false);
				magicPanel.SetActive(false);
				tacticsPanel.SetActive(false);
			}
			// 修改为不可见按钮
			/*
			targetChooseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(targetChooseRay, out targetHit))
			{
				if (Input.GetMouseButtonDown(0) && targetHit.collider.gameObject.tag == "Enemy")
				{
					currentActTargetUnit = targetHit.collider.gameObject;
					isWaitForPlayerToChooseTarget = false;
					currentActTargetUnitStatus = currentActTargetUnit.GetComponent<RoleUnit>();
					//如果是远程单位直接在这里LaunchAttack，就不需要RunToTarget
					if (currentActUnit.GetComponent<RoleUnit>().RNG == 1)
					{
						LaunchAttack();
					}
					else
					{
						RunToTarget();
					}
				}
			}
			*/
			// 等待点击
			if (isClickedEnemy)
			{
				isClickedEnemy = false;
				isWaitForPlayerToChooseTarget = false;
				currentActTargetUnitStatus = currentActTargetUnit.GetComponent<RoleUnit>();
				//如果是远程单位直接在这里LaunchAttack，就不需要RunToTarget
				if (currentActUnit.GetComponent<RoleUnit>().RNG == 1)
				{
					LaunchAttack();
				}
				else
				{
					RunToTarget();
				}
			}

		}

		// 跑向出战位置
		if (isUnitRunningToBattle)
		{
			float disToBattle = 999;
			//到目标的距离，需要实时计算
			if (currentActUnit.tag == "Player") { 
				disToBattle = Vector3.Distance(currentActPlayerUnitPos.position, currentActUnit.transform.position);           
			}
			else if (currentActUnit.tag == "Enemy")
			{
				disToBattle = Vector3.Distance(currentActEnemyUnitPos.position, currentActUnit.transform.position);
			}

			//避免靠近目标时抖动
			if (disToBattle > 0.1)
			{
				if (currentActUnit.tag == "Player")
				{
					currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", -1);
				}
				else if (currentActUnit.tag == "Enemy")
				{
					currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", 1);
				}
				// Time.deltaTime保证速度单位是每秒
				if (currentActUnit.tag == "Player")
				{
					currentActUnit.transform.localPosition = Vector3.MoveTowards(currentActUnit.transform.localPosition, currentActPlayerUnitPos.position, unitMoveSpeed * Time.deltaTime);
				}
				else if (currentActUnit.tag == "Enemy")
				{
					currentActUnit.transform.localPosition = Vector3.MoveTowards(currentActUnit.transform.localPosition, currentActEnemyUnitPos.position, unitMoveSpeed * Time.deltaTime);
				}
			}
			else
			{
				//进入战斗状态
				currentActUnit.GetComponentInChildren<Animator>().SetTrigger("Battle");
				//关闭移动状态
				isUnitRunningToBattle = false;
			}
		}

		if (isUnitRunningToTarget)
		{
			currentActUnit.GetComponentInChildren<Animator>().SetTrigger("Idle");
			float distanceToTarget = Vector3.Distance(currentActUnitTargetPosition, currentActUnit.transform.position);           //到目标的距离，需要实时计算
																																	//避免靠近目标时抖动
			if (distanceToTarget > 1)
			{
				if (currentActUnit.tag == "Player")
				{
					currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", -1);
				}
				else if (currentActUnit.tag == "Enemy")
				{
					currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", 1);
				}
				//Time.deltaTime保证速度单位是每秒
				currentActUnit.transform.localPosition = Vector3.MoveTowards(currentActUnit.transform.localPosition, currentActUnitTargetPosition, unitMoveSpeed * Time.deltaTime);
			}
			else
			{
				//停止移动
				currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", 0);
				//关闭移动状态
				isUnitRunningToTarget = false;
				//记录停下的位置
				currentactUnitStopPosition = currentActUnit.transform.position;
				//开始攻击
				LaunchAttack();
			}
		}

		if (isUnitRunningBack)
		{
			//离初始位置的距离
			float distanceToInitial = Vector3.Distance(currentActUnit.transform.position, currentActUnitInitialPosition);           
			if (distanceToInitial > 0.3)
			{
				if (currentActUnit.tag == "Player")
				{
					currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", 1);
				}
				else if (currentActUnit.tag == "Enemy")
				{
					currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", -1);
				}
				currentActUnit.transform.localPosition = Vector3.MoveTowards(currentActUnit.transform.localPosition, currentActUnitInitialPosition, unitMoveSpeed * Time.deltaTime);
			}
			else
			{
				//停止移动
				currentActUnit.GetComponentInChildren<Animator>().SetInteger("Horizontal", 0);
				currentActUnit.GetComponentInChildren<Animator>().SetTrigger("Idle");
				//关闭移动状态
				isUnitRunningBack = false;
				//修正到初始位置和朝向
				currentActUnit.transform.position = currentActUnitInitialPosition;
				//攻击单位回原位后行动结束，到下一个单位
				ToBattle();
			}
		}
	}

	void Init()
	{
		GeneratePlayerList();
		GenerateEnemyList();
		// 显示提示文字
		StartCoroutine(ShowNotice("战斗开始"));
		// 单元分配
		battleUnits = new List<GameObject>();
		playerUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
		enemyUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		playerUnits.ForEach(p => battleUnits.Add(p));
		enemyUnits.ForEach(p => battleUnits.Add(p));
	}

	void InitAfter()
	{
		SortBySpeed();
		ToBattle();
	}

	/// <summary>
	/// 显示提示文字
	/// </summary>
	/// <param name="text"></param>
	/// <param name="time"></param>
	/// <returns></returns>
	IEnumerator ShowNotice(string text, float time = 2f)
	{
		notice.SetActive(true);
		notice.GetComponent<Text>().text = text;
		yield return new WaitForSeconds(time);
		notice.SetActive(false);
	}

	// 生成玩家列表
	void GeneratePlayerList()
	{
		List<RoleUnitDAO> roleUnits = GameManager._instance.roleUnits;
		roleUnits.ForEach(p => Debug.Log("角色：" + p.unitName));
		for (int i = 0; i < roleUnits.Count; i++)
		{
			// 加载每一个对象
			GameObject prefab = Resources.Load("Role/Player/" + roleUnits[i].unitId) as GameObject;
			Vector3 pos = playerUnitPos_1.position;
			GameObject panel = rolePanel_1;
			if (i == 0)
			{
				pos = playerUnitPos_2.position;
				panel = rolePanel_2;
			}
			else if (i == 1)
			{
				pos = playerUnitPos_1.position;
				panel = rolePanel_1;
			}
			else if (i == 2)
			{
				pos = playerUnitPos_3.position;
				panel = rolePanel_3;
			}
			GameObject role = Instantiate(prefab, pos, Quaternion.identity);
			role.tag = "Player";
			RoleUnitDAO roleData = dbCalculator.GetRoleUnitByIdAndLevel(role.GetComponent<RoleUnit>().unitId, roleUnits[i].level);
			role.GetComponent<RoleUnit>().SetInitData(roleData);
			panel.SetActive(true);
			role.GetComponent<RoleUnit>().SetPanel(panel);
		}
	}

	// 生成敌人列表
	void GenerateEnemyList()
	{
		//string prefix = "Assets/Resources/";
		//string path = "Role/Enemy/" + enemyZone + "/";

		// 获取所有prefab
		//List<string> prefabNameList = GetPrefabNameListFromPath(prefix + path);
		totalExp = 0;
		int enemyCount = Random.Range(1, 4);
		for (int i = 0; i < enemyCount; i++)
		{
			// 加载每一个对象
			//GameObject prefab = Resources.Load(path + prefabNameList[Random.Range(0, prefabNameList.Count - 1)]) as GameObject;
			GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
			Vector3 pos = enemyUnitPos_1.position;
			if (i == 0)
			{
				pos = enemyUnitPos_2.position;
			}
			else if (i == 1)
			{
				pos = enemyUnitPos_1.position;
			}
			else if (i == 2)
			{
				pos = enemyUnitPos_3.position;
			}
			GameObject role = Instantiate(prefab, pos, Quaternion.identity);
			role.tag = "Enemy";
			int level = Random.Range(minEnemyLevel, maxEnemyLevel + 1);
			RoleUnitDAO roleData = dbCalculator.GetRoleUnitByIdAndLevel(role.GetComponent<RoleUnit>().unitId, level);
			role.GetComponent<RoleUnit>().SetInitData(roleData);
			// TEST 需要删除
			//role.GetComponent<RoleUnit>().HP = 1;
			totalExp += role.GetComponent<RoleUnit>().EXP;
			// 设置不可见按钮
			SetEnemyBtn(role, i);
		}
	}

	// 获取路径下的所有prefab的名称
	List<string> GetPrefabNameListFromPath(string path)
	{
		List<string> prefabList = new List<string>();
		string[] paths = Directory.GetFiles(path, "*.prefab");
		foreach (string prefabPath in paths)
		{
			string[] names = prefabPath.Split(new string[] { "/", "." }, StringSplitOptions.RemoveEmptyEntries);
			string name = names[names.Length - 2];
			prefabList.Add(name);
		}
		return prefabList;
	}

	// 速度降序排列
	void SortBySpeed()
	{
		battleUnits.Sort((x, y) => y.GetComponent<RoleUnit>().SPD.CompareTo(x.GetComponent<RoleUnit>().SPD));
	}

	void ToBattle()
	{
		remainPlayerUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
		remainEnemyUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		if (averageExp == 0) averageExp = totalExp / remainPlayerUnits.Count;
		if (remainPlayerUnits.Count == 0)
		{
			// 失败
			StartCoroutine(ShowNotice("战斗失败"));
			SetResultPanel();
		}
		else if (remainEnemyUnits.Count == 0)
		{
			// 成功
			StartCoroutine(ShowNotice("战斗胜利"));
			SetResultPanel();
		}
		else
		{
			// 战斗队列第一位出栈
			currentActUnit = battleUnits[0];
			currentActUnitStatus = currentActUnit.GetComponent<RoleUnit>();
			battleUnits.RemoveAt(0);
			battleUnits.Add(currentActUnit);
			
			if (!currentActUnitStatus.dead)
			{
				RunToBattlePos();
				StartCoroutine(WaitForFindTarget());
			}
			else
			{
				ToBattle();
			}
		}
	}

	/// <summary>
	/// 设置结算界面数据
	/// </summary>
	private void SetResultPanel()
	{
		roleNameText_1.SetActive(false);
		roleExpText_1.SetActive(false);
		roleNameText_2.SetActive(false);
		roleExpText_2.SetActive(false);
		roleNameText_3.SetActive(false);
		roleExpText_3.SetActive(false);
		lv_1.SetActive(false);
		lv_2.SetActive(false);
		lv_3.SetActive(false);
		List<RoleUnitDAO> roleList = GameManager._instance.roleUnits;
		for (int i = 0; i < roleList.Count; i++)
		{
			if (i == 0)
			{
				roleNameText_1.GetComponent<Text>().text = roleList[i].unitName + " :";
				roleExpText_1.GetComponent<Text>().text = averageExp.ToString();
				roleNameText_1.SetActive(true);
				roleExpText_1.SetActive(true);
			}
			else if (i == 1)
			{
				roleNameText_2.GetComponent<Text>().text = roleList[i].unitName + " :";
				roleExpText_2.GetComponent<Text>().text = averageExp.ToString();
				roleNameText_2.SetActive(true);
				roleExpText_2.SetActive(true);
			}
			else if (i == 2)
			{
				roleNameText_3.GetComponent<Text>().text = roleList[i].unitName + " :";
				roleExpText_3.GetComponent<Text>().text = averageExp.ToString();
				roleNameText_3.SetActive(true);
				roleExpText_3.SetActive(true);
			}
			// 获取经验值
			int oldLevel = GameManager._instance.roleUnits[i].level;
			GameManager._instance.roleUnits[i].EXP += averageExp;
			GameManager._instance.roleUnits[i] = dbCalculator.GetRoleUnitByIdAndExp(GameManager._instance.roleUnits[i].unitId, GameManager._instance.roleUnits[i].EXP);
			int newLevel = GameManager._instance.roleUnits[i].level;
			if (newLevel > oldLevel)
			{
				Debug.Log(GameManager._instance.roleUnits[i].unitName + "升级了");
				if (i == 0)
				{
					lv_1.SetActive(true);
				}
				else if (i == 1)
				{
					lv_2.SetActive(true);
				}
				else if (i == 2)
				{
					lv_3.SetActive(true);
				}
			}
		}
		resultPanel.SetActive(true);
		Debug.Log("玩家经验值：" + GameManager._instance.roleUnits[0].unitId + "," + GameManager._instance.roleUnits[0].EXP);
		GameManager._instance.SaveGame();
	}

	public void OnResultPanelClose()
	{
		LevelLoader._instance.LoadPreviousLevel();
	}

	void RunToBattlePos()
	{
		// 保存移动前的位置和朝向，因为跑回来还要用
		currentActUnitInitialPosition = currentActUnit.transform.position;
		// 跑向战斗位置
		isUnitRunningToBattle = true;
	}

	/// <summary>
	/// 等待时间
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	IEnumerator WaitForFindTarget(float time = 1f)
	{
		yield return new WaitForSeconds(time);
		FindTarget();
	}

	void FindTarget()
	{
		if (currentActUnit.tag == "Enemy")
		{
			//如果行动单位是怪物则从存活玩家对象中随机一个目标
			int targetIndex = Random.Range(0, remainPlayerUnits.Count);
			currentActTargetUnit = remainPlayerUnits[targetIndex];

			//如果是远程单位直接在这里LaunchAttack，就不需要RunToTarget
			if (currentActUnit.GetComponent<RoleUnit>().RNG == 1)
			{
				LaunchAttack();
			}
			else
			{
				RunToTarget();
			}
		}
		else if (currentActUnit.tag == "Player")
		{
			isWaitForPlayerToChooseSkill = true;
		}
	}

	void RunToTarget()
	{
		
		// 目标的位置
		currentActUnitTargetPosition = currentActTargetUnit.transform.position;
		// 开启移动状态，移动的控制放到Update里，因为要每一帧判断离目标的距离
		isUnitRunningToTarget = true;
        
	}

	/// <summary>
	/// 发动攻击
	/// </summary>
	void LaunchAttack()
	{
		StartCoroutine(WaitForAttack());
	}

	/// <summary>
	/// 播放攻击动画，延时
	/// </summary>
	/// <returns></returns>
	IEnumerator WaitForAttack()
	{
		// 显示攻击名字，TODO: 替换成播放动画
		ShowHint(currentActUnit, currentActUnitStatus.GetAttackName());
		string animId = "";
		SkillDAO skillDAO = currentActUnitStatus.GetSkill();
		AttackAddition attackAddition = currentActUnitStatus.GetAttackAddition();
		if (skillDAO != null)
		{
			animId = skillDAO.id;
		}
		else if (attackAddition != null)
		{
			animId = "AA" + (int)attackAddition.additionType;
		}
		// 播放属性攻击或者技能的动画
		currentActUnit.GetComponentInChildren<Animator>().SetTrigger(animId);
		yield return new WaitForSeconds(1);		// 获取行动角色的攻击结果
		List<string> result = currentActTargetUnit.GetComponent<RoleUnit>().GetRealDamage(currentActUnitStatus);
		ShowHint(currentActTargetUnit, result[1]);
		currentActUnit.GetComponentInChildren<Animator>().SetTrigger("Idle");
		yield return new WaitForSeconds(1f);
		if (result.Count > 2)
		{
			if (result[2] == "Player")
			{
				ShowHint(currentActUnit, result[3]);
			}
			else if (result[2] == "Enemy")
			{
				ShowHint(currentActTargetUnit, result[3]);
			}
			yield return new WaitForSeconds(0.5f);
		}
		// 使角色返回
		isUnitRunningBack = true;

	}

	// 普攻事件触发
	public void OnAttack(AttackAddition attackAddition)
	{
		// 设置当前回合的附加属性
		currentActUnitStatus.SetAttackAddition(attackAddition);
		isWaitForPlayerToChooseSkill = false;
		isWaitForPlayerToChooseTarget = true;
	}

	// 技能事件触发
	public void OnAttack(SkillDAO skill)
	{
		// 设置当前回合的附加属性
		currentActUnitStatus.SetSkill(skill);
		isWaitForPlayerToChooseSkill = false;
		isWaitForPlayerToChooseTarget = true;
	}

	/// <summary>
	/// 显示伤害信息
	/// </summary>
	/// <param name="str"></param>
	void ShowHint(GameObject target, string str)
	{
		// 世界坐标转屏幕坐标
		Vector3 unitPos = Camera.main.WorldToScreenPoint(target.transform.position);
		//Vector3 guiPos;
		//RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), unitPos, UICamera, out guiPos);
		GameObject hintGO = Instantiate(hint);
		hintGO.GetComponent<Text>().text = str;
		hintGO.transform.SetParent(canvas.transform, false);
		hintGO.transform.position = unitPos + new Vector3(0, 50f, 0);
		// 销毁
		Destroy(hintGO, 0.5f);
	}

	/// <summary>
	/// 设置敌人的不可见按钮，用于玩家选择
	/// </summary>
	void SetEnemyBtn(GameObject enemy, int index)
	{
		// 世界坐标转屏幕坐标
		Vector3 unitPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
		GameObject enemyBtnGO = Instantiate(enemyBtn);
		// 设置敌人的索引值
		enemyBtnGO.GetComponentInChildren<Text>().text = index.ToString();
		enemyBtnGO.transform.SetParent(canvas.transform, false);
		enemyBtnGO.transform.position = unitPos;
		enemyBtnGO.GetComponent<Button>().onClick.AddListener(delegate ()
		{
			Debug.Log("选择敌人：" + index);
			if (isWaitForPlayerToChooseTarget)
			{
				currentActTargetUnit = enemy;
				isClickedEnemy = true;
			}
		});
	}

	/// <summary>
	/// 延迟时间
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	IEnumerator WaitForTime(float time = 1f)
	{
		yield return new WaitForSeconds(time);
		Debug.Log(time);
	}
}
