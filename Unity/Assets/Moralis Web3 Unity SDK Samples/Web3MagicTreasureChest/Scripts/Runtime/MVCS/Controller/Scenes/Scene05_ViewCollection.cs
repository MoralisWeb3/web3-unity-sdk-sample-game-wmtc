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
                throw new Exception("find existing user error");
            }
            
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

        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}