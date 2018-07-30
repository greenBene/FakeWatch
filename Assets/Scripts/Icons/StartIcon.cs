using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIcon : Icon {
	
	protected override void Execute() {
        if (!GameManager.Instance.RequestStateChange(GameState.Tutorial))
            print("Icon darf nicht wechseln");
	}
}
