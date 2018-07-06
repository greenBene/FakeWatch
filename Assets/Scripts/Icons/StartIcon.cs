using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIcon : Icon {
	
	protected override void Execute() {
		SceneManager.LoadScene("Tutorial");
	}
}
