using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour {

	public float money;
	public Text scoreText;
	
	public void CoinCollected()
	{
		money += 1;
		scoreText.text = "Coins: " + money;
	}
}
