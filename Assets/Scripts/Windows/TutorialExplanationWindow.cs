using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialExplanationWindow : Window {

    public override void Show() {
        base.Show();
        SetPosition(Screen.width / 2, Screen.height / 2);
    }

    protected override void MoveToFront() {
        this.gameObject.transform.parent.transform.SetAsLastSibling();
    }
}
