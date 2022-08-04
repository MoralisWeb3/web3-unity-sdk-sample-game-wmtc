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
    // FUNCTIONS: HELPERS
    ///////////////////////////////////////////////////////////

    function fromHexChar(uint8 c) public pure returns (uint8) 
    {
        if (bytes1(c) >= bytes1('0') && bytes1(c) <= bytes1('9')) {
            return c - uint8(bytes1('0'));
        }
        if (bytes1(c) >= bytes1('a') && bytes1(c) <= bytes1('f')) {
            return 10 + c - uint8(bytes1('a'));
        }
        if (bytes1(c) >= bytes1('A') && bytes1(c) <= bytes1('F')) {
            return 10 + c - uint8(bytes1('A'));
        }
        return 0;
    }

   function hexStringToAddress(string memory s) public pure returns (bytes memory) 
   {
        bytes memory ss = bytes(s);
        require(ss.length%2 == 0); // length must be even
        bytes memory r = new bytes(ss.length/2);
        for (uint i=0; i<ss.length/2; ++i) {
            r[i] = bytes1(fromHexChar(uint8(ss[2*i])) * 16 +
                        fromHexChar(uint8(ss[2*i+1])));
        }

        return r;

    }

    function toAddress(string memory s) public pure returns (address) 
    {
        bytes memory _bytes = hexStringToAddress(s);
        require(_bytes.length >= 1 + 20, "toAddress_outOfBounds");
        address tempAddress;

        assembly {
            tempAddress := div(mload(add(add(_bytes, 0x20), 1)), 0x1000000000000000000000000)
        }

        return tempAddress;
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


    function getRewardsHistory() public view returns (string memory rewardTitle, uint rewardType, uint rewardPrice )
    {
        require(_isRegistered[_lastRegisteredAddress], "Must be registered");

        rewardTitle = _lastReward[_lastRegisteredAddress].Title;
        rewardType = _lastReward[_lastRegisteredAddress].Type;
        rewardPrice = _lastReward[_lastRegisteredAddress].Price;
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


