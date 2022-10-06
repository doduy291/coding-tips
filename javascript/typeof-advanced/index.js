const typeOfAdvanced = (value) => {
  return Object.prototype.toString.call(value).slice(8, -1);
};
// => [object String], [object Array], [object Null], [object Date],etc.
console.log(typeOfAdvanced("")); // => String
console.log(typeOfAdvanced([])); // => Array
console.log(typeOfAdvanced({})); // => Object
console.log(typeOfAdvanced(new Date())); // => Date
