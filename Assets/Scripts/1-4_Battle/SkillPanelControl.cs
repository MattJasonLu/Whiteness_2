using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelControl : MonoBehaviour {

	public GameObject canvas;
	public DBCalculator dBCalculator;
	public GameObject buttonPrefab;
	private GameObject basicPanel;
	private GameObject content;

	void Awake()
	{
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		content = canvas.transform.Find("SkillPanel/ItemTab/Viewport/Content").gameObject;
	}

	void Start()
	{
		SetTactics();
	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
	}

	/// <summary>
	/// 设置技能
	/// </summary>
	public void SetTactics()
	{
		string unitId = BattleSystem._instance.currentActUnitStatus.unitId;
		int currentCP = BattleSystem._instance.currentActUnitStatus.CP;
		List<SkillDAO> magics = dBCalculator.GetTacticsByRoleId(unitId);
		magics.ForEach(p => {
			GameObject skillItem = Instantiate(buttonPrefab, content.transform, false);
			skillItem.transform.Find("Text").GetComponent<Text>().text = p.name;
			skillItem.GetComponent<Button>().onClick.AddListener(delegate() 
			{
				// 发动对应技能
				LaunchTactic(p);
			});
			// 如果cp值不够则该战技不可用
			if (p.consume > currentCP)
			{
				skillItem.GetComponent<Button>().interactable = false;
			}
		});
		GameObject backBtn = Instantiate(buttonPrefab, content.transform, false);
		backBtn.transform.Find("Text").GetComponent<Text>().text = "返回";
		backBtn.GetComponent<Button>().onClick.AddListener(delegate () {
			this.Back();
		});
	}

	/// <summary>
	/// 发动战技
	/// </summary>
	/// <param name="skill"></param>
	void LaunchTactic(SkillDAO skill)
	{
		BattleSystem._instance.OnAttack(skill);
	}
}
