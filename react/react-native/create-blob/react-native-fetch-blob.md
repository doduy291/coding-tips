### Using [react-native-blob-util](https://github.com/RonRadtke/react-native-blob-util)

## 1. `RNFetchBlob.polyfill.Blob.build()`

```js
# Example code
const base64 = 'data:image/jpeg;base64,/ABCDXYZ...'
const blob = await RNFetchBlob.polyfill.Blob.build(base64.split(",")[1], {
  type: "image/png",
});

/* For Typescript */
/* Typescript catches error when using polyfill.Blob.build, use @ts-expect-error to prevent to throwing error. */

// @ts-expect-error
const blob = await RNFetchBlob.polyfill.Blob.build(base64.split(",")[1], {
    type: "image/png",
});
```

# How It Works:

- This method directly builds a Blob using RNFetchBlob's polyfill for Blob.
- Internally, it decodes the base64 data and constructs the Blob in one step.

# Pros:

- Native Efficiency: Uses optimized native methods under the hood, making it faster for larger files.
- Simpler API: Combines base64 decoding and Blob creation into one function call.
- Asynchronous: The await keyword ensures non-blocking execution, which is better for performance when dealing with large data.

# Cons:

- Dependency-Specific: Relies on RNFetchBlob's polyfill, making it less portable to environments without the library.

## 2. `RNFetchBlob.base64.decode` + `Blob()`

```js
const binaryData = RNFetchBlob.base64.decode(base64.split(",")[1]);
const blob = new Blob([binaryData], { type: "image/jpeg" });
```

# How It Works:

- Decode Base64: RNFetchBlob.base64.decode() converts the base64 string into a binary string.
- Create Blob: The binary string is wrapped in a standard Blob object.

# Pros:

- Flexibility: Separates decoding and Blob creation, allowing greater control over intermediate data.
- Standard API: Uses the native Blob() constructor, which can be more portable in environments where RNFetchBlob is not required.

# Cons:

- Double Handling: The binary string is created first and then wrapped into a Blob, which introduces an intermediate step.
- Performance Overhead: For large files, converting base64 to a binary string and then creating a Blob can consume more memory and CPU time.
