using MoralisUnity.Kits.AuthenticationKit;
using MoralisUnity.Samples.Shared.UI;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.Scenes
{
	/// <summary>
	/// The UI for <see cref="Scene02_Authentication"/>
	/// </summary>
	public class Scene02_AuthenticationUI : Scene_BaseUI
	{
		//  Properties ------------------------------------
		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }
		public AuthenticationKit AuthenticationKit { get { return _authenticationKit; } }


		//  Fields ----------------------------------------
		[Header("References (Scene)")]

		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		[SerializeField]
		private AuthenticationKit _authenticationKit = null;

		
		//  Unity Methods----------------------------------


		//  General Methods -------------------------------

		
		//  Event Handlers --------------------------------
		
	}
}
