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

		public BaseButtonUI UnregisterUserButtonUI { get { return _unregisterUserButtonUI; } }
		public BaseButtonUI RegisterUserButtonUI { get { return _registerUserButtonUI; } }
		public BaseButtonUI AddGoldButtonUI { get { return _addGoldButtonUI; } }
		public BaseButtonUI SpendGoldButtonUI { get { return _spendGoldButtonUI; } }
		public BaseButtonUI AddTreasureButtonUI { get { return _addTreasureButtonUI; } }
		public BaseButtonUI SellTreasureButtonUI { get { return _sellTreasureButtonUI; } }
		public BaseButtonUI BackButtonUI { get { return _backButtonUI; } }


		//  Fields ----------------------------------------
		[Header("References (Scene)")]

		[SerializeField]
		private TopUI _topUI = null;

		[SerializeField]
		private TMP_Text _text = null;

		[SerializeField]
		private BaseButtonUI _unregisterUserButtonUI = null;

		[SerializeField]
		private BaseButtonUI _registerUserButtonUI = null;

		[SerializeField]
		private BaseButtonUI _addGoldButtonUI = null;

		[SerializeField]
		private BaseButtonUI _spendGoldButtonUI = null;

		[SerializeField]
		private BaseButtonUI _addTreasureButtonUI = null;

		[SerializeField]
		private BaseButtonUI _sellTreasureButtonUI = null;

		[SerializeField]
		private BaseButtonUI _backButtonUI = null;

		//  Unity Methods----------------------------------


		//  General Methods -------------------------------


		//  Event Handlers --------------------------------

	}
}
