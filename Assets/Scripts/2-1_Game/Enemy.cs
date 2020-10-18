using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Role
{
    public GameObject m_Player;
    public GameObject m_Player2;
    public GameObject m_Player3;

    [SerializeField]
    private Menu m_Menu;
    [SerializeField]
    private GameObject m_Effect_1;
    [SerializeField]
    private GameObject m_HitEffect;
    [SerializeField]
    private GameObject m_BumperEffect;
    /// <summary>
    /// Enemy retreat back position
    /// </summary>
    private Vector3 m_EnemyBackPos;
    private Vector3 m_EnemyOriginPos;
    private bool m_IsBack;
    private bool m_IsReturn;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveBack();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Defend()
    {
        base.Defend();
    }

    public void Retreat()
    {
        m_IsBack = true;
        m_IsReturn = false;
        // Coordinate the enemy's retreat position
        m_EnemyOriginPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        m_EnemyBackPos = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z);
    }

    /// <summary>
    /// The enemy fell back one step after being hit
    /// </summary>
    public void MoveBack()
    {
        if (m_IsBack)
        {
            StartCoroutine(MoveBackCor());
        }
    }

    IEnumerator MoveBackCor()
    {
        if (!m_IsReturn)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, m_EnemyBackPos, Time.deltaTime * 5);
            yield return new WaitForSeconds(0.2f);
            m_IsReturn = true;
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, m_EnemyOriginPos, Time.deltaTime * 4);
            yield return new WaitForSeconds(0.8f);
            this.transform.position = m_EnemyOriginPos;
            m_IsBack = false;
        }
    }

    public void CommonAttack()
    {
        StartCoroutine(CommonAttackCor());
    }

    IEnumerator CommonAttackCor()
    {
        GameObject effect = Instantiate(m_Effect_1);
        effect.transform.SetParent(this.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x - 0.5f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        effect.transform.localScale = new Vector3(effect.transform.localScale.x * 2, effect.transform.localScale.y * 2, effect.transform.localScale.z * 2);
        effect.transform.localEulerAngles = new Vector3(90, 180, -90);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        // 敌人攻击后玩家受击的延时
        yield return new WaitForSeconds(0.1f);
        m_Player.GetComponent<Player>().GetHit();
    }

    public void AllAttack()
    {
        StartCoroutine(AllAttackCor());
    }

    IEnumerator AllAttackCor()
    {
        GameObject effect = Instantiate(m_Effect_1);
        effect.transform.SetParent(this.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x - 0.5f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        effect.transform.localScale = new Vector3(effect.transform.localScale.x * 2, effect.transform.localScale.y * 2, effect.transform.localScale.z * 2);
        effect.transform.localEulerAngles = new Vector3(90, 180, -90);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        yield return new WaitForSeconds(0.1f);
        m_Player.GetComponent<Player>().GetHit();
        m_Player2.GetComponent<Player>().GetHit();
        m_Player3.GetComponent<Player>().GetHit();
    }

    public void RollAttack()
    {
        StartCoroutine(RollAttackCor());
    }

    IEnumerator RollAttackCor()
    {
        GameObject effect = Instantiate(m_Effect_1);
        effect.transform.SetParent(this.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x - 0.5f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        effect.transform.localScale = new Vector3(effect.transform.localScale.x * 2, effect.transform.localScale.y * 2, effect.transform.localScale.z * 2);
        effect.transform.localEulerAngles = new Vector3(90, 180, -90);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        yield return new WaitForSeconds(0.1f);
        Destroy(m_Player3.GetComponent<Player>().m_NumGo);
        m_Player3.GetComponent<Player>().GetHit();
    }

    /// <summary>
    /// 蓄力
    /// </summary>
    public void BumperAttack()
    {
        StartCoroutine(BumperAttackCor());
    }

    IEnumerator BumperAttackCor()
    {
        GameObject effect = Instantiate(m_BumperEffect);
        effect.transform.SetParent(this.transform, false);
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localScale = new Vector3(effect.transform.localScale.x * 2, effect.transform.localScale.y * 2, effect.transform.localScale.z * 2);
        effect.transform.localEulerAngles = new Vector3(90, 180, -90);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        yield return new WaitForSeconds(1f);
    }

    public void GetHit()
    {
        GameObject hitEffect = Instantiate(m_HitEffect);
        hitEffect.transform.SetParent(this.transform, false);
        hitEffect.transform.localScale = new Vector3(hitEffect.transform.localScale.x * 2, hitEffect.transform.localScale.y * 2, hitEffect.transform.localScale.z * 2);
        // hitEffect.transform.position = new Vector3(
        //     hitEffect.transform.position.x, hitEffect.transform.position.y, hitEffect.transform.position.z);
        hitEffect.GetComponent<ParticleSystem>().Play();
        Destroy(hitEffect, 2f);
    }
}
