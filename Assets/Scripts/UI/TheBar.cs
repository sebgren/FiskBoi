using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheBar : MonoBehaviour

{
    [SerializeField]
    PlayerStats stats;

    [SerializeField]
    GameManager manager;
    [SerializeField]
    Text timer;

    [SerializeField]
    Image left;
    [SerializeField]
    Image right;


    // Start is called before the first frame update
    void Start()
    {
        manager = StaticReference.GameManager();
        stats = StaticReference.PlayerStats();
        left = GameObject.Find("LeftFill").GetComponent<Image>();
        right = GameObject.Find("RightFill").GetComponent<Image>();
        stats.breathEvent += (target, breath) =>
        {
            var adjustedBreath = breath + 10;
            adjustedBreath = adjustedBreath / 20;
            left.fillAmount = 1 - adjustedBreath;
            right.fillAmount = adjustedBreath;
        };
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = Math.Round(manager._timer, 2).ToString();
    }
}
