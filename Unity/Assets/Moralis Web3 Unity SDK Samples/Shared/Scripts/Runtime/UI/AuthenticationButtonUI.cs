using MoralisUnity.Platform.Objects;
using System.Threading.Tasks;

#pragma warning disable 1998
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
            await CheckHasMoralisUserAsync();
        }
        
        //  General Methods -------------------------------
        private async Task CheckHasMoralisUserAsync()
        {
            Moralis.Start();
            MoralisUser moralisUser = await Moralis.GetUserAsync();
            _hasMoralisUserAsync = moralisUser != null;
            await RefreshUI();
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