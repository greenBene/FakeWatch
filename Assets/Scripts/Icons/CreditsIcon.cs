using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsIcon : Icon {
	
	public CreditsWindow credits;
	
	protected override void Execute() {
		credits.Show();
	}
}
