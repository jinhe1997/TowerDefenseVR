using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
public float MoveSpeed = 1f;
public Transform buliding;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, buliding.position, MoveSpeed * Time.deltaTime);
	}
}
