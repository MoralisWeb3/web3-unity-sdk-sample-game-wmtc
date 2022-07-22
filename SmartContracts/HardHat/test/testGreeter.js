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
describe("The Greeter Contract", function ()
{

    ///////////////////////////////////////////////////////////
    // TEST
    ///////////////////////////////////////////////////////////
    it("Sets _greeting to DEFAULT when deployed", async function ()
    {
        const [owner] = await ethers.getSigners();
        const Greeter = await ethers.getContractFactory("Greeter");
        const hardhatGreeter = await Greeter.deploy();

        const greeting = await hardhatGreeter.getGreeting();

        expect(greeting).to.equal("Default Greeting");
    }),


        ///////////////////////////////////////////////////////////
        // TEST
        ///////////////////////////////////////////////////////////
        it("Sets _greeting to NEW VALUE when setGreeting", async function ()
        {
            const [owner] = await ethers.getSigners();
            const Greeter = await ethers.getContractFactory("Greeter");
            const hardhatGreeter = await Greeter.deploy();

            const newValue = "new Value";
            await hardhatGreeter.setGreeting(newValue);
            const greeting = await hardhatGreeter.getGreeting();

            expect(greeting).to.equal(newValue);
        }),


        ///////////////////////////////////////////////////////////
        // TEST
        ///////////////////////////////////////////////////////////
        it("Sets _greeting to EMPTY STRING when setGreeting", async function ()
        {
            const [owner] = await ethers.getSigners();
            const Greeter = await ethers.getContractFactory("Greeter");
            const hardhatGreeter = await Greeter.deploy();

            const newValue = "";
            await hardhatGreeter.setGreeting(newValue);
            const greeting = await hardhatGreeter.getGreeting();

            expect(greeting).to.equal(newValue);
        }),


        ///////////////////////////////////////////////////////////
        // TEST
        ///////////////////////////////////////////////////////////
        it("Reverts when EMPTY STRING passed to setGreetingSafe", async function ()
        {
            const [owner] = await ethers.getSigners();
            const Greeter = await ethers.getContractFactory("Greeter");
            const hardhatGreeter = await Greeter.deploy();

            const newValue = "";
            await expect(hardhatGreeter.setGreetingSafe(newValue)).to.be.revertedWith("Unexpected value for greeting");
        });
});

