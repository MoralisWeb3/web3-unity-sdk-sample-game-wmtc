using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene01_Intro : MonoBehaviour
    {
        //  Properties ------------------------------------
 
		
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene01_IntroUI _ui;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            _ui.PlayGameButtonUI.Button.onClick.AddListener(PlayGameButtonUI_OnClicked);
            _ui.ViewCollectionButtonUI.Button.onClick.AddListener(ViewCollectionButtonUI_OnClicked);
            _ui.AuthenticationButtonUI.Button.onClick.AddListener(AuthenticationButtonUI_OnClicked);
            _ui.SettingsButtonUI.Button.onClick.AddListener(SettingsButtonUI_OnClicked);

            TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnModelChanged);
            TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();

  
            RefreshUI();

            bool isAuthenticated = _ui.AuthenticationButtonUI.IsAuthenticated;
            if (isAuthenticated)
            {
                bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredUserAsync();

                if (!isRegistered)
                {
                    await TheGameSingleton.Instance.TheGameController.RegisterUserAsync();
                }
            }

        }




        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            bool isAuthenticated = _ui.AuthenticationButtonUI.IsAuthenticated;
            _ui.PlayGameButtonUI.IsInteractable = isAuthenticated;
            _ui.ViewCollectionButtonUI.IsInteractable = isAuthenticated;
            _ui.SettingsButtonUI.IsInteractable = isAuthenticated;
            _ui.AuthenticationButtonUI.IsInteractable = true;
            
        }

        //  Event Handlers --------------------------------
        private void OnModelChanged(TheGameModel theGameModel)
        {
            _ui.TopUI.GoldCornerUI.Text.text = $"{theGameModel.Gold.Value}/100";
            _ui.TopUI.CollectionUI.Text.text = $"{theGameModel.TreasurePrizeDtos.Value.Count}/10";
        }

        private void AuthenticationButtonUI_OnClicked()
        {
            bool checkIfNeeded = _ui.AuthenticationButtonUI.IsAuthenticated;
            TheGameSingleton.Instance.TheGameController.LoadAuthenticationSceneAsync(); 
        }
   
        
        private void ViewCollectionButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadViewCollectionSceneAsync();
        }
        
        
        private void SettingsButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadSettingsSceneAsync();
        }


        private void PlayGameButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadGameSceneAsync();
        }
    }
}