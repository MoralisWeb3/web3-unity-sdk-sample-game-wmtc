using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System;
using System.Text;
using UnityEngine;
using MoralisUnity.Samples.Shared;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Samples.Shared.Exceptions;

#pragma warning disable 1998, 4014
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene04_DeveloperConsole : MonoBehaviour
    {
        //  Properties ------------------------------------
 
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene04_DeveloperConsoleUI _ui;

        private StringBuilder _titleTextBuilder = new StringBuilder();
        private StringBuilder _outputTextStringBuilder = new StringBuilder();

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                // Sometimes, ONLY warn
                Debug.LogWarning(new RequiredMoralisUserException().Message);
            }


            _titleTextBuilder.AppendLine("~The Developer Console~");
            _titleTextBuilder.AppendLine();

            TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnModelChanged);
            TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();

            
            _ui.IsRegisteredButtonUI.Button.onClick.AddListener(IsRegisteredButtonUI_OnClicked);
            _ui.UnregisterButtonUI.Button.onClick.AddListener(UnregisterButtonUI_OnClicked);
            _ui.RegisterButtonUI.Button.onClick.AddListener(RegisterButtonUI_OnClicked);
            _ui.AddGoldButtonUI.Button.onClick.AddListener(AddGoldButtonUI_OnClicked);
            _ui.SpendGoldButtonUI.Button.onClick.AddListener(SpendGoldButtonUI_OnClicked);
            _ui.AddTreasureButtonUI.Button.onClick.AddListener(AddTreasurePrizeButtonUI_OnClicked);
            _ui.SellTreasureButtonUI.Button.onClick.AddListener(SellTreasurePrizeButtonUI_OnClicked);
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            RefreshUI();
            
        }


        //  General Methods -------------------------------
        private async UniTask RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO

            _ui.Text.text = _titleTextBuilder.ToString() + _outputTextStringBuilder.ToString();
        }

        private async UniTask<bool> EnsureIsRegistered()
        {
            bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();
            if (!isRegistered)
            {
                _outputTextStringBuilder.Clear();
                _outputTextStringBuilder.AppendHeaderLine($"EnsureIsRegistered(). Failed.");
                await RefreshUI();
            }

            return isRegistered;
        }

        private async UniTask ShowLoadingDuringMethodAsync(Func<UniTask> task)
        {
            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
            true,
            false,
            SharedConstants.Loading,
            async delegate ()
            {
                await task();
            });
        }

        //  Event Handlers --------------------------------
        private async void OnModelChanged(TheGameModel theGameModel)
        {
            Debug.Log("Model change");
            _ui.TopUI.GoldCornerUI.Text.text = $"{theGameModel.Gold.Value}/100";
            _ui.TopUI.CollectionUI.Text.text = $"{theGameModel.TreasurePrizeDtos.Value.Count}/10";

            _outputTextStringBuilder.Clear();
            _outputTextStringBuilder.AppendHeaderLine($"OnModelChanged()");
            _outputTextStringBuilder.AppendHeaderLine($"Gold = {theGameModel.Gold.Value}");
            _outputTextStringBuilder.AppendHeaderLine($"TreasurePrizeDtos.Count = {theGameModel.TreasurePrizeDtos.Value.Count}");
            await RefreshUI();
        }

        
        private async void IsRegisteredButtonUI_OnClicked()
        {
           

            await ShowLoadingDuringMethodAsync(
              async delegate ()
              {
                 
                  bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

                  _outputTextStringBuilder.Clear();
                  _outputTextStringBuilder.AppendHeaderLine($"isRegistered()");
                  _outputTextStringBuilder.AppendBullet($"result = {isRegistered}");

                  await RefreshUI();
              });

        }

        private async void UnregisterButtonUI_OnClicked()
        {
            bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

            if (!isRegistered)
            {
                Debug.Log($"Operation cancelled, since isRegistered = {isRegistered}");
                return;
            }

            await ShowLoadingDuringMethodAsync(
              async delegate ()
              {
                  await TheGameSingleton.Instance.TheGameController.UnregisterAsync();

                  bool isRegisteredAfter = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

                  _outputTextStringBuilder.Clear();
                  _outputTextStringBuilder.AppendHeaderLine($"UnregisterAsync()");
                  _outputTextStringBuilder.AppendBullet($"result = {isRegisteredAfter}");

       
                  await RefreshUI();
              });

        }

        private async void RegisterButtonUI_OnClicked()
        {
            bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

            if (isRegistered)
            {
                Debug.Log($"Operation cancelled, since isRegistered = {isRegistered}");
                return;
            }

            await ShowLoadingDuringMethodAsync(
              async delegate ()
              {
                  await TheGameSingleton.Instance.TheGameController.RegisterAsync();

                  bool isRegisteredAfter = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

                  _outputTextStringBuilder.Clear();
                  _outputTextStringBuilder.AppendHeaderLine($"RegisterAsync()");
                  _outputTextStringBuilder.AppendBullet($"result = {isRegisteredAfter}");


                  await RefreshUI();
              });

        }


        private async void AddGoldButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }

            await ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    int gold = await TheGameSingleton.Instance.TheGameController.AddGold(2);
                    
                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"AddGold()");
                    _outputTextStringBuilder.AppendBullet($"result = {gold}");

                    await RefreshUI();
                });
   
        }



        private async void SpendGoldButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }


            await ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    int gold = await TheGameSingleton.Instance.TheGameController.SpendGold(1);

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"SpendGold()");
                    _outputTextStringBuilder.AppendBullet($"result = {gold}");

                    await RefreshUI();
                });
 
        }

        private async void AddTreasurePrizeButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }

            TreasurePrizeDto treasurePrizeDto = new TreasurePrizeDto("Blah again");

            await TheGameSingleton.Instance.TheGameController.AddTreasurePrize(treasurePrizeDto);

            _outputTextStringBuilder.AppendHeaderLine($"AddTreasurePrize()");
 
            await RefreshUI();
        }

        private async void SellTreasurePrizeButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }

            TreasurePrizeDto treasurePrizeDto = new TreasurePrizeDto("Blah to delete, pull from existing list");
            await TheGameSingleton.Instance.TheGameController.SellTreasurePrize(treasurePrizeDto);

            _outputTextStringBuilder.AppendHeaderLine($"SellTreasurePrize()");

            await RefreshUI();
        }


        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadSettingsSceneAsync();
        }
    }
}