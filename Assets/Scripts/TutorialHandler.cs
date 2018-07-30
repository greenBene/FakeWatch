using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    TutorialExplanationWindow[] tutorialWindows;
    [SerializeField]
    int index = 0;

    private void Start() {
        tutorialWindows = new TutorialExplanationWindow[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) { //hohlt die childe objekte des tutorials und wirft sie in den array
            tutorialWindows[i] = transform.GetChild(i).GetComponent<TutorialExplanationWindow>();
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
            tutorialWindows[0].Show();
            index = 0;
        }
    }

    public void AbortTutorial() {
        if(index < tutorialWindows.Length)
            tutorialWindows[index].Close();
        else
            tutorialWindows[tutorialWindows.Length-1].Close();
    }

    public void ExitTutorial() {
        LogSystem.LogOnFile("Exited Tutorial at Window number: " + (index + 1));
        GameManager.Instance.RequestStateChange(EventTrigger.Tutorial, EventMessage.Failed);
    }

    public void SkipTutorial() {
        LogSystem.LogOnFile("Skiped Tutorial at Window number: " + (index + 1));
        GameManager.Instance.RequestStateChange(EventTrigger.Tutorial, EventMessage.Scip);
    }

    public void NextWindow() {
        tutorialWindows[index].Close();
        index++;
        if (index == tutorialWindows.Length) {
            GameManager.Instance.RequestStateChange(EventTrigger.Tutorial, EventMessage.Sucsess);
            return;
        }
        tutorialWindows[index].Show();
    }
}
