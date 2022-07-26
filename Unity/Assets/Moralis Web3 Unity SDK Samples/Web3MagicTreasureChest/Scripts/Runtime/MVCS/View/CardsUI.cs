using System.Collections.Generic;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the TreasureChest for the game
	/// </summary>
	public class CardsUI : MonoBehaviour
	{
		// Properties -------------------------------------
		public CardUI CardUIPrefab { get { return _cardUIPrefab;}}
		
		// Fields -----------------------------------------
		//[Header("References (Scene)")] 
		
		[Header("References (Project)")] 
		[SerializeField] 
		private CardUI _cardUIPrefab = null;
		
	
			
		//  Unity Methods----------------------------------
		protected void Start()
		{

		}
		
		protected void Update()
		{


		}
		
		// General Methods --------------------------------
		public void CreateCards(ReferencePoint cardStartReferencePoint, List<ReferencePoint> cardEndReferencePoints)
		{
			Debug.Log("1");
			int index = 0;
			foreach (ReferencePoint cardReferencePoint in cardEndReferencePoints)
			{
				Debug.Log("2");
				CreateCard(cardStartReferencePoint, cardReferencePoint, index++);
			}
		}

		private void CreateCard(ReferencePoint cardStartReferencePoint, ReferencePoint cardReferencePoint, int index)
		{
			CardUI cardUI = TheGameHelper.InstantiatePrefab<CardUI>(_cardUIPrefab, transform, cardReferencePoint.transform.position);
			
			TweenHelper.TransformDOBlendableMoveBy(cardUI.gameObject, cardStartReferencePoint.transform.position,
				cardReferencePoint.transform.position, 0.5f, 0.25f * index).onComplete = () =>
			{
				Debug.Log("done");
			};
		}
		
		// Event Handlers ---------------------------------


	}
}
