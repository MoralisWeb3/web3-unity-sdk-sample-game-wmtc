using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the TreasureChest for the game
	/// </summary>
	public class CardUI : MonoBehaviour
	{
		// Properties -------------------------------------
		public Sprite GoldSprite { get { return _goldSprite;}}
		public Sprite PrizeSprite { get { return _prizeSprite;}}
		public SpriteRenderer SpriteRenderer { get { return _spriteRenderer;}}
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private Sprite _goldSprite = null;
		
		[SerializeField] 
		private Sprite _prizeSprite = null;
		
		[SerializeField] 
		private SpriteRenderer _spriteRenderer = null;

		
		//  Unity Methods----------------------------------
		
		// General Methods --------------------------------

		
		// Event Handlers ---------------------------------


	}
}
