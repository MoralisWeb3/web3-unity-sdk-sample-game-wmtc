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
describe("The Gold Contract", function ()
{

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Deploys with no exceptions", async function ()
    {
        // Arrange
        const [owner] = await ethers.getSigners();
        const Gold = await ethers.getContractFactory("Gold");

        // Act
        const gold = await Gold.deploy();

        // Expect
        expect(true).to.equal(true);
    }),


    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets totalSupply to 0 when deployed", async function ()
    {
        // Arrange
        const [owner] = await ethers.getSigners();
        const Gold = await ethers.getContractFactory("Gold");
        const gold = await Gold.deploy();

        // Act
        const totalSupply = await gold.totalSupply();

        // Expect
        expect(totalSupply).to.equal(0);
    }),


    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets goldBalance to 0 when deployed", async function ()
    {
        // Arrange
        const [owner] = await ethers.getSigners();
        const Gold = await ethers.getContractFactory("Gold");
        const gold = await Gold.deploy();

        // Act
        const goldBalance = await gold.getGold();

        // Expect
        expect(goldBalance).to.equal(0);
    })

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets goldBalance to 10 when addGold 10", async function ()
    {
        // Arrange
        const [owner] = await ethers.getSigners();
        const Gold = await ethers.getContractFactory("Gold");
        const gold = await Gold.deploy();

        // Act
        await gold.addGold(10);
        const goldBalance = await gold.getGold();

        // Expect
        expect(goldBalance).to.equal(10);
    }),

    ///////////////////////////////////////////////////////////
    // TEST - TODO: MAKE THIS WORK..
    ///////////////////////////////////////////////////////////
    // it("Reverts on removeGold 10 when deployed", async function ()
    // {
    //     // Arrange
    //     const [owner] = await ethers.getSigners();
    //     const Gold = await ethers.getContractFactory("Gold");
    //     const gold = await Gold.deploy();

    //     // Act
    //     await gold.removeGold(10);

    //     // Expect
    //     await expect(gold.getGold()).to.be.reverted();
    //     //revertedWith('ERC20: burn amount exceeds balance');
    // }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets goldBalance to 05 when addGold 10 removGold 05", async function ()
    {
        // Arrange
        const [owner] = await ethers.getSigners();
        const Gold = await ethers.getContractFactory("Gold");
        const gold = await Gold.deploy();

        // Act
        await gold.addGold(10);
        await gold.removeGold(5);
        const goldBalance = await gold.getGold();

        // Expect
        expect(goldBalance).to.equal(5);
        
    })

});

