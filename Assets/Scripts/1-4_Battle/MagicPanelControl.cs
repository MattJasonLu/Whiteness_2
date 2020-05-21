using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicPanelControl : MonoBehaviour {

	public GameObject canvas;
	public DBCalculator dBCalculator;
	public GameObject buttonPrefab;
	private GameObject basicPanel;
	private GameObject content;

	void Awake()
	{
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		content = canvas.transform.Find("MagicPanel/ItemTab/Viewport/Content").gameObject;
	}

	void Start()
	{
		SetMagic();
	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
	}

	/// <summary>
	/// 设置技能
	/// </summary>
	public void SetMagic()
	{
		string unitId = BattleSystem._instance.currentActUnitStatus.unitId;
		int currentEP = BattleSystem._instance.currentActUnitStatus.EP;
		List<SkillDAO> magics = dBCalculator.GetMagicsByRoleId(unitId);
		magics.ForEach(p => {
			GameObject skillBtn = Instantiate(buttonPrefab, content.transform, false);
			skillBtn.transform.Find("Text").GetComponent<Text>().text = p.name;
			skillBtn.GetComponent<Button>().onClick.AddListener(delegate ()
			{
				// 发动对应技能
				LaunchMagic(p);
			});
			// 如果ep值不够则该战技不可用
			if (p.consume > currentEP)
			{
				skillBtn.GetComponent<Button>().interactable = false;
			}
		});
		GameObject backBtn = Instantiate(buttonPrefab, content.transform, false);
		backBtn.transform.Find("Text").GetComponent<Text>().text = "返回";
		backBtn.GetComponent<Button>().onClick.AddListener(delegate () {
			this.Back();
		});
	}

	// 发动魔法攻击
	void LaunchMagic(SkillDAO skill)
	{
		//StartCoroutine("Magic_" + magicId);
		BattleSystem._instance.OnAttack(skill);
	}

	IEnumerator Magic_SM001()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("发动烈火斩击");
	}

	IEnumerator Magic_SM002()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("火羽");
	}
}
