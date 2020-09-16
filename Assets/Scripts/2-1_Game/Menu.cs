using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Menu : MonoBehaviour
{
    /// <summary>
    /// Main camera
    /// </summary>
    [SerializeField]
    private GameObject m_Camera;
    /// <summary>
    /// Player gameobject
    /// </summary>
    [SerializeField]
    private GameObject m_Player;
    /// <summary>
    /// Enemy gameobject
    /// </summary>
    [SerializeField]
    private GameObject m_Enemy;
    /// <summary>
    /// Skill one
    /// </summary>
    [SerializeField]
    private GameObject m_Effect_1;
    /// <summary>
    /// Skill two
    /// </summary>
    [SerializeField]
    private GameObject m_Effect_2;

    /// <summary>
    /// Enemy retreat back position
    /// </summary>
    private Vector3 m_EnemyBackPos;
    private Vector3 m_EnemyOriginPos;
    /// <summary>
    /// Is the enemy retreating
    /// </summary>
    private bool m_IsEnemyBack = false;
    private bool m_EnemyBackDir = false;
    /// <summary>
    /// Post processing
    /// </summary>
    [SerializeField]
    private Volume m_Volume;

    private float m_TimeTracker = 0.0f;

    void Start()
    {
        
    }

    void Update() 
    {
        ChooseRole();
        EnemyMoveBack();
    }

    /// <summary>
    /// Test skill one
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
    /// Enlarge processing of selected roles
    /// </summary>
    public void ChooseRole()
    {
        // 判断是否选中了角色
        if (m_Player.GetComponent<RoleUnit>().isClicked)
        {
            // 人物往前移动，同时镜头放大
            MoveTowards();
            // 放大，移动摄像机到合适的位置
            m_Camera.GetComponent<CameraMove>().FocusRole();
        }
    }

    /// <summary>
    /// Character moves forward
    /// </summary>
    public void MoveTowards()
    {
        // 主角人物向前移动
        if (m_Player.transform.position.x < -1f)
        {
            Vector3 newPos = new Vector3(m_Player.transform.position.x + 1.5f, m_Player.transform.position.y, m_Player.transform.position.z);
            m_Player.transform.position = Vector3.Lerp(m_Player.transform.position, newPos, Time.deltaTime * 2);
            // 镜头对人物聚焦
            m_Volume.profile.components[0].parameters[2].SetValue(new FloatParameter(Mathf.Lerp(0.3f, 0.4f, Time.deltaTime * (m_TimeTracker += 0.1f) * 50)));
        }
    }

    /// <summary>
    /// The enemy fell back one step after being hit
    /// </summary>
    public void EnemyMoveBack()
    {
        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove()
    {
        // 播放完技能后显示敌人的受击效果
        if (m_IsEnemyBack)
        {
            if (m_EnemyBackDir)
            {
                m_Enemy.transform.position = Vector3.Lerp(m_Enemy.transform.position, m_EnemyBackPos, Time.deltaTime * 5);
                yield return new WaitForSeconds(0.2f);
                m_EnemyBackDir = false;
            }
            else
            {
                m_Enemy.transform.position = Vector3.Lerp(m_Enemy.transform.position, m_EnemyOriginPos, Time.deltaTime * 4);
                yield return new WaitForSeconds(0.8f);
                m_IsEnemyBack = false;
                m_Enemy.transform.position = m_EnemyOriginPos;
            }
        }
    }

    /// <summary>
    /// Play skill one
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySkill1()
    {
        m_TimeTracker = 0f;
        m_EnemyOriginPos = new Vector3(m_Enemy.transform.position.x, m_Enemy.transform.position.y, m_Enemy.transform.position.z);
        // 确定敌人撤退的位置坐标
        m_EnemyBackPos = new Vector3(m_Enemy.transform.position.x + 0.5f, m_Enemy.transform.position.y, m_Enemy.transform.position.z);
        

        // 播放人物攻击动画
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Battle");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Skill1");
        // 延时
        yield return new WaitForSeconds(0.6f);
        // 播放效果
        GameObject effect = Instantiate(m_Effect_1);
        effect.transform.SetParent(m_Player.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x + 0.5f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        m_Player.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Skill1");
        m_Player.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Battle");
        yield return new WaitForSeconds(0.5f);
        m_IsEnemyBack = true;
        m_EnemyBackDir = true;
        // 播放敌人受到的刀击效果
        GameObject effect2 = Instantiate(m_Effect_1);
        effect2.transform.SetParent(m_Enemy.transform, false);
        effect2.transform.localScale = new Vector3(effect2.transform.localScale.x * 2, effect2.transform.localScale.y * 2, effect2.transform.localScale.z * 2);
        effect2.transform.position = new Vector3(
            effect2.transform.position.x, effect2.transform.position.y, effect2.transform.position.z);
        effect2.GetComponent<ParticleSystem>().Play();
        Destroy(effect2, 2f);

        // TODO 技能释放完后人物要后退，镜头要扩大
    }

    /// <summary>
    /// Play skill two
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySkill2()
    {
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
