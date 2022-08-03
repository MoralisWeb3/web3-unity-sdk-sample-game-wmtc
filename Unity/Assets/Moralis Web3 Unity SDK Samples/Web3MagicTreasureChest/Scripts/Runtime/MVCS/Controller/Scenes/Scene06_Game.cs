using System;
using System.Collections.Generic;
using MoralisUnity.Samples.Shared.Exceptions;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using UnityEngine;

#pragma warning disable 1998, 4014
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
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
        private Scene06_GameUI _ui;

   
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
 
            // 1. Listen to back button
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            _ui.ReplayButtonUI.Button.onClick.AddListener(ReplayButtonUI_OnClicked);

            // 2. Check for user
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new RequiredMoralisUserException();
            }

            _observableGameState.OnValueChanged.AddListener(ObservableGameState_OnValueChanged);
            _observableGameState.Value = GameState.Null;
            
            RefreshUI();

        }

        protected async void Update()
        {

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Debug");
                await _treasureChestUI.TakeDamage();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("Debug");
                await _treasureChestUI.Open(false);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("Debug");
                await _treasureChestUI.BounceWhileOpen();
            }
        }


        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }

        //  Event Handlers --------------------------------

        private async void ObservableGameState_OnValueChanged(GameState gameState)
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

                    await _treasureChestUI.TakeDamage();
                    
                    _observableGameState.Value = GameState.TreasureChestOpening;
                    break;
                case GameState.TreasureChestOpening:

                    bool willSkipWait = false;
                    await _treasureChestUI.Open(willSkipWait);
                    _observableGameState.Value = GameState.TreasureChestOpened;
                    break;
                case GameState.TreasureChestOpened:
                    _observableGameState.Value = GameState.CardsEntering;
                    break;
                case GameState.CardsEntering:
                    _observableGameState.Value = GameState.CardsEntered;

                    // Don't await this
                    _treasureChestUI.BounceWhileOpen();

                    await _cardsUI.CreateCards(_cardStartRP, _cardEndRPs);
                    break;
                case GameState.CardsEntered:
                    _observableGameState.Value = GameState.CardsIdle;

                    break;

                case GameState.CardsIdle:

                    int goldAmount = 33;
                    await TheGameSingleton.Instance.TheGameController.StartGameAndGiveRewardsAsync(goldAmount);
                    break;
            }
        }
        
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }

        private void ReplayButtonUI_OnClicked()
        {
            // For debugging
            TheGameSingleton.Instance.TheGameController.LoadGameSceneAsync();
        }
    }
}