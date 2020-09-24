using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scrollLable;
    [SerializeField] private SettingPopup menuSetting;
    private int score;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnEnemyHit()
    {
        score += 1;
        scrollLable.text = score.ToString();
    }

    private void Start()
    {
        score = 0;
        scrollLable.text = score.ToString();
        menuSetting.Close();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void Update()
    {
       // scrollLable.text = Time.realtimeSinceStartup.ToString();
    }

    public void OnOpenSetting()
    {
        menuSetting.Open();
    }

    public void OnPointerDown()
    {

    }
}