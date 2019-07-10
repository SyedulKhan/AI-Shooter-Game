using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currentCoin;
    public Text coinText;

	// Use this for initialization
	void Start () {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AddCoin(int coinToAdd)
    {
        currentCoin += coinToAdd;
        coinText.text = "Coins: " + currentCoin;
    }
}
