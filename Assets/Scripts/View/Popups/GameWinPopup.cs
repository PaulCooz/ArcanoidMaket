using System;
using Dataers;
using DG.Tweening;
using Libs;
using Loaders;
using Logics;
using Logics.Loaders;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

namespace View.Popups
{
    public class GameWinPopup : Popup
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image progress;
        [SerializeField] private float animationDuration = 1.0f;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            titleText.text = LocaleManager.GetText("gameWinPopupTitle");

            var info = LevelLoader.GetLevelInfo();
            progress.fillAmount = info.x / info.y;
        }

        public override void Show()
        {
            transform.DOScale(Vector3.one, animationDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationDuration);
        }

        public void OnNextPush()
        {
            LevelManager.LoadNextLevel();
            Hide();
        }

        public void OnExitPush()
        {
            Hide();
            SceneChanger.LoadScene("Levels");
            Destroy(gameObject, 2 * animationDuration);
        }
    }
}