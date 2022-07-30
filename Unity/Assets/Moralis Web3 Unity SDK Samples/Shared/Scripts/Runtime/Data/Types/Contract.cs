using System;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Samples.Shared.Exceptions;
using MoralisUnity.Sdk.Interfaces;
using MoralisUnity.Web3Api.Models;
using Nethereum.Hex.HexTypes;
using UnityEngine;
using WalletConnectSharp.Unity;

namespace MoralisUnity.Samples.Shared.Data.Types
{
	/// <summary>
	/// Wrapper class for a Web3API Eth Contract.
	/// </summary>
	public abstract class Contract: IInitializable
	{
		// Properties -------------------------------------
		public string Address { get { return _address; } }
		public string Abi { get { return _abi; } }
        public bool IsInitialized { get { return _isInitialized; } protected set { _isInitialized = value; } }
		public virtual ChainList ChainList 
		{ 
			get 
			{
				// Must override in sublcass
				throw new NotImplementedException();
			} 
		}

		// Fields -----------------------------------------
		protected string _address;
		protected string _abi;
		private bool _isInitialized = false;

		// Initialization Methods -------------------------
		public Contract()
		{
			Initialize();
		}


		public virtual void Initialize ()
		{
			if (IsInitialized)
			{
				return;
			}
			SetContractDetails();
			_isInitialized = true;
		}


		protected virtual void SetContractDetails()
		{
			// Must override in sublcass
			throw new NotImplementedException();
		}


		public void RequireIsInitialized()
		{
			if (!IsInitialized)
            {
				throw new NotInitializedException(this);
			}
		}


		// General Methods --------------------------------
		protected virtual object[] GetAbiObject ()
        {
			// Must override in sublcass
			throw new NotImplementedException();
		}

		protected async UniTask<string> ExecuteContractFunctionAsync(string functionName, object[] args, bool isLogging)
		{

			RequireIsInitialized();

			MoralisUser moralisUser = await Moralis.GetUserAsync();
			if (moralisUser == null)
			{
				throw new RequiredMoralisUserException();
			}


			if (WalletConnect.Instance == null)
			{
				throw new NullReferenceException("ExecuteContractFunction() failed. " + SharedConstants.WalletConnectNullReferenceException);
			}

			await Moralis.SetupWeb3();


			// Estimate the gas
			HexBigInteger value = new HexBigInteger(0);
			HexBigInteger gas = new HexBigInteger(0);
			HexBigInteger gasPrice = new HexBigInteger(0);

			if (isLogging)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine($"Contract.ExecuteContractFunction()...");
				stringBuilder.AppendLine($"");
				stringBuilder.AppendLine($"\taddress		= {_address}");
				stringBuilder.AppendLine($"\tabi.Length	= {_abi.Length}");
				stringBuilder.AppendLine($"\tfunctionName	= {functionName}");
				stringBuilder.AppendLine($"\targs		= {args}");
				stringBuilder.AppendLine($"\tvalue		= {value}");
				stringBuilder.AppendLine($"\tgas		= {gas}");
				stringBuilder.AppendLine($"\tgasPrice	= {gasPrice}");
				Debug.Log($"{stringBuilder.ToString()}");
				
				Debug.Log($"Moralis.ExecuteContractFunction() START");
			}
			
			// Related Documentation
			// Call Method (Read/Write) - https://docs.moralis.io/moralis-dapp/web3/blockchain-interactions-unity
			// Call Method (Read Only) - https://docs.moralis.io/moralis-dapp/web3-api/native#runcontractfunction
			string result = await Moralis.ExecuteContractFunction(_address, _abi, functionName, args, value, gas, gasPrice);

			if (isLogging)
			{
				Debug.Log($"Moralis.ExecuteContractFunction() FINISH. result = {result}");
			}

			return result;
		}

		public async UniTask<string> RunContractFunction(string functionName, object[] args, bool isLogging)
		{

			RequireIsInitialized();

			object[] abi = GetAbiObject();

			// Prepare the contract request
			RunContractDto runContractDto = new RunContractDto()
			{
				Abi = abi,
				Params = null
			};

			if (isLogging)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine($"Contract.RunContractFunction()...");
				stringBuilder.AppendLine($"");
				stringBuilder.AppendLine($"\taddress		= {_address}");
				stringBuilder.AppendLine($"\tfunctionName	= {functionName}");
				stringBuilder.AppendLine($"\trunContractDto.Abi.Length	= {runContractDto.Abi}");
				stringBuilder.AppendLine($"\trunContractDto.Params	= {runContractDto.Params}");
				stringBuilder.AppendLine($"\tchainList	= {ChainList}");
				Debug.Log($"{stringBuilder.ToString()}");

				Debug.Log($"Moralis.ExecuteContractFunction() START");
			}


			///////////////////////////////////////////
			// Execute: RunContractFunction	
			///////////////////////////////////////////
			MoralisClient moralisClient = Moralis.GetClient();

			string result = await moralisClient.Web3Api.Native.RunContractFunction(_address, functionName,
				runContractDto, ChainList);

			if (isLogging)
			{

				Debug.Log($"Moralis.ExecuteContractFunction() FINISH. result = {result}");
			}

			return result;
		}


    }

}
