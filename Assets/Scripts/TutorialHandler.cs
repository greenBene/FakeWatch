using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour {

    GameObject[] tutorialWindows;
    int index = 0;

    private void Start() {
        tutorialWindows = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) { //hohlt die childe objekte des tutorials und wirft sie in den array
            tutorialWindows[i] = transform.GetChild(i).gameObject;
            tutorialWindows[i].SetActive(false);
        }
        GameManager.Instance.RegistTutorial(this);
    }
    private void OnDestroy() {
        try {
            GameManager.Instance.RegistTutorial(this);//meldet nur sich selbst ab;
        } catch (System.Exception e) {
            print(e.Message);
        }
    }

    public void StartTutorial() {
        if (tutorialWindows.Length != 0) {
            tutorialWindows[0].SetActive(true);
            index = 0;
        }
    }

    public void AbortTutorial() {
        tutorialWindows[index].SetActive(false);
    }

    public void ExitTutorial() {
        GameManager.Instance.RequestStateChange(EventTrigger.Tutorial, EventMessage.Failed);
    }

    public void NextWindow() {
        if (index == tutorialWindows.Length) {
            GameManager.Instance.RequestStateChange(EventTrigger.Tutorial, EventMessage.Sucsess);
            return;
        }
        tutorialWindows[index].SetActive(false);
        index++;
        tutorialWindows[index].SetActive(true);
    }
}
