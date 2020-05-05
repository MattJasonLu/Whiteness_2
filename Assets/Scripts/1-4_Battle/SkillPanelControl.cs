using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelControl : MonoBehaviour {

	public GameObject basicPanel;

	public void Skill_1()
	{

	}

	public void Skill_2()
	{

	}
	public void Skill_3()
	{

	}

	public void Skill_4()
	{

	}

	public void Skill_5()
	{

	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
	}
}
