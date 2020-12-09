using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectableHandler : MonoBehaviour

{
	
	public Text coinText;

	public int coinCount = 0;



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
		coinText.text = coinCount.ToString();
		


	}

	void OnTriggerEnter(Collider col)
	{
		


		if (col.gameObject.CompareTag("coin"))
		{
            Debug.Log("Coin Collision");
			coinCount++;
		}


	}





}

