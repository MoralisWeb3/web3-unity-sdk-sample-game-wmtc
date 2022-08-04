using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the TreasureChest for the game
	/// </summary>
	public class CardUI : MonoBehaviour
	{
		// Properties -------------------------------------
		private Sprite GoldSprite { get { return _goldSprite;}}
		private Sprite PrizeSprite { get { return _prizeSprite;}}
		
		private SpriteRenderer SpriteRenderer { get { return _spriteRenderer;}}
		
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
