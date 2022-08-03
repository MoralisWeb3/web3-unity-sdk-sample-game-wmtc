using MoralisUnity.Samples.Shared.UI;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// The UI for <see cref="Scene06_Game"/>
	/// </summary> 
	public class Scene06_GameUI : Scene_UIWithTop
	{
		//  Properties ------------------------------------
		public BaseButtonUI ReplayButtonUI { get { return _replayButtonUI; } }

		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }

		//  Fields ----------------------------------------
		[Header("References (Scene)")]

		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		[SerializeField]
		private BaseButtonUI _replayButtonUI = null;

		
		//  Unity Methods----------------------------------


		//  General Methods -------------------------------

		
		//  Event Handlers --------------------------------
		
	}
}
