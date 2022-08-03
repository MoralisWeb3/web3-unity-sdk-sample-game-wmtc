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


        //  Unity Methods----------------------------------
        protected async void Start()
        {
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();

            Debug.Log("hasMoralisUserAsync: " + hasMoralisUserAsync);

            if (hasMoralisUserAsync)
            {
                _ui.AuthenticationKit.OnDisconnected.AddListener(AuthenticationUI_OnDisconnected);
            }
            else
            {
                _ui.AuthenticationKit.OnConnected.AddListener(AuthenticationUI_OnConnected);
            }



        }


        //  General Methods -------------------------------


        //  Event Handlers --------------------------------


        private void BackButtonUI_OnClicked()
        {
            // Stop any processes
            Destroy(_ui.gameObject);
            
            // Leave
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();
            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }
        
        private void AuthenticationUI_OnConnected()
        {
            Debug.Log("AuthenticationUI_OnConnected");
            BackButtonUI_OnClicked();
        }

        private void AuthenticationUI_OnDisconnected()
        {
            Debug.Log("AuthenticationUI_OnConnected");
            BackButtonUI_OnClicked();
        }

    }
}