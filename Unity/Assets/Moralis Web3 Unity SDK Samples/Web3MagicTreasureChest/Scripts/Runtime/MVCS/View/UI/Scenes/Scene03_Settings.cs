using System;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene03_Settings : MonoBehaviour
    {
        //  Properties ------------------------------------
 
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene03_SettingsUI _scene03_SettingsUI;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new Exception("find existing user error");
            }
            
            _scene03_SettingsUI.DeveloperConsoleButtonUI.Button.onClick.AddListener(DeveloperConsoleButtonUI_OnClicked);
            _scene03_SettingsUI.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            RefreshUI();
            
        }


        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            _scene03_SettingsUI.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }

        //  Event Handlers --------------------------------
   
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