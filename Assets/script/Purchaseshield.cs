using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchaseshield : MonoBehaviour {

	public Text moneyText;
	public GameObject basicshieldPrefab;
	GameObject boughtshield;





	void Update () {
		moneyText.text = "Money $" + Money.Amount;
		//確保有塔可移動
		if (boughtshield != null)
		{
		MovePurchasedshield ();
		CheckForWall ();
		}
	}

	void MovePurchasedshield()
	{

		//移動購買的塔在視線上
		boughtshield.transform.position = Camera.main.transform.position +
		Camera.main.transform.forward * 5;
		
	}

	void CheckForWall()
	{
		//創造射線
		Ray raycast = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hit;

		Debug.DrawRay (raycast.origin, raycast.direction * 100);


		if (Physics.Raycast (raycast, out hit))
		{

		if (hit.collider.gameObject.tag == "shield")
		{
			//把塔放置在牆上
			boughtshield.transform.position = hit.collider.gameObject.transform.position +
			new Vector3 (0, 0.75f, 0);

		//按按鈕買塔
		if (Input.anyKeyDown)
{
		//去掉放置區塊的標籤
		hit.collider.gameObject.tag = "Untagged";

		//讓塔變不透明
		Color colour = boughtshield.GetComponent<Renderer>().material.color;
		colour.a = 1f;
		boughtshield.GetComponent<Renderer> ().material.color = colour;
		//把塔的程式碼啟用
		boughtshield.GetComponent<shield>().enabled = true;

		//放置塔 才能去買其他的塔
		boughtshield = null;
}

		}
	}

	}


	public void BuyBasicshield()
	{
		//若有足購金錢 是否會買防禦塔
		if (Money.Amount < 40 || boughtshield != null)
			return;

		//產生新的防禦塔
		boughtshield = Instantiate(basicshieldPrefab, Camera.main.ScreenToWorldPoint( new Vector3 (0.5f, 0.5f)),Quaternion.identity);

		//讓塔變透明
		Color colour = boughtshield.GetComponent<Renderer>().material.color;
		colour.a = 0.5f;
		boughtshield.GetComponent<Renderer> ().material.color = colour;
	 
		//買塔減錢
		Money.Amount -= 20;
	}


}
