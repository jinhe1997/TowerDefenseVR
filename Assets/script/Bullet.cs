using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {


	public Transform target;

	public float speed = 10f;
	public float damage = 5f;


	void Update ()
	{
		//確認目標存在
		if (target != null) {
			//往敵人方位移動
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
		} else {
			Destroy (gameObject);
		}
	}
		

	void OnTriggerEnter(Collider other)
	{

		GameObject obj = other.gameObject;

		//確認是目標
		if (obj.tag == "Enemy")
		{
			obj.SendMessage ("Hurt", damage);
			Destroy(gameObject);
		} 
		else if (obj.tag == "Ground")
		{
			Destroy(gameObject);
		}

	}

}
