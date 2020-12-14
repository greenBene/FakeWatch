using UnityEngine;
using UnityEngine.SceneManagement;

public class Initiator : MonoBehaviour
{
	private void Start()
	{
		Data.GetInstance().mySDK = new LocalDataSDK();
		Data.GetInstance().myLocalisator = new JSONLocalisator();
		SceneManager.LoadScene(StringCollecton.LOCKSCREEN);
	}
}
