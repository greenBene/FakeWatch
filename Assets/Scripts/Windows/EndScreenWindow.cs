using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EndScreenWindow : Window {
    [SerializeField]
    Text text;

    public override void Start() {
        base.Start();
        GameManager.Instance.RegistEnd(this);
    }
    public override void Show() {
        if (PlayerPrefs.GetString("language") == "german") {
            text.text = "Mitarbeiter Evaluation von FactcheckerIn ID: " + GameManager.Instance.PlayerID + ". \n\nSie haben " + GameManager.Instance.correctMarkedArticles +
                " Nachrichten korrekt auf ihren Warheitsgehalt beurteilt.\nDagegen haben Sie " + GameManager.Instance.wronglyMarkedArticlesAsTrue +
                " falsche Nachrichten als wahr\nund " + GameManager.Instance.wronglyMarkedArticlesAsFalse +
                " wahre Nachrichten als falsch eingestuft.";
        } else {
            text.text = "Employee Evaluation of Factchecker ID: " + GameManager.Instance.PlayerID + ".\n\nYou have checked " + GameManager.Instance.correctMarkedArticles +
                " news correctly based on their truth.\nYou marked " + GameManager.Instance.wronglyMarkedArticlesAsTrue +
                " fake news as true\nand " + GameManager.Instance.wronglyMarkedArticlesAsFalse +
                " true news as wrong.";
        }
        SetPosition(Screen.width / 2, Screen.height / 2);

        base.Show();
    }

    public override void Destroy() {
        try {
            GameManager.Instance.RegistEnd(this);
        }catch(System.Exception e) {
            print(e.Message);
        }
        base.Destroy();
    }

    public void Restart() {
        GameManager.Instance.RequestStateChange(GameState.Desktop);
    }
}
