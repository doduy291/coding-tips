/* Example 2: */
/* Example from TomDoesTech */
/* https://www.youtube.com/watch?v=PldXoGemkyk */

function createObserver() {
  return {
    subscribers: [], // [fn,fn,...]
    subscribe(fn) {
      this.subscribers.push(fn);
    },
    unsubscribe(fn) {
      this.subscribers = this.subscribers.filter((item) => item !== fn);
    },
    broadcast(data) {
      for (let i = 0; i < this.subscribers.length; i++) {
        this.subscribers[i](data);
      }
    },
  };
}

const observer = new createObserver();

const fn = (data) => {
  console.log("Callback 1 was executed with data::::", data);
};
const fn2 = (data) => {
  console.log("Callback 2 was executed with data::::", data);
};
observer.subscribe(fn);
observer.subscribe(fn2);

observer.broadcast("Hello from observer");
