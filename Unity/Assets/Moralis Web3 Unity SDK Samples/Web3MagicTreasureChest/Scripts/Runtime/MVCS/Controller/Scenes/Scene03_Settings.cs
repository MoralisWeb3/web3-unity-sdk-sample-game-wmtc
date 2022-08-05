using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Exceptions;
using MoralisUnity.Samples.Web3MagicTreasureChest.Exceptions;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using UnityEngine;

#pragma warning disable 1998, 4014
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene03_Settings : MonoBehaviour
    {
        //  Properties ------------------------------------
 
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene03_SettingsUI _ui;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUserAsync)
            {
                throw new RequiredMoralisUserException();
            }

            _ui.ResetAllDataButtonUI.Button.onClick.AddListener(ResetAllDataButtonUI_OnClicked);
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            
            // Refresh -> CheckRegister -> Refresh Again
            RefreshUI();
            await TheGameSingleton.Instance.TheGameController.ShowMessagePassiveAsync(
                async delegate()
                {
                    // Refresh the model
                    bool isRegisteredAsync = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();
                    
                    if (!isRegisteredAsync)
                    {
                        throw new RequiredIsRegisteredException();
                    }

                    //Refresh after async
                    RefreshUI();
                    
                });

            
        }
        
        //  General Methods -------------------------------
        private async UniTask RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }
        
        private async void ResetAllData()
        {
            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Resetting,
                async delegate ()
                {
                    await TheGameSingleton.Instance.TheGameController.SafeReregisterDeleteAllTreasurePrizeAsync();
                    
                    // Refresh the model
                    bool isRegisteredAsync = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();
                    
                    if (!isRegisteredAsync)
                    {
                        throw new RequiredIsRegisteredException();
                    }

                    //Refresh after async
                    await RefreshUI();
                });
            
        }

        //  Event Handlers --------------------------------


        private void ResetAllDataButtonUI_OnClicked()
        {
            if (Input.GetKey(KeyCode.Space) 
                || Input.GetKey(KeyCode.RightShift) 
                || Input.GetKey(KeyCode.LeftShift))
            {
                // This is a secret menu for developers
                Debug.LogWarning("SpaceBar Held. Will Open Developer Console");
                TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
                TheGameSingleton.Instance.TheGameController.LoadDeveloperConsoleSceneAsync();
            }
            else
            {
                Debug.LogWarning("SpaceBar NOT Held. Will Reset All Data");
                TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
                ResetAllData();
            }
        }
        
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}