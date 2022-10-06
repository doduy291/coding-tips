function countDown(currentNum) {
  console.log(currentNum);
  let nextNum = currentNum - 1;
  if (nextNum > 0) {
    countDown(nextNum);
  }
}
countDown(3);
