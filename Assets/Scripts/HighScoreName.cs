using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreName : MonoBehaviour {

    [SerializeField] Text text;

    public void SetHighScore() {
        LogSystem.SaveHighScore(text.text);
        GameManager.Instance.RequestStateChange(GameState.Desktop);
    }
}
