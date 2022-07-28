using MoralisUnity.Samples.Shared.UI;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// The UI for <see cref="Scene01_Intro"/>
	/// </summary>
	public class Scene01_IntroUI : Scene_BaseUI
	{
		//  Properties ------------------------------------
		public TopUI TopUI { get { return _topUI; } }
		public DungeonUI DungeonUI { get { return _dungeonUI; } }
		public BaseButtonUI PlayGameButtonUI { get { return _playGameButtonUI; } }
		public BaseButtonUI ViewCollectionButtonUI { get { return _viewCollectionButtonUI; } }
		public AuthenticationButtonUI AuthenticationButtonUI { get { return _authenticationButtonUI; } }
		public BaseButtonUI SettingsButtonUI { get { return _settingsButtonUI; } }


        //  Fields ----------------------------------------
        [Header ("References (Scene)")]

		[SerializeField]
		private TopUI _topUI = null;

		[SerializeField]
		private DungeonUI _dungeonUI = null;

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
