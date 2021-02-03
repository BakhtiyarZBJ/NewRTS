using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Root : MonoBehaviour {

    public enum STATE { Idle, Moving, Combat, Following};
    [Header("State")]
    public STATE currentState;
    public TextMesh myStateText;

    [Header("Stats")]
    public int HP = 60;
    public int def = 5;
    public int atk = 10;
    public int atkVar = 2;
    public float atkSpeed = 2.5f;
    public float reach = 3;

    [Header("Detection")]
    public List<Root> detected;

    [Header("Other")]
    public ParticleSystem myDamageParticle;
    public ParticleSystem myHealParticle;
    public Slider barraHP;
    public float fadeTime = 6;
    public GameObject floatingText;
	// Use this for initialization
	void Start () {
        myStateText = GetComponentInChildren<TextMesh>();
        myDamageParticle = GetComponentInChildren<ParticleSystem>();
        myHealParticle =transform.Find("Heals").GetComponent<ParticleSystem>();

        ChangeState(STATE.Idle);
        
        barraHP.maxValue = HP;
        barraHP.value = HP;
        Invoke("FadeLifeBar", fadeTime);
	}
	
    public void ChangeState(STATE state)
    {
        currentState = state;
        if (myStateText != null) myStateText.text = state.ToString();
    }

    public void TakeDamage(int damage)
    {
        myDamageParticle.Emit(5);
        int realDamage = damage - def <= 0 ? 1 : damage - def;
        HP = HP - realDamage <= 0 ? 0 : HP - realDamage;
        //if (HP <= 0) Destroy(gameObject);
        barraHP.gameObject.SetActive(true);
        barraHP.value = HP;
        Invoke("FadeLifeBar", fadeTime);

        GameObject displayDmg = Instantiate(floatingText, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        displayDmg.GetComponent<Floating_Text>().Dano = realDamage.ToString();

        if (HP <= 0)
        {
            if (tag == "Kir1" || tag == "Kir2")
            {
                gameObject.SetActive(false);
                Invoke("KirRevive", 5);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void GetHeals(int heal)
    {
        myHealParticle.Emit(5);
        HP = HP + heal >= (int)barraHP.maxValue ? (int)barraHP.maxValue : HP + heal;

        barraHP.gameObject.SetActive(true);
        barraHP.value = HP;
        Invoke("FadeLifeBar", fadeTime);

        GameObject displayDmg = Instantiate(floatingText, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        displayDmg.GetComponent<Floating_Text>().Dano = "+" + heal.ToString();
    }

    public void FadeLifeBar()
    {
        if (barraHP.value == barraHP.maxValue)
        {
            barraHP.gameObject.SetActive(false);
        }
    }

    public void KirRevive()
    {
        HP = (int)barraHP.maxValue;
        barraHP.value = HP;
        gameObject.SetActive(true);
    }

}
