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

    private bool isMove = false;

    void Update() 
    {
        MoveTowards();
    }

    /// <summary>
    /// 测试技能1
    /// </summary>
    public void OnClickSkill_1()
    {
        StartCoroutine(PlaySkill1());
    }

    public void OnClickSkill_2()
    {
        StartCoroutine(PlaySkill2());
    }

    /// <summary>
    /// 人物向前移动
    /// </summary>
    public void MoveTowards()
    {
        if (isMove)
        {
            // 主角人物向前移动
            if (m_Player.transform.position.x < -1f)
            {
                Vector3 newPos = new Vector3(m_Player.transform.position.x + 1.5f, m_Player.transform.position.y, m_Player.transform.position.z);
                m_Player.transform.position = Vector3.Lerp(m_Player.transform.position, newPos, Time.deltaTime * 2);
            }
        }
    }

    /// <summary>
    /// 播放技能1
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySkill1()
    {
        isMove = true;
        yield return new WaitForSeconds(0.3f);
        isMove = false;
        // 播放人物攻击动画
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Battle");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Skill1");
        // 延时
        yield return new WaitForSeconds(0.3f);
        // 播放效果
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
    /// 播放技能2
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySkill2()
    {
        isMove = true;
        yield return new WaitForSeconds(0.3f);
        isMove = false;
        // 播放人物攻击动画
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Battle");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Skill1");
        // 延时
        yield return new WaitForSeconds(0.3f);
        // 播放效果
        GameObject effect = Instantiate(m_Effect_2);
        effect.transform.SetParent(m_Player.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x + 0.3f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Skill1");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Battle");
    }
}
