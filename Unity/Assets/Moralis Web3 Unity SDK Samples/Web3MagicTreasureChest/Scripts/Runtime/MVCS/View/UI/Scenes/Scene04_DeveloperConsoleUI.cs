using MoralisUnity.Samples.Shared.UI;
using UnityEngine;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;
using TMPro;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI.Scenes
{
	/// <summary>
	/// The UI for <see cref="Scene04_DeveloperConsole"/>
	/// </summary>
	public class Scene04_DeveloperConsoleUI : Scene_BaseUI
	{
		//  Properties ------------------------------------
		public TopUI TopUI { get { return _topUI; } }
		public TMP_Text Text { get { return _text; } }

		public BaseButtonUI IsRegisteredButtonUI { get { return _isRegisteredButtonUI; } }
		public BaseButtonUI UnregisterButtonUI { get { return _unregisterButtonUI; } }
		public BaseButtonUI RegisterButtonUI { get { return _registerButtonUI; } }
		public BaseButtonUI RewardPrizesButtonUI { get { return _rewardPrizesButtonUI; } }
		public BaseButtonUI SetGoldByPlusButtonUI { get { return _setGoldByPlusButtonUI; } }
		public BaseButtonUI SetGoldByMinusButtonUI { get { return _setGoldByMinusButtonUI; } }
		public BaseButtonUI AddTreasureButtonUI { get { return _addTreasureButtonUI; } }
		public BaseButtonUI SellTreasureButtonUI { get { return _sellTreasureButtonUI; } }
		public BaseButtonUI DeleteAllTreasurePrizesButtonUI { get { return _deleteAllTreasurePrizesButtonUI; } }
		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }


		//  Fields ----------------------------------------
		[Header("References (Scene)")]

		[SerializeField]
		private TopUI _topUI = null;

		[SerializeField]
		private TMP_Text _text = null;

		[SerializeField]
		private BaseButtonUI _isRegisteredButtonUI = null;

		[SerializeField]
		private BaseButtonUI _unregisterButtonUI = null;

		[SerializeField]
		private BaseButtonUI _registerButtonUI = null;

		[SerializeField]
		private BaseButtonUI _rewardPrizesButtonUI = null;

		[SerializeField]
		private BaseButtonUI _setGoldByPlusButtonUI = null;

		[SerializeField]
		private BaseButtonUI _setGoldByMinusButtonUI = null;

		[SerializeField]
		private BaseButtonUI _addTreasureButtonUI = null;

		[SerializeField]
		private BaseButtonUI _sellTreasureButtonUI = null;

		[SerializeField]
		private BaseButtonUI _deleteAllTreasurePrizesButtonUI = null;

		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		//  Unity Methods----------------------------------


		//  General Methods -------------------------------


		//  Event Handlers --------------------------------

	}
}
