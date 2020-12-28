using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbteilungF {
	public class LogIn : MonoBehaviour
	{
		[SerializeField] bool DoLogin = true;

		public void Execute()
		{
			SceneManager.LoadScene(DoLogin ? StringCollecton.INGAME : StringCollecton.LOCKSCREEN);
		}
	}
}
