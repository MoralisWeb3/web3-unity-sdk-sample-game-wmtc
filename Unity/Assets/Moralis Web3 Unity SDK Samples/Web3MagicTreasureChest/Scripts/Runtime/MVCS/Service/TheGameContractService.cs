using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Shared.Exceptions;
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


        // CORE Methods -------------------------
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

        public async UniTask<string> GetRewardsHistoryAsync()
        {
            string result = await _theGameContract.GetRewardsHistory();
            return result;
        }

        public async UniTask<string> GetMsgSender()
        {
            string result = await _theGameContract.getMsgSender();
            return result;
        }


        // OTHER Methods -------------------------

        public async UniTask<string> StartGameAndGiveRewardsAsync(int goldAmount)
        {
            string result = await _theGameContract.StartGameAndGiveRewards(goldAmount);
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


        public async UniTask<string> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
        {
            string result = await _theGameContract.MintNftAsync(treasurePrizeDto);
            return result;
        }


        public async UniTask<string> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
        {
            string result = await _theGameContract.BurnNftAsync(treasurePrizeDto);
            return result;
        }

        public async UniTask<string> BurnAllTreasurePrizeAsync(List<TreasurePrizeDto> treasurePrizeDtos)
        {
            string result = await _theGameContract.BurnNftsAsync(treasurePrizeDtos);
            return result;
        }



        public async Task<List<TreasurePrizeDto>> GetTreasurePrizesAsync()
        {
            // Create Method Return Value
            List<TreasurePrizeDto> treasurePrizeDtos = new List<TreasurePrizeDto>();

            // Check System Status
            bool hasMoralisUser = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralisUser)
            {
                // Sometimes, ONLY warn
                throw new RequiredMoralisUserException();
            }

            // Get NFT Info
            MoralisUser moralisUser = await TheGameSingleton.Instance.GetMoralisUserAsync();
            var nftCollection = await Moralis.Web3Api.Account.GetNFTsForContract(
                moralisUser.ethAddress,
                _theGameContract.TreasurePrizeContractAddress,
                _theGameContract.ChainList);

            Debug.Log("nftCollection.Result.Count: " + nftCollection.Result.Count);

            // Create Method Return Value
            foreach (NftOwner nftOwner in nftCollection.Result)
            {
                string ownerAddress = nftOwner.OwnerOf;
                string tokenIdString = nftOwner.TokenId;
                string metadata = nftOwner.TokenUri;
                Debug.Log($"nftOwner ownerAddress={ownerAddress} tokenIdString={tokenIdString} metadata={metadata}");
                TreasurePrizeDto treasurePrizeDto = TreasurePrizeDto.CreateNewFromMetadata(ownerAddress, tokenIdString, metadata) as TreasurePrizeDto;
                Debug.Log("created :  " + treasurePrizeDto);
                treasurePrizeDtos.Add(treasurePrizeDto);
            }

            // Finalize Method Return Value
            return treasurePrizeDtos;
        }

 



        // General Methods --------------------------------

        // Event Handlers ---------------------------------

    }
}
