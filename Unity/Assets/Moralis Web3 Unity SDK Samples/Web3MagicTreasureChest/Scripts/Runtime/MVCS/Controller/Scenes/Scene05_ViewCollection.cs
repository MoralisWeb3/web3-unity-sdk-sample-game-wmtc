using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System;
using MoralisUnity.Samples.Shared.Exceptions;
using UnityEngine;

#pragma warning disable 1998
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene05_ViewCollection : MonoBehaviour
    {
        //  Properties ------------------------------------

 
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene05_ViewCollectionUI _ui;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new RequiredMoralisUserException();
            }
            
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            RefreshUI();
            
        }


        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }

        //  Event Handlers --------------------------------
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}