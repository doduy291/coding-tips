> These ways should only work for functions that do not depend on result of other functions. They are totally independent functions.

- Example 1:

```bash
const caseX = (x) => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(x);
    }, 3000);
  });
};

const caseY = (y) => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(y);
    }, 2000);
  });
};

const totalAsyncAwait = async (x, y) => {
  console.time("TIME-PROCESS::ASYNC-AWAIT");
  const _x = await caseX(x);
  const _y = await caseY(y);
  console.timeEnd(`TIME-PROCESS::ASYNC-AWAIT`);
  return _x + _y;
};

# const _x = await caseX(x) and const _y = await caseY(y) can be understood like this with Promise:
...
caseX(x).then((resultX) =>
  caseY(y).then((resultY) => {
    // To do something...
    }),
);
...

const totalPromiseAll = async (x, y) => {
  console.time("TIME-PROCESS::PROMISE");
  const [_x, _y] = await Promise.all([caseX(x), caseY(y)]);
  console.timeEnd(`TIME-PROCESS::PROMISE`);
  return _x + _y;
};

// Way to use async await like Promise.all
const totalNewAsyncAwait = async (x, y) => {
  console.time("TIME-PROCESS::NEW-ASYNC-AWAIT");
  const promiseX = caseX(x);
  const promiseY = caseY(y);
  const _awaitX = await promiseX;
  const _awaitY = await promiseY;
  console.timeEnd(`TIME-PROCESS::NEW-ASYNC-AWAIT`);
  return _awaitX + _awaitY;
};

totalAsyncAwait(1, 2);
// ===> 5.0 seconds

totalPromiseAll(1, 2);
// ===> 3 seconds

totalNewAsyncAwait(1, 2);
// ===> 3 seconds

```
