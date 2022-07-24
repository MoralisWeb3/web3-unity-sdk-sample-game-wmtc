///////////////////////////////////////////////////////////
// REQUIRES
///////////////////////////////////////////////////////////
const {
    time,
    loadFixture,
} = require("@nomicfoundation/hardhat-network-helpers");
const { anyValue } = require("@nomicfoundation/hardhat-chai-matchers/withArgs");
const { expect } = require("chai");


///////////////////////////////////////////////////////////
// TEST
///////////////////////////////////////////////////////////
describe("The Game Contract", function ()
{
    async function deployTokenFixture() 
    {

        // Arrange
        const [owner, addr1, addr2] = await ethers.getSigners();

        // Gold
        const Gold = await ethers.getContractFactory("Gold");
        const gold = await Gold.deploy();

        // TheGameContract
        const TheGameContract = await ethers.getContractFactory("TheGameContract");
        const theGameContract = await TheGameContract.deploy(gold.address);

        return { theGameContract };
    
    }

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Deploys with no exceptions", async function ()
    {
        // Arrange
        const { theGameContract } = await loadFixture(deployTokenFixture);

        // Act

        // Expect
        expect(true).to.equal(true);
    }),


    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets thegamecontractBalance to 0 when deployed", async function ()
    {
        // Arrange
        const { theGameContract } = await loadFixture(deployTokenFixture);

        // Act
        const goldBalance = await theGameContract.getGold();

        // Expect
        expect(goldBalance).to.equal(0);
    })

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets thegamecontractBalance to 10 when addTheGameContract 10", async function ()
    {
        // Arrange
        const { theGameContract } = await loadFixture(deployTokenFixture);

        // Act
        await theGameContract.addGold(10);
        const goldBalance = await theGameContract.getGold();

        // Expect
        expect(goldBalance).to.equal(10);
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets thegamecontractBalance to 05 when addTheGameContract 10 removTheGameContract 05", async function ()
    {
        // Arrange
        const { theGameContract } = await loadFixture(deployTokenFixture);

        // Act
        await theGameContract.addGold(10);
        await theGameContract.removeGold(5);
        const goldBalance = await theGameContract.getGold();

        // Expect
        expect(goldBalance).to.equal(5);
        
    })

});

