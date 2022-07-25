using MoralisUnity.Kits.AuthenticationKit;
using MoralisUnity.Samples.Shared.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// Replace with comments...
	/// </summary>
	public class Scene02_AuthenticationUI : Scene_BaseUI
	{
		//  Properties ------------------------------------
		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }
		public AuthenticationKit AuthenticationKit { get { return _authenticationKit; } }

		
		//  Fields ----------------------------------------
		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		[SerializeField]
		private AuthenticationKit _authenticationKit = null;

		
		//  Unity Methods----------------------------------


		//  General Methods -------------------------------

		
		//  Event Handlers --------------------------------
		
	}
}
