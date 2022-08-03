using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System.Text;
using UnityEngine;
using MoralisUnity.Samples.Shared;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Shared.Exceptions;
using System.Collections.Generic;
using MoralisUnity.Platform.Objects;

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
            _ui.RegisterButtonUI.Button.onClick.AddListener(RegisterButtonUI_OnClicked);
            _ui.RewardPrizesButtonUI.Button.onClick.AddListener(RewardPrizesButtonUI_OnClicked);

            //
            _ui.UnregisterButtonUI.Button.onClick.AddListener(UnregisterButtonUI_OnClicked);
            _ui.SetGoldByPlusButtonUI.Button.onClick.AddListener(SetGoldByPlusButtonUI_OnClicked);
            _ui.SetGoldByMinusButtonUI.Button.onClick.AddListener(SetGoldByMinusButtonUI_OnClicked);
            _ui.AddTreasureButtonUI.Button.onClick.AddListener(AddTreasurePrizeButtonUI_OnClicked);
            _ui.SellTreasureButtonUI.Button.onClick.AddListener(SellTreasurePrizeButtonUI_OnClicked);
            _ui.DeleteAllTreasurePrizesButtonUI.Button.onClick.AddListener(DeleteAllTreasurePrizesButtonUI_OnClicked);
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
            // Use the cached here so its quick
            bool isRegisteredCached = TheGameSingleton.Instance.TheGameController.IsRegisteredCached();
            if (!isRegisteredCached)
            {
                _outputTextStringBuilder.Clear();
                _outputTextStringBuilder.AppendHeaderLine($"EnsureIsRegistered(). Failed.");
                await RefreshUI();
            }

            return isRegisteredCached;
        }



        //  Event Handlers --------------------------------
        private async void OnModelChanged(TheGameModel theGameModel)
        {
            _ui.TopUI.GoldCornerUI.Text.text = $"{theGameModel.Gold.Value}/100";
            _ui.TopUI.CollectionUI.Text.text = $"{theGameModel.TreasurePrizeDtos.Value.Count}/10";

            _outputTextStringBuilder.Clear();
            _outputTextStringBuilder.AppendHeaderLine($"OnModelChanged()");
            _outputTextStringBuilder.AppendBullet($"Gold = {theGameModel.Gold.Value}");
            _outputTextStringBuilder.AppendBullet($"TreasurePrizeDtos.Count = {theGameModel.TreasurePrizeDtos.Value.Count}");
            await RefreshUI();
        }

        
        private async void IsRegisteredButtonUI_OnClicked()
        {
            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
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

            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
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

            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
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


        
        private async void SetGoldByPlusButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    int gold = await TheGameSingleton.Instance.TheGameController.SetGoldByAsync(2);
                    
                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"AddGold()");
                    _outputTextStringBuilder.AppendBullet($"result = {gold}");

                    await RefreshUI();
                });
        }


        private async void SetGoldByMinusButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    int gold = await TheGameSingleton.Instance.TheGameController.SetGoldByAsync(-1);

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

            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    MoralisUser moralisUser = await TheGameSingleton.Instance.GetMoralisUserAsync();
                    TreasurePrizeMetadata treasurePrizeMetadata = new TreasurePrizeMetadata
                    {
                        Title = "test 123",
                        Price = 10
                    };
                    string metadata = TreasurePrizeDto.ConvertMetadataObjectToString(treasurePrizeMetadata);
                    TreasurePrizeDto treasurePrizeDto = new TreasurePrizeDto(moralisUser.ethAddress, metadata);
                    List <TreasurePrizeDto> treasurePrizeDtos = await TheGameSingleton.Instance.TheGameController.AddTreasurePrizeAsync(treasurePrizeDto);

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"AddTreasurePrize()");
                    _outputTextStringBuilder.AppendBullet($"result.Count = {treasurePrizeDtos.Count}");

                    await RefreshUI();
                });
        }

        
        private async void SellTreasurePrizeButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }
            
            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    List<TreasurePrizeDto> treasurePrizeDtos = await TheGameSingleton.Instance.TheGameController.GetTreasurePrizesAsync();

                    if (treasurePrizeDtos.Count == 0)
                    {
                        Debug.LogWarning("Nothing to sell. That is ok.");
                        return;
                    }

                    // Sell the most recent
                    TreasurePrizeDto treasurePrizeDto = treasurePrizeDtos[treasurePrizeDtos.Count-1];
                    List<TreasurePrizeDto> treasurePrizeDtosAfter = await TheGameSingleton.Instance.TheGameController.SellTreasurePrizeAsync(treasurePrizeDto);

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"SellTreasurePrize()");
                    _outputTextStringBuilder.AppendBullet($"result.Count was = {treasurePrizeDtos.Count}");
                    _outputTextStringBuilder.AppendBullet($"result.Count is  = {treasurePrizeDtosAfter.Count}");

                    await RefreshUI();
                });
        }
        
        private async void DeleteAllTreasurePrizesButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }
            
            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    List<TreasurePrizeDto> treasurePrizeDtos = 
                        await TheGameSingleton.Instance.TheGameController.DeleteAllTreasurePrizeAsync();

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"DeleteAllTreasurePrizeAsync()");
                    _outputTextStringBuilder.AppendBullet($"result.Count = {treasurePrizeDtos.Count}");

                    await RefreshUI();
                });
        }

        
        
            
        private async void RewardPrizesButtonUI_OnClicked()
        {
            if (!await EnsureIsRegistered())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    int goldAmount = 22;
                    await TheGameSingleton.Instance.TheGameController.StartGameAndGiveRewardsAsync(goldAmount);

                    Reward reward = await TheGameSingleton.Instance.TheGameController.GetRewardsHistoryAsync();
                    
                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"StartGameAndGiveRewards()");
                    _outputTextStringBuilder.AppendBullet($"Gold Spent = {goldAmount}");
                    _outputTextStringBuilder.AppendBullet($"reward.Title = {reward.Title}");
                    _outputTextStringBuilder.AppendBullet($"reward.Type = {reward.Type}");
                    _outputTextStringBuilder.AppendBullet($"reward.Price = {reward.Price}");
                    

                    await RefreshUI();
                });
        }


        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadSettingsSceneAsync();
        }
    }
}