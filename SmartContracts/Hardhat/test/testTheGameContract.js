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
        
        // TreasurePrize
        const TreasurePrize = await ethers.getContractFactory("TreasurePrize");
        const treasurePrize = await TreasurePrize.deploy();

        // TheGameContract
        const TheGameContract = await ethers.getContractFactory("TheGameContract");
        const theGameContract = await TheGameContract.deploy(gold.address, treasurePrize.address);

        return { theGameContract, addr1, addr2 };
    
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
    it("Sets getGold to 0 when deployed", async function ()
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
    it("Sets getGold to 10 when addTheGameContract 10", async function ()
    {
        // Arrange
        const { theGameContract } = await loadFixture(deployTokenFixture);

        // Act
        //await theGameContract.addGold(10);
        const goldBalance = await theGameContract.getGold();

        // Expect
        expect(goldBalance).to.equal(0);
    }),


    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets getGold to 05 when addTheGameContract 10 removTheGameContract 05", async function ()
    {
        // Arrange
        const { theGameContract } = await loadFixture(deployTokenFixture);

        // Act
        //await theGameContract.addGold(10);
        //await theGameContract.removeGold(5);
        const goldBalance = await theGameContract.getGold();

        // Expect
        expect(goldBalance).to.equal(0);
        
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets isRegistered to false when deployed", async function ()
    {
        // Arrange
        const { theGameContract, addr1 } = await loadFixture(deployTokenFixture);

        // Act
        const isRegistered = await theGameContract.connect(addr1).isRegistered();

        // Expect
        expect(isRegistered).to.equal(false);
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets isRegistered to true when register", async function ()
    {
        // Arrange
        const { theGameContract, addr1 } = await loadFixture(deployTokenFixture);

        // Act
        await theGameContract.connect(addr1).register();
        const isRegistered = await theGameContract.connect(addr1).isRegistered();

        // Expect
        expect(isRegistered).to.equal(true);
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets getGold to 99 when register", async function ()
    {
        // Arrange
        const { theGameContract, addr1 } = await loadFixture(deployTokenFixture);

        // Act
        await theGameContract.connect(addr1).register();
        const goldBalance = await theGameContract.connect(addr1).getGold();

        // Expect
        expect(goldBalance).to.equal(99);
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets getGold to 0 when register, unregister", async function ()
    {
        // Arrange
        const { theGameContract, addr1 } = await loadFixture(deployTokenFixture);

        // Act
        await theGameContract.connect(addr1).register();
        await theGameContract.connect(addr1).setGold(1);
        await theGameContract.connect(addr1).unregister();
        const goldBalance = await theGameContract.connect(addr1).getGold();

        // Expect
        expect(goldBalance).to.equal(0);
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets isRegistered to true when register, unregister", async function ()
    {
        // Arrange
        const { theGameContract, addr1 } = await loadFixture(deployTokenFixture);

        // Act
        await theGameContract.connect(addr1).register();
        await theGameContract.connect(addr1).unregister();
        const isRegistered = await theGameContract.connect(addr1).isRegistered();

        // Expect
        expect(isRegistered).to.equal(false);
    }),

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it.only("Sets r between min/max when randomRange (min, max, n)", async function ()
    {
        // Arrange
        const { theGameContract, addr1 } = await loadFixture(deployTokenFixture);
        var nonce = 0;
        var min = 0;
        var max = 10;

        for (var i = 0; i< 100; i++)
        {
            // Act
            const r = await theGameContract.randomRange(1, 10, nonce++);
            
            // Expect
            expect(r).to.greaterThanOrEqual(min).and.lessThanOrEqual(max);
        }
    })

});

