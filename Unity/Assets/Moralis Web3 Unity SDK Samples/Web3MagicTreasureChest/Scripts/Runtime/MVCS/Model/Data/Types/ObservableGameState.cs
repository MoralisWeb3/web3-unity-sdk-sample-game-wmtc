
using System;
using MoralisUnity.Samples.Shared.Data.Types;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types
{
	/// <summary>
	/// Determines the user-input state of the game
	/// </summary>
	public enum GameState
	{
		Null,
		TreasureChestEntering,
		TreasureChestEntered,
		TreasureChestIdle,
		WaitForUser,
		TreasureChestOpening,
		TreasureChestOpened,
		CardsEntering,
		CardsEntered,
		CardsIdle
		
	}
	
	/// <summary>
	/// Observable wrapper invoking events for <see cref="GameState"/>
	/// </summary>
	[Serializable]
	public class ObservableGameState : Observable<GameState>
	{
		//  Properties ------------------------------------

		
		//  Fields ----------------------------------------

		
		//  General Methods -------------------------------
		protected override GameState OnValueChanging(GameState oldValue, GameState newValue)
		{
			//Debug.Log($"ObservableGameState.OnValueChanging() newValue = {newValue}");
			Debug.Log($"{newValue}");
			
			//TODO: Throw errors here if state changes incorrectly per ordering
			return base.OnValueChanging(oldValue, newValue);
		}


		//  Event Handlers --------------------------------
	}
	
}
