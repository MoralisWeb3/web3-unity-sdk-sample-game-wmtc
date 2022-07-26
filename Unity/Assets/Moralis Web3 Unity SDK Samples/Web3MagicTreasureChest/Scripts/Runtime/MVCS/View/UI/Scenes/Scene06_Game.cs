using System;
using System.Collections.Generic;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene06_Game : MonoBehaviour
    {
        //  Properties ------------------------------------
        public ObservableGameState ObservableGameState { get { return _observableGameState;}}
        
        //  Fields ----------------------------------------
        [Header("Debugging")]
        
        [SerializeField] 
        private ObservableGameState _observableGameState = new ObservableGameState();

        [Header("References (Scene)")]
        [SerializeField]
        private Scene06_GameUI _scene06_GameUI;

   
        [SerializeField] 
        private TreasureChestUI _treasureChestUI = null;

        [SerializeField] 
        private CardsUI _cardsUI = null;
        
        [Header("Reference Points (Scene)")]
        [SerializeField] 
        private ReferencePoint _treasureChestStartRP = null;
        
        [SerializeField] 
        private ReferencePoint _treasureChestEndRP = null;

        [SerializeField] 
        private ReferencePoint _cardStartRP = null;

        [SerializeField] 
        private List<ReferencePoint> _cardEndRPs = null;

        
        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new Exception("find existing user error");
            }
            
            _scene06_GameUI.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            _observableGameState.OnValueChanged.AddListener(ObservableGameState_OnValueChanged);
            _observableGameState.Value = GameState.Null;
            
            RefreshUI();

        }




        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            _scene06_GameUI.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }

        //  Event Handlers --------------------------------
        private void ObservableGameState_OnValueChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Null:
                    _observableGameState.Value = GameState.TreasureChestEntering;
                    TweenHelper.TransformMoveTo(_treasureChestUI.gameObject, _treasureChestStartRP.transform.position);
              
                    break;
                case GameState.TreasureChestEntering:
                    
                    TweenHelper.TransformDOBlendableMoveBy(_treasureChestUI.gameObject, _treasureChestStartRP.transform.position,
                        _treasureChestEndRP.transform.position, 0.25f, 0).onComplete = () =>
                    {
                        _observableGameState.Value = GameState.TreasureChestEntered;
                    };
                    
                    break;
                case GameState.TreasureChestEntered:
                    _observableGameState.Value = GameState.TreasureChestIdle;
                    break;
                case GameState.TreasureChestIdle:
                    _observableGameState.Value = GameState.TreasureChestOpening;
                    break;
                case GameState.TreasureChestOpening:
                    _observableGameState.Value = GameState.TreasureChestOpened;
                    break;
                case GameState.TreasureChestOpened:
                    _observableGameState.Value = GameState.CardsEntering;
                    _cardsUI.CreateCards(_cardStartRP, _cardEndRPs);
                    break;
                
                case GameState.CardsEntering:
                    _observableGameState.Value = GameState.CardsIdle;
                    break;
            }
        }
        
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}