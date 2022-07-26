using System.Threading.Tasks;

namespace MoralisUnity.Samples.Shared.UI
{
    /// <summary>
    /// The UI for shared use.
    /// </summary>
    public class AuthenticationButtonUI : BaseButtonUI
    {
        //  Properties  ---------------------------------------
        public bool IsAuthenticated { get { return _hasMoralisUserAsync;}}
      
        //  Fields  ---------------------------------------
        private bool _hasMoralisUserAsync = false;
        
        //  Unity Methods  --------------------------------
        public async void Start()
        {
            await CheckState();
        }
        
        //  General Methods -------------------------------
        private async Task CheckState()
        {
            //_hasMoralisUserAsync = await TheGameSingleton.Instance.HasMoralisUserAsync();
            //await RefreshUI();
        }
        
        private async Task RefreshUI()
        {
            if (_hasMoralisUserAsync)
            {
                Text.text = SharedConstants.Logout;
            }
            else
            {
                Text.text = SharedConstants.Authenticate;
            }
        }
		
        //  Event Handlers --------------------------------

    }
}