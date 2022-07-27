using MoralisUnity.Samples.Shared.UI;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// The UI for <see cref="Scene03_Settings"/>
	/// </summary>
	public class Scene03_SettingsUI : Scene_BaseUI
	{

		//  Properties ------------------------------------
		public TopUI TopUI { get { return _topUI; } }
		public BaseButtonUI DeveloperConsoleButtonUI { get { return _developerConsoleButtonUI; } }
		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }


		//  Fields ----------------------------------------
		[Header("References (Scene)")]
		[SerializeField]
		private TopUI _topUI = null;

		[SerializeField]
		private BaseButtonUI _developerConsoleButtonUI = null;

		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		
		//  Unity Methods----------------------------------


		//  General Methods -------------------------------

		
		//  Event Handlers --------------------------------
		
	}
}
