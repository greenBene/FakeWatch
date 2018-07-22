using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsWindow : Window {
    public override void Show() {
        base.Show();
        LogSystem.LogOnFile("Creadits got opened");
        SetPosition(Screen.width / 2, Screen.height / 2);
    }
}
