using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Exceptions;
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

            RefreshUI();
        }
        
        //  General Methods -------------------------------
        private async UniTask RefreshUI()
        {
            _ui.BackButtonUI.IsInteractable = true; // toggle some settings buttons, TODO
        }
        
        private async void ResetAllData()
        {
            await TheGameSingleton.Instance.TheGameController.ShowLoadingDuringMethodAsync(
                async delegate ()
                {
                    bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();

                    if (isRegistered)
                    {
                        await TheGameSingleton.Instance.TheGameController.UnregisterAsync();
                    }
                    
                    await TheGameSingleton.Instance.TheGameController.RegisterAsync();

                    await RefreshUI();
                });
            
        }

        //  Event Handlers --------------------------------


        private void ResetAllDataButtonUI_OnClicked()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.LogWarning("Spacebar Held. Opening Developer Console");
                TheGameSingleton.Instance.TheGameController.LoadDeveloperConsoleSceneAsync();
            }
            else
            {
                Debug.LogWarning("FYI, Hold Spacebar and click this button to open Developer Console");
                ResetAllData();
            }
        }
        
        private void BackButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}