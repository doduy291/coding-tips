/* Example 1: */
/* Example from Tips Javascript */
/* https://www.youtube.com/watch?v=Z3mPTWuFw00&t=3s */

// Object
class Leader {
  receiveRequest(offer) {
    console.log(`result::${offer}`);
  }
}

// Proxy object
class SecretaryProxy {
  constructor() {
    this.leader = new Leader();
  }
  receiveRequest(offer) {
    this.leader.receiveRequest(offer);
  }
}

// Object
class Developer {
  constructor(offer) {
    this.offer = offer;
  }
  applyFor(target) {
    target.receiveRequest(this.offer);
  }
}

/* Usage */
const dev = new Developer("Dev wants to increase salary up to 5k$");
dev.applyFor(new SecretaryProxy());
