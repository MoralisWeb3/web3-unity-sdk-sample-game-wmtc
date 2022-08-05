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

    address _lastRegisteredAddress;

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
    // FUNCTIONS: GETTERS
    ///////////////////////////////////////////////////////////
    function getLastRegisteredAddress() public view returns (string memory lastRegisteredAddress)
    {
        lastRegisteredAddress = Strings.toHexString(uint256(uint160(_lastRegisteredAddress)), 20);
    }

    function getMessage(string memory messageIn) public pure returns (string memory messageOut)
    {
        messageOut = messageIn;
    }


    function isRegistered() public view returns (bool isPlayerRegistered)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        isPlayerRegistered = _isRegistered[_lastRegisteredAddress];
    }


    function getGold() public view returns (uint256 balance)
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        balance = Gold(_goldContractAddress).getGold(_lastRegisteredAddress);
    }


    function getRewardsHistory() public view returns (string memory reward)
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        string memory rewardTitle2 = _lastReward[_lastRegisteredAddress].Title;
        uint256 rewardType2 = _lastReward[_lastRegisteredAddress].Type;
        uint256 rewardPrice2 = _lastReward[_lastRegisteredAddress].Price;
        reward = string(abi.encodePacked("Title=", rewardTitle2, "|Price=", rewardPrice2, "|Type=", rewardType2));

    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REGISTRATION
    ///////////////////////////////////////////////////////////
    function register() public
    {
        _lastRegisteredAddress = msg.sender;
        _isRegistered[_lastRegisteredAddress] = true;
        setGold(100);
    }


    function unregister() public
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        //Update gold first
        setGold(0);

        //Then unregister
        _isRegistered[_lastRegisteredAddress] = false;
        _lastRegisteredAddress = address(0);
        
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REWARDS
    ///////////////////////////////////////////////////////////
    function startGameAndGiveRewards(uint256 goldAmount) public
    {
        require(goldAmount > 0, "goldAmount must be > 0 to start the game");

        require(getGold() >= goldAmount, "getGold() must be >= goldAmount to start the game");

        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

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


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: CLEAR DATA
    ///////////////////////////////////////////////////////////

    function safeReregisterAndBurnNfts(uint256[] calldata tokenIds) public
    {
        // Do not require isRegistered for this method to run
        bool isReg = isRegistered();
        if (isReg)
        {
            unregister();
        }

        register();
        burnNfts(tokenIds);
        
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: GOLD
    ///////////////////////////////////////////////////////////



    function setGold(uint256 targetBalance) public
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        Gold(_goldContractAddress).setGold(_lastRegisteredAddress, targetBalance);
    }


    function setGoldBy(int delta) public
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        Gold(_goldContractAddress).setGoldBy(msg.sender, delta); 
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: TREASUREPRIZE
    ///////////////////////////////////////////////////////////
    function mintNft(string memory tokenURI) public 
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        TreasurePrize(_treasurePrizeContractAddress).mintNft(msg.sender, tokenURI);
    }


    function burnNft(uint256 tokenId) public
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        TreasurePrize(_treasurePrizeContractAddress).burnNft(tokenId);
    }


    function burnNfts(uint256[] calldata tokenIds) public
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        TreasurePrize(_treasurePrizeContractAddress).burnNfts(tokenIds); 
    }
}


