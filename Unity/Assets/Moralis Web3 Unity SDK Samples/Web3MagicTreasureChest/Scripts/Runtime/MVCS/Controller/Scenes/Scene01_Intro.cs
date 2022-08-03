using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using UnityEngine;

#pragma warning disable 1998
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene01_Intro : MonoBehaviour
    {
        //  Properties ------------------------------------
 
		
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene01_IntroUI _ui;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            _ui.PlayGameButtonUI.Button.onClick.AddListener(PlayGameButtonUI_OnClicked);
            _ui.ViewCollectionButtonUI.Button.onClick.AddListener(ViewCollectionButtonUI_OnClicked);
            _ui.AuthenticationButtonUI.Button.onClick.AddListener(AuthenticationButtonUI_OnClicked);
            _ui.SettingsButtonUI.Button.onClick.AddListener(SettingsButtonUI_OnClicked);
  
          
            bool isAuthenticated = _ui.AuthenticationButtonUI.IsAuthenticated;
            
            
            if (isAuthenticated)
            {
                Debug.Log($"isAuthenticated = {isAuthenticated}");
                
                bool isRegistered = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();
                
                if (!isRegistered)
                {
                    Debug.Log($"isRegistered before = {isRegistered}");
                    await TheGameSingleton.Instance.TheGameController.RegisterAsync();
                    bool isRegistered2 = await TheGameSingleton.Instance.TheGameController.IsRegisteredAsync();
                    Debug.Log($"isRegistered after = {isRegistered2}");
                }

            }
            else
            {
                Debug.Log($"isAuthenticated = {isAuthenticated}. User must click authenticate.");
            }
            
            RefreshUI();

        }




        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            bool isAuthenticated = _ui.AuthenticationButtonUI.IsAuthenticated;
            _ui.PlayGameButtonUI.IsInteractable = isAuthenticated;
            _ui.ViewCollectionButtonUI.IsInteractable = isAuthenticated;
            _ui.SettingsButtonUI.IsInteractable = isAuthenticated;
            _ui.AuthenticationButtonUI.IsInteractable = true;
            
        }

        //  Event Handlers --------------------------------

        private async void AuthenticationButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadAuthenticationSceneAsync(); 
        }
   
        
        private async void ViewCollectionButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadViewCollectionSceneAsync();
        }
        
        
        private async void SettingsButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadSettingsSceneAsync();
        }


        private async void PlayGameButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadGameSceneAsync();
        }
    }
}