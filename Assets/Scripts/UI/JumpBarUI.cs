using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBarUI : MonoBehaviour
{
    private Slider jumpSlider;
    private PlayerMovement pM;
    
    public void Start()
    {
        pM = FindObjectOfType<PlayerMovement>();
        jumpSlider = GetComponent<Slider>();
    }

    public void Update()
    {
        jumpSlider.value = pM._jumpPower01;
    }
}
