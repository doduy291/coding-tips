onmessage = function (message) {
  let sum = 0;
  for (let i = 0; i < 10000000000; i++) {
    sum += i;
  }
  console.log(message.data);
  console.log(`The final sum is: ${sum}`);
};
