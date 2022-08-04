
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	/// <summary>
	/// Handles communication with external sources (e.g. database/servers/contracts)
	///		* See <see cref="TheGameSingleton"/> 
	/// </summary>
	public interface ITheGameService 
	{
		// Properties -------------------------------------
		PendingMessage PendingMessageActive { get; }
		PendingMessage PendingMessagePassive { get; }

		// General Methods --------------------------------
		
		// Delay that runs locally to await state change finality
		UniTask DelayExtraAfterStateChange();

		// Web3 API Call - Various Return Types
		UniTask<List<TreasurePrizeDto>> GetTreasurePrizesAsync();

		// RunContractFunction - Various Return Types
		UniTask<bool> IsRegisteredAsync();
		UniTask<int> GetGoldAsync();
		UniTask<Reward> GetRewardsHistoryAsync();
		UniTask<string> GetLastRegisteredAddress();

		// ExecuteContractFunction - Must Be String Return Type
		UniTask RegisterAsync();
		UniTask UnregisterAsync();
		UniTask SetGoldAsync(int targetBalance);
		UniTask SetGoldByAsync(int deltaBalance);
		UniTask AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto);
		UniTask SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto);
		UniTask DeleteAllTreasurePrizeAsync();
		UniTask StartGameAndGiveRewardsAsync(int goldAmount);

	}
}
