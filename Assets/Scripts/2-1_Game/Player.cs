using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Role
{
    public bool m_IsClicked = false;
    public GameObject m_Enemy;
    [SerializeField]
    private GameObject m_Camera;
    private Menu m_Menu;
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
    [SerializeField]
    private GameObject m_HitEffect;

    // Start is called before the first frame update
    void Start()
    {
        m_Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!m_IsClicked)
        {
            m_IsClicked = true;
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Defend()
    {
        base.Defend();
    }

    /// <summary>
    /// Test skill one
    /// </summary>
    public void PlaySkill_1()
    {
        StartCoroutine(PlaySkillCor_1());
    }

    /// <summary>
    /// Play skill one
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySkillCor_1()
    {
        // Play character attack animation
        this.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Battle");
        this.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Skill1");
        // Time delay
        yield return new WaitForSeconds(0.6f);
        // Play effect
        GameObject effect = Instantiate(m_Effect_1);
        effect.transform.SetParent(this.transform, false);
        effect.transform.position = new Vector3(
            effect.transform.position.x + 0.5f, effect.transform.position.y + 0.2f, effect.transform.position.z);
        // TODO 2020-9-18 15:32:03 Need to emit light before attack
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 2f);
        this.transform.Find("Anim").GetComponent<Animator>().ResetTrigger("Skill1");
        this.transform.Find("Anim").GetComponent<Animator>().SetTrigger("Battle");
        yield return new WaitForSeconds(0.5f);
        m_Camera.GetComponent<CameraMove>().SlowMotion();
        // Plays the effect of a attack on an enemy
        GameObject effect2 = Instantiate(m_Effect_1);
        effect2.transform.SetParent(m_Enemy.transform, false);
        effect2.transform.localScale = new Vector3(effect2.transform.localScale.x * 2, effect2.transform.localScale.y * 2, effect2.transform.localScale.z * 2);
        effect2.transform.position = new Vector3(
            effect2.transform.position.x, effect2.transform.position.y, effect2.transform.position.z);
        effect2.GetComponent<ParticleSystem>().Play();
        // TODO 2020-9-18 15:31:03 Need to emit light under attack
        Destroy(effect2, 2f);
        yield return new WaitForSeconds(0.5f);
        m_Camera.GetComponent<CameraMove>().NormalMotion();
        GameObject hitEffect = Instantiate(m_HitEffect);
        hitEffect.transform.SetParent(m_Enemy.transform, false);
        hitEffect.transform.localScale = new Vector3(hitEffect.transform.localScale.x * 2, hitEffect.transform.localScale.y * 2, hitEffect.transform.localScale.z * 2);
        // hitEffect.transform.position = new Vector3(
        //     hitEffect.transform.position.x, hitEffect.transform.position.y, hitEffect.transform.position.z);
        hitEffect.GetComponent<ParticleSystem>().Play();
        Destroy(hitEffect, 2f);
        // Update: 2020-9-18 15:23:50
        // After the skill is released, the character has to step back and the lens has to expand
        yield return new WaitForSeconds(0.5f);
        m_Camera.GetComponent<CameraMove>().OnAttackOver();
    }
}
