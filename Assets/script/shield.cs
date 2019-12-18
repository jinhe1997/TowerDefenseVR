using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour {

public float health = 50f;
	float currentHealth;
	public GameObject healthBarPrefab;
	GameObject healthBar;

void Awake() {
currentHealth = health;
		healthBar = Instantiate (healthBarPrefab, transform.position + new Vector3 (0, 0.9f, -0.03f), Quaternion.identity, transform);

	}

private void OnTriggerEnter(Collider other)
{
	GameObject obj = other.gameObject;
	if (obj.tag == "Enemy")
	{
		currentHealth -= obj.GetComponent<Enemy>().damage;

//改變綠色血量條大小 反映出傷害
		Transform pivot = healthBar.transform.Find("HealthyPivot");
		Vector3 scale = pivot.localScale;
		scale.x = Mathf.Clamp (currentHealth / health, 0, 1);
		pivot.localScale = scale;
	Destroy(obj);
	CheckHealth();

	}


}

void CheckHealth()
{

if(currentHealth <=0 )
{
	Destroy(gameObject);
}

	
		
	}
}
