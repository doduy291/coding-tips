import Redis from "ioredis";

const redis = new Redis();

async function addVideo(videoId) {
  console.log(await redis.set(`video::${videoId}`, 0));
}
async function playVideo(videoId, userId) {
  try {
    const keyVideo = `video::${videoId}`;
    const keyUserId = `users::${userId}`;
    console.log(await redis.get(keyVideo));
    const keyUserStorage = await redis.set(
      keyUserId,
      "test-user-key",
      "NX",
      "EX",
      20,
    ); // # Options: https://redis.io/commands/set/

    if (keyUserStorage === "OK") {
      await redis.incrby(keyVideo, 1); // # Options: https://redis.io/commands/incrby/
    }
  } catch (error) {
    console.error(`Error: playVideo::`, error);
  }
}
// addVideo(10001);
playVideo(10001, 102);

// !To prevent spam with account clone, should replace userId = IP address
