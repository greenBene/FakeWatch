using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeHandler : MonoBehaviour {

    Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
    }

    public void ChangeVolume() {
        AudioListener.volume = slider.value;
    }
}
