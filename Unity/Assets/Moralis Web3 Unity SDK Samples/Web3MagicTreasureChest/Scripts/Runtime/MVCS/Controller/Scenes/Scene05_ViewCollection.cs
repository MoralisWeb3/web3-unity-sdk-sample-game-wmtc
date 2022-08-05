using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared;
using MoralisUnity.Samples.Shared.Exceptions;
using UnityEngine;

#pragma warning disable 1998, 4014, 414
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

        private StringBuilder _titleTextBuilder = new StringBuilder();
        
        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new RequiredMoralisUserException();
            }
            
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            
            // Refresh -> CheckRegister -> Refresh Again
            await RefreshUI();
            _titleTextBuilder.Clear();
            _titleTextBuilder.AppendHeaderLine("Collection");
            await TheGameSingleton.Instance.TheGameController.ShowMessagePassiveAsync(
                async delegate ()
                {
                    // Refresh the model
                    await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

                    
                    List<TreasurePrizeDto> treasurePrizeDtos = 
                        await TheGameSingleton.Instance.TheGameController.GetTreasurePrizesAsync();
                    
                    if (treasurePrizeDtos.Count == 0)
                    {
                        _titleTextBuilder.AppendLine("• Your collection is empty");
                        _titleTextBuilder.AppendLine();
                        _titleTextBuilder.AppendLine("Play game to earn prizes!");
                    }
                    else
                    {
                        foreach (TreasurePrizeDto treasurePrizeDto in treasurePrizeDtos)
                        {
                            string goldText = TheGameHelper.FormatTextGold((int)treasurePrizeDto.Price);
                            _titleTextBuilder.AppendLine($"• {treasurePrizeDto.Title} {goldText}");
                        }

                        _titleTextBuilder.AppendLine("");
                        _titleTextBuilder.AppendLine("Selling items is not yet supported.");
                    }
                    await RefreshUI();
                });

            
        }

        //  General Methods -------------------------------
        private async UniTask RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true;

            
            _ui.Text.text = _titleTextBuilder.ToString();
        }

        //  Event Handlers --------------------------------
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}