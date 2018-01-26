using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class RPGCharacterGUI : MonoBehaviour{
	RPGCharacterControllerFREE rpgCharacter;
	bool useNav;
	bool navToggle;

	void Start(){
		rpgCharacter = GetComponent<RPGCharacterControllerFREE>();
	}


}