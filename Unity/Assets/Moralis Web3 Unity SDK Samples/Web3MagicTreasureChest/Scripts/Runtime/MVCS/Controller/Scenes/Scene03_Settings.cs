using MoralisUnity.Samples.Shared.Exceptions;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System;
using UnityEngine;

#pragma warning disable 1998
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene03_Settings : MonoBehaviour
    {
        //  Properties ------------------------------------
 
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene03_SettingsUI _ui;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                // Sometimes, ONLY warn
                Debug.LogWarning(new RequiredMoralisUserException().Message);
            }


            _ui.DeveloperConsoleButtonUI.Button.onClick.AddListener(DeveloperConsoleButtonUI_OnClicked);
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnModelChanged);
            TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();

            RefreshUI();
            
        }


        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }

        //  Event Handlers --------------------------------
        private void OnModelChanged(TheGameModel theGameModel)
        {
            _ui.TopUI.GoldCornerUI.Text.text = $"{theGameModel.Gold.Value}/100";
            _ui.TopUI.CollectionUI.Text.text = $"{theGameModel.TreasurePrizeDtos.Value.Count}/10";
        }

        private void DeveloperConsoleButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadDeveloperConsoleSceneAsync();
        }
        
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}