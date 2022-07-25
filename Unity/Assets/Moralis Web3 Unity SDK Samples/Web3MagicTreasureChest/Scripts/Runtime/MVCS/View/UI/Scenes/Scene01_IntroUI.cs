using MoralisUnity.Samples.Shared.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// Replace with comments...
	/// </summary>
	public class Scene01_IntroUI : Scene_BaseUI
	{
		//  Properties ------------------------------------
		public BaseButtonUI PlayGameButtonUI { get { return _playGameButtonUI; } }
		public BaseButtonUI ViewCollectionButtonUI { get { return _viewCollectionButtonUI; } }
		public AuthenticationButtonUI AuthenticationButtonUI { get { return _authenticationButtonUI; } }
		public BaseButtonUI SettingsButtonUI { get { return _settingsButtonUI; } }

		
		//  Fields ----------------------------------------
		[SerializeField]
		private BaseButtonUI _playGameButtonUI = null;

		[SerializeField]
		private BaseButtonUI _viewCollectionButtonUI = null;

		[SerializeField]
		private AuthenticationButtonUI _authenticationButtonUI = null;

		[SerializeField]
		private BaseButtonUI _settingsButtonUI = null;

		
		//  Unity Methods----------------------------------


		//  General Methods -------------------------------

		
		//  Event Handlers --------------------------------
		
	}
}
