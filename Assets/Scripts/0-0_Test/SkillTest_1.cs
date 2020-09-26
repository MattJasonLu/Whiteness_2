using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTest_1 : MonoBehaviour
{
	[SerializeField]
    private Animator m_Animator;

	[SerializeField]
	private GameObject m_Skill1;
    public void Update ()
    {
		KeyControl();
	}
 
    private void KeyControl ()
    {
        // 技能1
		if (Input.GetKeyDown(KeyCode.Q))
		{
			PlaySkillEffect_1();
		}
        // 技能2
		if (Input.GetKeyDown(KeyCode.W))
		{
            
		}
	}

	/// <summary>
	/// 播放技能1的特效
	/// </summary>
	private void PlaySkillEffect_1()
	{
		m_Animator.SetTrigger("skill_1");
		Transform skill1 = Instantiate(m_Skill1).transform;
		ParticleSystem part_1 = skill1.Find("SwordSlashRed").GetComponent<ParticleSystem>();
		part_1.Play();
		//Destroy(part_1.gameObject, 0.5f);
		ParticleSystem part_2 = skill1.Find("SwordHitRed").GetComponent<ParticleSystem>();
		part_2.Play();
		//Destroy(part_2.gameObject, 0.5f);
	}
}
