using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_stats : MonoBehaviour {//estadisticas basicas del jugador como su dinero, el hp del edificio a proteger y la UI
	public Text Money_UI;
	public static int Money;
	public int Start_money=1000;
	public static int HP=500;
	public int hp_actual;
	public GameObject Fin_UI;
	// Use this for initialization
	void Start () 
	{
		Money = Start_money;
		hp_actual = HP;
	}
	void Update()
	{
		Money_UI.text=Money.ToString()+"€";
		if (HP<=0) 
		{
			Debug.Log ("Perdiste");
			Fin_UI.SetActive (true);
			Time.timeScale = 0f;
		}
	}

}
