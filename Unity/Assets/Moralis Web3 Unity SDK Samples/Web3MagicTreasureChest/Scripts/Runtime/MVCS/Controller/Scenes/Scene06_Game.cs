using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Exceptions;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using UnityEngine;

#pragma warning disable 1998, 4014, 414
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene06_Game : MonoBehaviour
    {
        //  Properties ------------------------------------
        public ObservableGameState ObservableGameState
        {
            get { return _observableGameState; }
        }

        public bool IsReadyForUserToClickPlay
        {
            get { return _observableGameState.Value == GameState.WaitForUser; }
        }

        //  Fields ----------------------------------------
        [Header("Debugging")] [SerializeField]
        private ObservableGameState _observableGameState = new ObservableGameState();

        [Header("References (Scene)")] [SerializeField]
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

        private readonly List<int> _goldCostPerPlay = new List<int> { 10, 30, 150 };

        private int _lastGoldOwned = 0;
        private int _lastGoldSpent = 0;
        private Reward _lastReward;
        
        
        //  Unity Methods----------------------------------
        protected async void Start()
        {
            // 1. Listen to back button
            _ui.Play01ButtonUI.Button.onClick.AddListener(Play01ButtonUI_OnClicked);
            _ui.Play02ButtonUI.Button.onClick.AddListener(Play02ButtonUI_OnClicked);
            _ui.Play03ButtonUI.Button.onClick.AddListener(Play03ButtonUI_OnClicked);
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            // Dynamic text
            TheGameHelper.SetButtonText(_ui.Play01ButtonUI.Button, $"Play ({_goldCostPerPlay[0]} Gold)");
            TheGameHelper.SetButtonText(_ui.Play02ButtonUI.Button, $"Play ({_goldCostPerPlay[1]} Gold)");
            TheGameHelper.SetButtonText(_ui.Play03ButtonUI.Button, $"Play ({_goldCostPerPlay[2]} Gold)");

            // 2. Check for user
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new RequiredMoralisUserException();
            }

            // AddListener - Update View When Model Changes
            TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnTheGameModelChanged);
            _observableGameState.OnValueChanged.AddListener(ObservableGameState_OnValueChanged);
            
            //Refresh before async
            _observableGameState.Value = GameState.Null;
            RefreshUI();
            
            //Do async
            await TheGameSingleton.Instance.TheGameController.ShowMessagePassiveAsync(
                async delegate()
                {
                    // TODO: make a method of 'Refresh?'
                    // Refresh the model
                    await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

                    //Refresh after async
                    RefreshUI();
                    
                });
        }


        //  General Methods -------------------------------
        private async void StartGame(int goldSpent)
        {
            _lastGoldSpent = goldSpent;
            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                async delegate()
                {
                    await _treasureChestUI.TakeDamage();
                    await TheGameSingleton.Instance.TheGameController.StartGameAndGiveRewardsAsync(_lastGoldSpent);
                    _lastReward = await TheGameSingleton.Instance.TheGameController.GetRewardsHistoryAsync();
                    _observableGameState.Value = GameState.TreasureChestOpening;

                    await RefreshUI();

                });
        }

        private async UniTask RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true;
            _ui.Play01ButtonUI.IsInteractable = IsReadyForUserToClickPlay && _lastGoldOwned >= _goldCostPerPlay[0];
            _ui.Play02ButtonUI.IsInteractable = IsReadyForUserToClickPlay && _lastGoldOwned >= _goldCostPerPlay[1];
            _ui.Play03ButtonUI.IsInteractable = IsReadyForUserToClickPlay && _lastGoldOwned >= _goldCostPerPlay[2];
        }


        //  Event Handlers --------------------------------
        private async void ObservableGameState_OnValueChanged(GameState gameState)
        {
            Debug.Log("gameState: " + gameState);
            switch (gameState)
            {
                case GameState.Null:
                    _observableGameState.Value = GameState.TreasureChestEntering;
                    break;
                case GameState.TreasureChestEntering:
                    _observableGameState.Value = GameState.TreasureChestEntered;
                    break;
                case GameState.TreasureChestEntered:
                    _observableGameState.Value = GameState.TreasureChestIdle;
                    break;
                case GameState.TreasureChestIdle:
                    _observableGameState.Value = GameState.WaitForUser;
                    break;
                case GameState.WaitForUser:
                    //wait for user click
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
                    
                    // Don't await this
                    _treasureChestUI.BounceWhileOpen();

                    await _cardsUI.ShowCardsForReward(_lastReward, _cardStartRP, _cardEndRPs);
                    
                    _observableGameState.Value = GameState.CardsEntered;
                    break;
                case GameState.CardsEntered:
                    _observableGameState.Value = GameState.CardsIdle;
                    break;

                case GameState.CardsIdle:

                    string theTypeName = TheGameHelper.GetRewardTypeNameByType(_lastReward.Type);
                    string message =
                        $"Congratulations!\nYou Spent {_lastGoldSpent} and\nwon `{theTypeName}` worth {_lastReward.Price}.";
                    await TheGameSingleton.Instance.TheGameController.ShowMessageCustom(message,
                        5000);
                    break;
            }
        }


        // Event Handlers ---------------------------------
        private async void OnTheGameModelChanged(TheGameModel theGameModel)
        {
            _lastGoldOwned = theGameModel.Gold.Value;
            await RefreshUI();
        }


        private void Play01ButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            StartGame(_goldCostPerPlay[0]);
        }

        private void Play02ButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            StartGame(_goldCostPerPlay[1]);
        }


        private void Play03ButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            StartGame(_goldCostPerPlay[2]);
        }



        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}
