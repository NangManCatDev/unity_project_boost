using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSkybox : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 0.5f;
    // Update is called once per frame
    void Update() {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
