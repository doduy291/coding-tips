/* Example realistic*/
/* Example from TomDoesTech */
/* https://www.youtube.com/watch?v=bQlYqOg8088 */
// ! Make sure you have MongoDB and Node.js installed.

const mongoose = require("mongoose");

const DB_ENDPOINT = "mongodb://localhost:27017/db-singleton";

function database() {
  let instance;
  let count = 0;

  async function connect() {
    const db = await mongoose.connect(DB_ENDPOINT);
    console.log("Connected to DB");
    return db;
  }

  async function getInstance() {
    count++;
    console.log(`Get instance count ${count}`);
    if (instance) {
      return instance;
    }
    console.log("No instance, creating db connection");

    instance = await connect();
    return instance;
  }
  return {
    getInstance,
    count,
  };
}
const db = database();

async function run() {
  await db.getInstance();
  await db.getInstance();
  await db.getInstance();
  await db.getInstance();
  await db.getInstance();
}

run();
// Get instance count 1
// No instance, creating db connection
// Connected to DB
// Get instance count 2
// Get instance count 3
// Get instance count 4
// Get instance count 5
