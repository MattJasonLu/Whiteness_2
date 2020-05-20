using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject firePrefab;
    private GameObject player;
    private GameObject enemy;
    private Transform effectTrans;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        enemy = GameObject.FindWithTag("Enemy").gameObject;
        effectTrans = player.transform.Find("EffectPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtn1Click()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        GameObject fire = Instantiate(firePrefab, effectTrans);
        fire.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        player.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        fire.GetComponentInChildren<ParticleSystem>().Stop();
        GameObject fire2 = Instantiate(firePrefab, enemy.transform);
        fire2.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        fire2.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
