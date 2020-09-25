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
    [SerializeField] private SettingPopup menu;
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
        menu.gameObject.SetActive(false);
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
        if(Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = menu.gameObject.activeSelf;
            menu.gameObject.SetActive(!isShowing);

            if(isShowing)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

    }

    public void OnOpenSetting()
    {
        menuSetting.Open();
    }

    public void OnPointerDown()
    {

    }
}