using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public float range = 3f;
	public float fireRate = 1f;
	public GameObject bulletPrefab;
	public Transform barrelExit;

	public float health = 25f;
	float currentHealth;
	public GameObject healthBarPrefab;
	GameObject healthBar;



  Transform target;
	float fireCounter = 0;



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

	void Update()
	{
		FindNextTarget ();

		if ( target != null)
		{
			AimAtTarget ();
			Shoot ();
		}
	}

	void FindNextTarget()
	{
		//尋找誰在砲塔範圍內
		int layerMask = 1 << 8;
		Collider[] enemies = Physics.OverlapSphere(transform.position, range, layerMask );

		//確認目標在範圍內
		if (enemies.Length > 0)
		{

			//假設 第一個目標 在最近的距離

			target = enemies[0].gameObject.transform;

			// 循環通過 全部的目標
			foreach (Collider enemy in enemies)
			{

				//計算 目標對於塔的距離
				float distance = Vector3.Distance(transform.position , enemy.transform.position);

				//找出 哪個目標是最近的
				if (distance < Vector3.Distance (transform.position, target.position)) {
					target = enemy.gameObject.transform;
				}

			}

		}

		else
		{
			target = null;
		}
	}

	void AimAtTarget()
	{
		//對準目標
		Vector3 LookPos = target.position - transform.position;
		LookPos.y = 0;

		Quaternion rotation = Quaternion.LookRotation (LookPos);
		transform.rotation = rotation;
	}



	void Shoot()
	{
		//確認是否能再發射
		if (fireCounter <= 0) {
			GameObject newBullet = Instantiate (bulletPrefab, barrelExit.position, Quaternion.identity);
			newBullet.GetComponent<Bullet> ().target = target;
			fireCounter = fireRate;
		}
		else
		{
			fireCounter -= Time.deltaTime;
	}



}

}