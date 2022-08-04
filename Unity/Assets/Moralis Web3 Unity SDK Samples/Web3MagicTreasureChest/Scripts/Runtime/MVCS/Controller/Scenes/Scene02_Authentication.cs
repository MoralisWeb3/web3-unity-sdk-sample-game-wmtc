using MoralisUnity.Kits.AuthenticationKit;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene02_Authentication : MonoBehaviour
    {
        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        [SerializeField]
        private Scene02_AuthenticationUI _ui;

        private bool _hasMoralisUserAtStart = false;

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);
            _ui.AuthenticationKit.OnStateChanged.AddListener(AuthenticationKit_OnStateChanged);
            
            //TODO: Apparently the Authkit will log out automatically regardless of _hasMoralisUserAtStart
            _hasMoralisUserAtStart = await TheGameSingleton.Instance.HasMoralisUserAsync();
            Debug.Log("auth, and _hasMoralisUserAtStart: " + _hasMoralisUserAtStart);
            

        }


        //  General Methods -------------------------------


        //  Event Handlers --------------------------------
        private void AuthenticationKit_OnStateChanged(AuthenticationKitState authenticationKitState)
        {
            Debug.Log(authenticationKitState);

            switch (authenticationKitState)
            {
                case AuthenticationKitState.MoralisLoggedIn:
                    
                    //KEEP
                    Debug.Log($"Was _hasMoralisUserAtStart = {_hasMoralisUserAtStart}, Now = {authenticationKitState}");
                    BackButtonUI_OnClicked();
                    break;
            }
        }

        private void BackButtonUI_OnClicked()
        {
            // Stop any processes
            Destroy(_ui.gameObject);
            
            // Leave
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
    }
}