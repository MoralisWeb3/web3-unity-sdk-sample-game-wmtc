// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "hardhat/console.sol";


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :    
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract Prize is ERC721URIStorage 
{

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////

    // Auto generates tokenIds
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIds;

    // User address who owns this contract instance
    address _owner;


    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor(string memory name, string memory symbol) ERC721 (name, symbol) 
    {
        _owner = msg.sender;

        console.log(
            "Prize.constructor() _owner = %s",
            _owner
        );
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: 
    //      *   Create New Prize
    ///////////////////////////////////////////////////////////
    function mintPropertyNft(string memory tokenURI) public returns (uint256)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == owner);

        uint256 newItemId = _tokenIds.current();
        _mint(msg.sender, newItemId);
        _setTokenURI(newItemId, tokenURI);

        _tokenIds.increment();
        return newItemId;
    }

    ///////////////////////////////////////////////////////////
    // FUNCTION: 
    //      *   Delete Existing Prize
    ///////////////////////////////////////////////////////////
    function burnPropertyNft(uint256 tokenId) public returns (string memory) 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == owner);

        _burn(tokenId);
        return "Success!";
    }
}


