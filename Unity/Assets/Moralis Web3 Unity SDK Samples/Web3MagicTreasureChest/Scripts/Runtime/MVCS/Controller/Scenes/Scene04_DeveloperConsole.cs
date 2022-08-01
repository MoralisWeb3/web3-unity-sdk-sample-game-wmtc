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
            _ui.UnregisterButtonUI.Button.onClick.AddListener(UnregisterUserButtonUI_OnClicked);
            _ui.RegisterButtonUI.Button.onClick.AddListener(RegisterUserButtonUI_OnClicked);
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
            bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredUserAsync();
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
            bool wasRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredUserAsync();
            if (wasRegistered)
            {
                await TheGameSingleton.Instance.TheGameController.UnregisterUserAsync();
            }

            await ShowLoadingDuringMethodAsync(
              async delegate ()
              {
                  TheGameContract theGameContract = new TheGameContract();

                  bool result1 = await theGameContract.isRegistered();
                  int result4 = await theGameContract.getGold();

                  _outputTextStringBuilder.Clear();
                  _outputTextStringBuilder.AppendHeaderLine($"isRegistered()");
                  _outputTextStringBuilder.AppendHeaderLine($"result1 = {result1}");
                  _outputTextStringBuilder.AppendHeaderLine($"result4 = {result4}");

                  await RefreshUI();
              });



            await RefreshUI();
        }

        private async void UnregisterUserButtonUI_OnClicked()
        {
            bool wasRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredUserAsync();
            if (wasRegistered)
            {
                await TheGameSingleton.Instance.TheGameController.UnregisterUserAsync();
            }

            await ShowLoadingDuringMethodAsync(
              async delegate ()
              {
                  TheGameContract theGameContract = new TheGameContract();

                  bool result1 = await theGameContract.isRegistered();
                  string result2 = await theGameContract.Unregister();
                  bool result3 = await theGameContract.isRegistered();
                  int result4 = await theGameContract.getGold();

                  _outputTextStringBuilder.Clear();
                  _outputTextStringBuilder.AppendHeaderLine($"unregister()");
                  _outputTextStringBuilder.AppendHeaderLine($"result1 = {result1}");
                  _outputTextStringBuilder.AppendHeaderLine($"result2 = {result2}");
                  _outputTextStringBuilder.AppendHeaderLine($"result3 = {result3}");
                  _outputTextStringBuilder.AppendHeaderLine($"result4 = {result4}");

                  await RefreshUI();
              });

  

            await RefreshUI();
        }

        private async void RegisterUserButtonUI_OnClicked()
        {
            bool wasRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredUserAsync();
            if (!wasRegistered)
            {
                await TheGameSingleton.Instance.TheGameController.RegisterUserAsync();
            }

            TheGameContract theGameContract = new TheGameContract();

            bool result1 = await theGameContract.isRegistered();
            string result2 = await theGameContract.Register();
            bool result3 = await theGameContract.isRegistered();
            int result4 = await theGameContract.getGold();

            _outputTextStringBuilder.Clear();
            _outputTextStringBuilder.AppendHeaderLine($"Register()");
            _outputTextStringBuilder.AppendHeaderLine($"result1 = {result1}");
            _outputTextStringBuilder.AppendHeaderLine($"result2 = {result2}");
            _outputTextStringBuilder.AppendHeaderLine($"result3 = {result3}");
            _outputTextStringBuilder.AppendHeaderLine($"result4 = {result4}");

            await RefreshUI();
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
                    TheGameContract theGameContract = new TheGameContract();

                    //await TheGameSingleton.Instance.TheGameController.AddGold(1);
                    string result = await theGameContract.setGold(10);
                    
                    _outputTextStringBuilder.AppendHeaderLine($"setGold()");
                    _outputTextStringBuilder.AppendBullet($"result = {result}");

                    int result2 = await theGameContract.getGold();
                    _outputTextStringBuilder.AppendBullet($"result2 = {result2}");

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
                    //await TheGameSingleton.Instance.TheGameController.SpendGold(1);

                    TheGameContract theGameContract = new TheGameContract();

                    //await TheGameSingleton.Instance.TheGameController.AddGold(1);
                    string result = await theGameContract.setGoldBy(-5);

                    _outputTextStringBuilder.AppendHeaderLine($"setGoldBy()");
                    _outputTextStringBuilder.AppendBullet($"result = {result}");

                    int result2 = await theGameContract.getGold();
                    _outputTextStringBuilder.AppendBullet($"result2 = {result2}");

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