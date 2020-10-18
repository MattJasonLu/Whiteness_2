using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


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
    [SerializeField]
    private GameObject m_Player2;
    [SerializeField]
    private GameObject m_Player3;
    /// <summary>
    /// Enemy gameobject
    /// </summary>
    [SerializeField]
    private GameObject m_Enemy;
   
    [SerializeField]
    private Text m_Title;

    void Start()
    {
        
    }

    void Update() 
    {
        ChooseRole();
    }

    public void OnClickPlayerSkill()
    {
        StartCoroutine(PlaySkillCor());
    }

    public void OnClickPlayerSkill2()
    {
        StartCoroutine(PlaySkill2Cor());
    }

    public void OnClickPlayerSkill3()
    {
        StartCoroutine(PlaySkill3Cor());
    }

    public void OnClickPlayerSkill4()
    {
        StartCoroutine(PlaySkill4Cor());
    }

    IEnumerator PlaySkillCor()
    {
        m_Player.GetComponent<Player>().PlaySkill_1();
        yield return new WaitForSeconds(1f);
        m_Enemy.GetComponent<Enemy>().Retreat();
        StartCoroutine(PlayEnemyRound1());
    }

    IEnumerator PlayEnemyRound1()
    {
        yield return new WaitForSeconds(2f);
        this.ShowText_1();
        yield return new WaitForSeconds(2f);
        m_Enemy.GetComponent<Enemy>().CommonAttack();
    }

    IEnumerator PlaySkill2Cor()
    {
        m_Player.GetComponent<Player>().PlaySkill_1();
        yield return new WaitForSeconds(1f);
        m_Enemy.GetComponent<Enemy>().Retreat();
        StartCoroutine(PlayEnemyRound3());
    }

    IEnumerator PlayEnemyRound3()
    {
        yield return new WaitForSeconds(2f);
        this.ShowText_2();
        yield return new WaitForSeconds(2f);
        m_Enemy.GetComponent<Enemy>().BumperAttack();
        yield return new WaitForSeconds(2f);
        m_Player.GetComponent<Player>().ShowNum(1);
        m_Player2.GetComponent<Player>().ShowNum(2);
        m_Player3.GetComponent<Player>().ShowNum(3);
    }

    /// <summary>
    /// 技能3
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySkill3Cor()
    {
        m_Player.GetComponent<Player>().PlaySkill_1();
        yield return new WaitForSeconds(1f);
        m_Enemy.GetComponent<Enemy>().Retreat();
        StartCoroutine(PlayEnemyRound4());
    }

    IEnumerator PlayEnemyRound4()
    {
        yield return new WaitForSeconds(2f);
        m_Enemy.GetComponent<Enemy>().AllAttack();
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlaySkill4Cor()
    {
        m_Player.GetComponent<Player>().PlaySkill_1();
        yield return new WaitForSeconds(1f);
        m_Enemy.GetComponent<Enemy>().Retreat();
        StartCoroutine(PlayEnemyRound5());
    }

    IEnumerator PlayEnemyRound5()
    {
        yield return new WaitForSeconds(2f);
        m_Enemy.GetComponent<Enemy>().RollAttack();
        yield return new WaitForSeconds(2f);
    }

    public void OnClickShake()
    {
        m_Camera.GetComponent<CameraMove>().ShakeCamera();  
    }

    /// <summary>
    /// Enlarge processing of selected roles
    /// </summary>
    public void ChooseRole()
    {
        // 判断是否选中了角色
        if (m_Player.GetComponent<Player>().m_IsClicked)
        {
            m_Player.GetComponent<Player>().m_IsClicked = false;
            // 人物往前移动，同时镜头放大，移动摄像机到合适的位置
            m_Camera.GetComponent<CameraMove>().OnClickRole();
        }
    }

    /// <summary>
    /// Change text
    /// </summary>
    public void ShowText_1()
    {
        StartCoroutine(ShowTextCor("黑蔷薇：愚蠢的入侵者"));
    }

    public void ShowText_2()
    {
        StartCoroutine(ShowTextCor("黑蔷薇：让我来审判你们"));
    }

    public void ShowText_3()
    {
        StartCoroutine(ShowTextCor("黑蔷薇：无理之徒"));
    }

    public void ShowText_4()
    {
        StartCoroutine(ShowTextCor("黑蔷薇：让这场闹剧结束吧"));
    }

    IEnumerator ShowTextCor(string text)
    {
        if (!m_Title.gameObject.activeSelf)
        {
            m_Title.gameObject.SetActive(true);
        }
        m_Title.text = text;
        yield return new WaitForSeconds(1);
        if (m_Title.gameObject.activeSelf)
        {
            m_Title.gameObject.SetActive(false);
        }
    }
}
