using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private GameObject m_Effect_1;
    [SerializeField]
    private GameObject m_Effect_2;

    /// <summary>
    /// 进入战斗
    /// </summary>
    public void OnClickIntoBattle()
    {
        Debug.Log("Into Battle");
    }

    /// <summary>
    /// 测试技能1
    /// </summary>
    public void OnClickSkill_1()
    {
        Debug.Log("Skill_1");
        StartCoroutine(PlaySkill1());
    }

    IEnumerator PlaySkill1()
    {
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Battle");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Skill1");
        yield return new WaitForSeconds(0.3f);
        GameObject effect = Instantiate(m_Effect_1);
        effect.transform.SetParent(m_Player.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x + 0.3f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Skill1");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Battle");
        
    }

    /// <summary>
    /// 测试技能2
    /// </summary>
    public void OnClickSkill_2()
    {
        Debug.Log("Skill_2");
        GameObject effect = Instantiate(m_Effect_2);
        effect.transform.SetParent(m_Player.transform, false);
        effect.transform.localEulerAngles = new Vector3(0 , 90, 0);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
    }
}
