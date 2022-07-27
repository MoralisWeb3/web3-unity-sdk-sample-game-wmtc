using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	/// <summary>
	/// Handles communication with external sources (e.g. database/servers/contracts)
	///		* See <see cref="TheGameSingleton"/> 
	/// </summary>
	public class TheGameContractService : ITheGameService
	{
		// Properties -------------------------------------
		public PendingMessage PendingMessageForDeletion { get { return _pendingMessageForDeletion; }}
		public PendingMessage PendingMessageForSave { get { return _pendingMessageForSave; }}
		
		
		// Fields -----------------------------------------
		private readonly PendingMessage _pendingMessageForDeletion = new PendingMessage("Please confirm transaction in your wallet", 0);
		private readonly PendingMessage _pendingMessageForSave = new PendingMessage("Please confirm transaction in your wallet", 0);
		private readonly TheGameContract _theGameContract = null;


		// Initialization Methods -------------------------
		public TheGameContractService()
		{
			_theGameContract = new TheGameContract();
		}

        public UniTask<bool> IsRegisteredUserAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask<bool> RegisterUserAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask<bool> UnregisterUserAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask<int> AddGold(int delta)
        {
            throw new System.NotImplementedException();
        }

        public UniTask<int> SpendGold(int delta)
        {
            throw new System.NotImplementedException();
        }

        public UniTask<List<TreasurePrizeDto>> AddTreasurePrize(TreasurePrizeDto treasurePrizeDto)
        {
            throw new System.NotImplementedException();
        }

        public UniTask<List<TreasurePrizeDto>> SellTreasurePrize(TreasurePrizeDto treasurePrizeDto)
        {
            throw new System.NotImplementedException();
        }


        // General Methods --------------------------------

        // Event Handlers ---------------------------------

    }
}
