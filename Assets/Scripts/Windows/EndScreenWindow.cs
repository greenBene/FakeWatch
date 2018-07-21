using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenWindow : Window {
    [SerializeField]
    Text text;

    public override void Start() {
        base.Start();
        GameManager.Instance.RegistEnd(this);
    }
    public override void Show() {
        if (PlayerPrefs.GetString("language") == "german") {
            text.text = "Mitarbeiter Evaluation von FactcheckerIn ID: 0189310. \n\nSie haben " + GameManager.Instance.correctMarkedArticles +
                " Nachrichten korrekt auf ihren Warheitsgehalt beurteilt.\nDagegen haben Sie " + GameManager.Instance.wronglyMarkedArticlesAsTrue +
                " falsche Nachrichten als wahr\nund " + GameManager.Instance.wronglyMarkedArticlesAsFalse +
                " wahre Nachrichten als falsch eingestuft.";
        } else {
            text.text = "Employee Evaluation of Factchecker ID: 0189310.\n\nYou have checked " + GameManager.Instance.correctMarkedArticles +
                " news correctly based on their truth.\nYou marked " + GameManager.Instance.wronglyMarkedArticlesAsTrue +
                " fake news as true\nand " + GameManager.Instance.wronglyMarkedArticlesAsFalse +
                " true news as wrong.";
        }
        SetPosition(Screen.width / 2, Screen.height / 2);

        base.Show();
    }

    public override void Destroy() {
        GameManager.Instance.RegistEnd(this);
        base.Destroy();
    }

    public void Restart() {
        GameManager.Instance.RequestStateChange(GameState.Desktop);
    }
}
