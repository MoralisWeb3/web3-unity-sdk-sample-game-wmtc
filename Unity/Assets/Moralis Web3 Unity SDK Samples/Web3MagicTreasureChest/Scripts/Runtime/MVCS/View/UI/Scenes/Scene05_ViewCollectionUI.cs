using MoralisUnity.Samples.Shared.UI;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// The UI for <see cref="Scene05_ViewCollection"/>
	/// </summary>
	public class Scene05_ViewCollectionUI : Scene_UIWithTop
	{
		//  Properties ------------------------------------

		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }

		//  Fields ----------------------------------------
		[Header("References (Scene)")]

		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		
		//  Unity Methods----------------------------------


		//  General Methods -------------------------------

		
		//  Event Handlers --------------------------------
		
	}
}
