using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
		public UniTask CreateCards(ReferencePoint cardStartReferencePoint, List<ReferencePoint> cardEndReferencePoints)
		{
			int index = 0;
			List<UniTask> uniTasks = new List<UniTask>();

			foreach (ReferencePoint cardReferencePoint in cardEndReferencePoints)
			{
                UniTask uniTask = CreateCard(cardStartReferencePoint, cardReferencePoint, index++);
				uniTasks.Add(uniTask);
			}

			return UniTask.WhenAll(uniTasks);
		}



		private UniTask CreateCard(ReferencePoint cardStartReferencePoint, ReferencePoint cardReferencePoint, int index)
		{
			CardUI cardUI = TheGameHelper.InstantiatePrefab<CardUI>(_cardUIPrefab, transform, cardReferencePoint.transform.position);

			float duration = 0.5f;
			float delay = 0.25f * index;

			TweenHelper.TransformDoScale(cardUI.gameObject, new Vector3(0, 0, 0), new Vector3(1, 1, 1), duration, delay);
			TweenHelper.TransformDORotate(cardUI.gameObject, new Vector3(90, 0, 0), new Vector3(0, 0, 0), duration, delay);

			bool isWaiting = true;
			TweenHelper.TransformDOBlendableMoveBy(cardUI.gameObject, cardStartReferencePoint.transform.position,
				cardReferencePoint.transform.position, duration, delay).onComplete = () =>
			{
				isWaiting = false;
			};

			return UniTask.WaitWhile(() => isWaiting); ;
		}

        // Event Handlers ---------------------------------


    }
}
