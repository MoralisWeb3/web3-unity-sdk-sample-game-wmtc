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
        protected void Start()
        {
            _scene02_AuthenticationUI.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

        }


        //  General Methods -------------------------------


        //  Event Handlers --------------------------------
        private void BackButtonUI_OnClicked()
        {

            TheGameSingleton.Instance.TheGameController.LoadIntroSceneAsync();
        }

    }
}