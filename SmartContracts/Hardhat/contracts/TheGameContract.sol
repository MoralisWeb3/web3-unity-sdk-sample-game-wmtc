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

    // Determines if the player has registered yet
    mapping (address => bool) _isRegistered;

    // Stores the most recent reward
    mapping (address => Reward) _lastReward;

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
    // FUNCTIONS: REGISTRATION
    ///////////////////////////////////////////////////////////
    function isRegistered() public view returns (bool isPlayerRegistered)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        isPlayerRegistered = _isRegistered[msg.sender];
    }


    function register() public
    {
        _isRegistered[msg.sender] = true;
        setGold(99);
    }


    function unregister() public
    {
        _isRegistered[msg.sender] = false;
        setGold(0);
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REWARDS
    ///////////////////////////////////////////////////////////
    function startGameAndGiveRewards(uint256 goldAmount) public
    {
        require(goldAmount > 0, "Must send goldAmount > 0 to start the game.");

        require(_isRegistered[msg.sender] == true, "Must be registered to start the game.");

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

            //NOTE: Metadata structure must match in both: TheGameContract.sol and TreasurePrizeDto.cs
            string memory metadata = string(abi.encodePacked("Title=", title, "|Price=", price));
            mintNft (metadata);     
        }

        _lastReward[msg.sender] = Reward (
        {
            Title: title,
            Type: theType,
            Price: price
        });

    }

    function getRewardsHistory() public view returns (string memory rewardTitle, uint rewardType, uint rewardPrice )
    {
        require(_isRegistered[msg.sender] == true, "Must be registered to start the game.");

        rewardTitle = _lastReward[msg.sender].Title;
        rewardType = _lastReward[msg.sender].Type;
        rewardPrice = _lastReward[msg.sender].Price;
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: RANDOM
    ///////////////////////////////////////////////////////////
    function randomRange (uint min, uint max, uint nonce) public view returns (uint) 
    {
        // The nonce is especially useful for unit-tests where the block **MAYBE** will not change enough
        uint randomnumber = uint(keccak256(abi.encodePacked(block.timestamp, block.difficulty, msg.sender, nonce))) % (max);
        randomnumber = randomnumber + min;
        return randomnumber;
    }
    

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: GOLD
    ///////////////////////////////////////////////////////////
    function getGold() public view returns (uint256 balance)
    {
        balance = Gold(_goldContractAddress).getGold();
    }


    function setGold(uint256 targetBalance) public
    {
        Gold(_goldContractAddress).setGold(targetBalance);
    }


    function setGoldBy(int delta) public
    {
        Gold(_goldContractAddress).setGoldBy(delta); 
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: TREASUREPRIZE
    ///////////////////////////////////////////////////////////
    function mintNft(string memory tokenURI) public 
    {
        TreasurePrize(_treasurePrizeContractAddress).mintNft(tokenURI);
    }


    function burnNft(uint256 tokenId) public
    {
        TreasurePrize(_treasurePrizeContractAddress).burnNft(tokenId);
    }


    function burnNfts(uint256[] calldata tokenIds) public
    {
        TreasurePrize(_treasurePrizeContractAddress).burnNfts(tokenIds); 
    }
}


