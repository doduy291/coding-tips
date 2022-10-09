const wait = (ms) => {
  return new Promise((res) => setTimeout(res, ms));
};

const getContent = async () => {
  await wait(1000);
  return "This is content";
};

const getComments = async () => {
  await wait(1500);
  return ["comment 1", "comment 2"];
};

const getRelatedPosts = async () => {
  await wait(2000);
  return ["post 2", "post 3", "post 4"];
};

const getPostAsyncAwait = async () => {
  console.time("TIME-PROCESS::ASYNC-AWAIT");

  const content = await getContent();
  const comments = await getComments();
  const relatedPosts = await getRelatedPosts();

  console.log(`Post Info: `, { content, comments, relatedPosts });
  console.timeEnd("TIME-PROCESS::ASYNC-AWAIT");
};
const getPostPromiseAll = async () => {
  console.time("TIME-PROCESS::PROMISE-ALL");

  const [content, comments, relatedPosts] = await Promise.all([
    getContent(),
    getComments(),
    getRelatedPosts(),
  ]);

  console.log(`Post Info: `, { content, comments, relatedPosts });
  console.timeEnd("TIME-PROCESS::PROMISE-ALL");
};

getPostAsyncAwait();

getPostPromiseAll();
