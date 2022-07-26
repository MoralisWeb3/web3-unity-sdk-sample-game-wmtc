using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene01_Intro : MonoBehaviour
    {
        //  Properties ------------------------------------
 
		
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene01_IntroUI _scene01_IntroUI;

		
        //  Unity Methods----------------------------------
        protected async void Start()
        {
            _scene01_IntroUI.PlayGameButtonUI.Button.onClick.AddListener(PlayGameButtonUI_OnClicked);
            _scene01_IntroUI.ViewCollectionButtonUI.Button.onClick.AddListener(ViewCollectionButtonUI_OnClicked);
            _scene01_IntroUI.AuthenticationButtonUI.Button.onClick.AddListener(AuthenticationButtonUI_OnClicked);
            _scene01_IntroUI.SettingsButtonUI.Button.onClick.AddListener(SettingsButtonUI_OnClicked);

            RefreshUI();
            
        }

        
        //  General Methods -------------------------------
        private async void RefreshUI()
        {
            bool isAuthenticated = _scene01_IntroUI.AuthenticationButtonUI.IsAuthenticated;
            _scene01_IntroUI.PlayGameButtonUI.IsInteractable = isAuthenticated;
            _scene01_IntroUI.ViewCollectionButtonUI.IsInteractable = isAuthenticated;
            _scene01_IntroUI.SettingsButtonUI.IsInteractable = isAuthenticated;
            _scene01_IntroUI.AuthenticationButtonUI.IsInteractable = true;
            
        }

        //  Event Handlers --------------------------------
        private void AuthenticationButtonUI_OnClicked()
        {
            bool checkIfNeeded = _scene01_IntroUI.AuthenticationButtonUI.IsAuthenticated;
            TheGameSingleton.Instance.TheGameController.LoadAuthenticationSceneAsync(); 
        }
   
        
        private void ViewCollectionButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadViewCollectionSceneAsync();
        }
        
        
        private void SettingsButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadSettingsSceneAsync();
        }


        private void PlayGameButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.LoadGameSceneAsync();
        }
    }
}