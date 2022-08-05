// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "hardhat/console.sol";
import "contracts/Gold.sol";
import "contracts/TreasurePrize.sol";


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   The proxy contact
//                                  for all other contracts
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract TheGameContract
{

    ///////////////////////////////////////////////////////////
    // STRUCTS
    ///////////////////////////////////////////////////////////
    struct Reward 
    {
        string Title;
        uint Type;
        uint Price;
    }

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////

    // Stores address of the Gold contract, to be called
    address _goldContractAddress;

    // Stores address of the TreasurePrize contract, to be called
    address _treasurePrizeContractAddress;

    mapping(address => bool) private _isRegistered;

    // Stores the most recent reward
    mapping (address => Reward) private _lastReward;

    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor(address goldContractAddress, address treasurePrizeContractAddress) 
    {
        _goldContractAddress = goldContractAddress;
        _treasurePrizeContractAddress = treasurePrizeContractAddress;

        console.log(
            "TheGameContract.constructor() _goldContractAddress = %s, _treasurePrizeContractAddress = %s",
            _goldContractAddress,
            _treasurePrizeContractAddress
        );
    }

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: HELPER
    ///////////////////////////////////////////////////////////
    function randomRange (uint min, uint max, uint nonce) public view returns (uint) 
    {
        // The nonce is especially useful for unit-tests, to ensure variation
        uint randomnumber = uint(keccak256(abi.encodePacked(block.timestamp, block.difficulty, msg.sender, nonce))) % (max);
        randomnumber = randomnumber + min;
        return randomnumber;
    }
    
    function convertRewardToString (Reward memory reward) public pure returns (string memory rewardString) 
    {
        rewardString = string(abi.encodePacked("Title=", reward.Title, "|Type=", reward.Type, "|Price=", reward.Price));
    }
    

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: DEBUGGING ONLY - CONFIRMING WHAT'S POSSIBLE
    ///////////////////////////////////////////////////////////
    function getMessage(string memory messageIn) external pure returns (string memory messageOut)
    {
        messageOut = messageIn;
    }

    function getAddress(address addressIn) external pure returns (address addressOut)
    {
        addressOut = addressIn;
    }

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: MODIFIERS
    ///////////////////////////////////////////////////////////
    modifier ensureIsRegistered (address userAddress)
    {
        // Validate
        require(_isRegistered[userAddress], "Must be registered");

        // Execute rest of function
      _;
    }



    ///////////////////////////////////////////////////////////
    // FUNCTIONS: GETTERS
    ///////////////////////////////////////////////////////////

    function getIsRegistered(address userAddress) public view returns (bool isRegistered) 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        isRegistered = _isRegistered[userAddress];
    }


    function getGold(address userAddress) public view ensureIsRegistered (userAddress) returns (uint256 balance) 
    {
        
        balance = Gold(_goldContractAddress).getGold(userAddress);
    }


    function getRewardsHistory(address userAddress) external view ensureIsRegistered (userAddress) returns (string memory rewardString)
    {
        rewardString = convertRewardToString(_lastReward[userAddress]);
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REGISTRATION
    ///////////////////////////////////////////////////////////
    function register() public
    {
        _isRegistered[msg.sender] = true;
        setGold(100);
    }


    function unregister() public ensureIsRegistered (msg.sender)
    {

        //Update gold first
        setGold(0);

        //Then unregister
        _isRegistered[msg.sender] = false;

    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REWARDS
    ///////////////////////////////////////////////////////////
    function startGameAndGiveRewards(uint256 goldAmount) ensureIsRegistered (msg.sender) external
    {
        require(goldAmount > 0, "goldAmount must be > 0 to start the game");

        require(getGold(msg.sender) >= goldAmount, "getGold() must be >= goldAmount to start the game");

        // Deduct gold
        setGoldBy(-int(goldAmount));

        uint random = randomRange (0, 100, 1);
        uint price = random;
        uint theType = 0;
        string memory title = "";

        if (random < 50)
        {
            // REWARD: Gold!
            theType = 1;
            title = "This is gold.";
            setGoldBy (int(random));
        } 
        else 
        {
            // REWARD: Prize!
            theType = 2;
            title = "This is an nft.";
        }

        _lastReward[msg.sender] = Reward (
        {
            Title: title,
            Type: theType,
            Price: price
        });

        if (theType == 2)
        {
            //NOTE: Metadata structure must match in both: TheGameContract.sol and TreasurePrizeDto.cs
            string memory metadata = convertRewardToString(_lastReward[msg.sender]);
            addTreasurePrize (metadata);     
        }


    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: CLEAR DATA
    ///////////////////////////////////////////////////////////
    function safeReregisterAndDeleteAllTreasurePrizes(uint256[] calldata tokenIds) external
    {
        // Do not require isRegistered for this method to run
        bool isRegistered = getIsRegistered(msg.sender);
        if (isRegistered)
        {
            unregister();
        }

        register();
        deleteAllTreasurePrizes(tokenIds);
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: GOLD
    ///////////////////////////////////////////////////////////
    function setGold(uint256 targetBalance) ensureIsRegistered (msg.sender) public
    {
        Gold(_goldContractAddress).setGold(msg.sender, targetBalance);
    }


    function setGoldBy(int delta) ensureIsRegistered (msg.sender) public
    {
        Gold(_goldContractAddress).setGoldBy(msg.sender, delta); 
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: TREASURE PRIZE
    ///////////////////////////////////////////////////////////
    function addTreasurePrize(string memory tokenURI) ensureIsRegistered (msg.sender)  public 
    {
        TreasurePrize(_treasurePrizeContractAddress).mintNft(msg.sender, tokenURI);
    }


    function deleteAllTreasurePrizes(uint256[] calldata tokenIds) ensureIsRegistered (msg.sender)  public
    {
        TreasurePrize(_treasurePrizeContractAddress).burnNfts(tokenIds); 
    }


    function sellTreasurePrize(uint256 tokenId) ensureIsRegistered (msg.sender)  external
    {
        //TODO reward gold for the specific prize

        //Then burn the prize
        TreasurePrize(_treasurePrizeContractAddress).burnNft(tokenId);
    }
}


