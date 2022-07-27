using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System;
using UnityEngine;

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
        private Scene05_ViewCollectionUI _scene05_ViewCollection;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new Exception("find existing user error");
            }
            
            _scene05_ViewCollection.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnModelChanged);
            TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();

            RefreshUI();
            
        }


        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            _scene05_ViewCollection.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }

        //  Event Handlers --------------------------------
        private void OnModelChanged(TheGameModel theGameModel)
        {
            _scene05_ViewCollection.TopUI.GoldCornerUI.Text.text = $"Gold {theGameModel.Gold.Value}/100";
            _scene05_ViewCollection.TopUI.CollectionUI.Text.text = $"Treasure {theGameModel.TreasurePrizeDtos.Value.Count}/10";
        }

        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}