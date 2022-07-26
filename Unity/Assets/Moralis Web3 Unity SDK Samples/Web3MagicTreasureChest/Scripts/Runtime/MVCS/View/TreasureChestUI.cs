using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the TreasureChest for the game
	/// </summary>
	public class TreasureChestUI : MonoBehaviour
	{
		// Properties -------------------------------------
		public Animator Animator { get { return _animator;}}
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private Animator _animator = null;
		
		//[Header("References (Project)")] 
			
		//  Unity Methods----------------------------------
		protected void Start()
		{

		}
		
		protected void Update()
		{

			if (Input.GetKeyDown(KeyCode.T))
			{
				_animator.SetTrigger("TakeDamage");
			}
			
			if (Input.GetKeyDown(KeyCode.O))
			{
				_animator.SetTrigger("Open");
			}
			
			if (Input.GetKeyDown(KeyCode.B))
			{
				_animator.SetTrigger("BounceWhileOpen");
			}
		}
		
		// General Methods --------------------------------

		
		// Event Handlers ---------------------------------

	}
}
