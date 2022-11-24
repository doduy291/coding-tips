```js
Output: [['Jan', 'March'], ['April', 'June'], ['July']]
Input: const months = ['Jan', 'March', 'April', 'June', 'July'];

// Splice Usage:
const arrContainer = []
while (months.length) {
    arrContainer.push(months.splice(0, 2));
}
console.log(arrContainer)

// Reduce Usage:
const perChunk = 2 // items per chunk
const result = months.reduce((resultArray, item, index) => {
  const chunkIndex = Math.floor(index/perChunk)

  if(!resultArray[chunkIndex]) {
    resultArray[chunkIndex] = [] // start a new chunk
  }
  resultArray[chunkIndex].push(item)

  return resultArray
}, [])

console.log(result);

// Slice Usage
let arrayChunk= (arr, step) => {
    let arrResult = [];
    for (let i = 0; i < arr.length; i+= step) {
        arrResult.push(arr.slice(i, i + step));
    }
    return arrResult;
}
console.log(arrayChunk(months, 2))
```
