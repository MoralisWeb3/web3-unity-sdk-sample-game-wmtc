using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;

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


        public async UniTask<bool> IsRegisteredAsync()
        {
            bool result = await _theGameContract.isRegistered();
            return result;
        }


        public async UniTask<string> RegisterAsync()
        {
            string result = await _theGameContract.Register();
            return result;
        }


        public async UniTask<string> UnregisterAsync()
        {
            string result = await _theGameContract.Unregister();
            return result;
        }


        public async UniTask<int> GetGoldAsync()
        {
            int result = await _theGameContract.getGold();
            return result;
        }


        public async UniTask<string> SetGoldAsync(int targetBalance)
        {
            string result = await _theGameContract.setGold(targetBalance);
            return result;
        }

        public async UniTask<string> SetGoldByAsync(int deltaBalance)
        {
            string result = await _theGameContract.setGoldBy(deltaBalance);
            return result;
        }


        public UniTask<List<TreasurePrizeDto>> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
        {
            throw new System.NotImplementedException();
        }


        public UniTask<List<TreasurePrizeDto>> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
        {
            throw new System.NotImplementedException();
        }


        // General Methods --------------------------------

        // Event Handlers ---------------------------------

    }
}
