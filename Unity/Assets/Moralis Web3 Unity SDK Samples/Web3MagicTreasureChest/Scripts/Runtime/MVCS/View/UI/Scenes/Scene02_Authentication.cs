using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Scene02_Authentication : MonoBehaviour
    {
        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        [SerializeField]
        private Scene02_AuthenticationUI _scene02_AuthenticationUI;


        //  Unity Methods----------------------------------
        protected async void Start()
        {
            _scene02_AuthenticationUI.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            bool hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (hasMoralisUserAsync)
            {
                _scene02_AuthenticationUI.AuthenticationKit.OnDisconnected.AddListener(AuthenticationUI_OnDisconnected);
            }
            else
            {
                _scene02_AuthenticationUI.AuthenticationKit.OnConnected.AddListener(AuthenticationUI_OnConnected);
            }
        }


        //  General Methods -------------------------------


        //  Event Handlers --------------------------------
        private void BackButtonUI_OnClicked()
        {
            // Stop any processes
            Destroy(_scene02_AuthenticationUI.gameObject);
            
            // Leave
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