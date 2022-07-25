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
        protected void Start()
        {
            Debug.Log("Scene01_Intro.Start()");
            
            _scene01_IntroUI.PlayGameButtonUI.Button.onClick.AddListener(PlayGameButtonUI_OnClicked);
            _scene01_IntroUI.ViewCollectionButtonUI.Button.onClick.AddListener(ViewCollectionButtonUI_OnClicked);
            _scene01_IntroUI.AuthenticationButtonUI.Button.onClick.AddListener(AuthenticationButtonUI_OnClicked);
            _scene01_IntroUI.SettingsButtonUI.Button.onClick.AddListener(SettingsButtonUI_OnClicked);
            
        }


        //  General Methods -------------------------------


        //  Event Handlers --------------------------------
        private void AuthenticationButtonUI_OnClicked()
        {
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