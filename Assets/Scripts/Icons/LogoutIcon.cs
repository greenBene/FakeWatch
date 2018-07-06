using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutIcon : Icon {

	protected override void Execute() {
		SceneManager.LoadScene(0);
	}
}
