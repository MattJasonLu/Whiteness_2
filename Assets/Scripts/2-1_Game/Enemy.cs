using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Role
{
    public GameObject m_Player;
    [SerializeField]
    private Menu m_Menu;
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
}
