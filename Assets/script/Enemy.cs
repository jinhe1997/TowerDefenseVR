using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 20f;
	float currentHealth;


	public GameObject healthBarPrefab;
	GameObject healthBar;
	//打死他會得到的錢
	public float worth = 4f;
	public float damage = 10f;






	void Awake ()
	 {
		currentHealth = health;
		healthBar = Instantiate (healthBarPrefab, transform.position + new Vector3 (0, 0.35f, 0), Quaternion.identity, transform);
		
	}
	
	public void Hurt(float damage)
	{
		currentHealth -= damage;
		//判斷目標是否會掛掉
		if (currentHealth <= 0)
		{
			Money.Amount += worth;
			Destroy(gameObject);
		}

		//改變綠色血量條大小 反映出傷害
		Transform pivot = healthBar.transform.Find("HealthyPivot");
		Vector3 scale = pivot.localScale;
		scale.x = Mathf.Clamp (currentHealth / health, 0, 1);
		pivot.localScale = scale;

	}
		
	}
