/* Example 1: */
/* Example from Tips Javascript*/
/* https://www.youtube.com/watch?v=7J5pRc2vzWk */

class Observer {
  constructor(name) {
    this.name = name;
  }
  updateStatus(location) {
    console.log(`${this.name}:::::PING:::::${JSON.stringify(location)}`);
  }
}

class Subject {
  constructor() {
    this.observers = [];
  }
  addObserver(observer) {
    this.observers.push(observer);
  }
  notifyAllObservers(location) {
    this.observers.forEach((observer) => observer.updateStatus(location));
  }
}

const subject = new Subject();

// your pick
const riki = new Observer("Riki");
const sniper = new Observer("Sniper");

// add your team
subject.addObserver(riki);
subject.addObserver(sniper);

// Notify to specific team
subject.notifyAllObservers({ team: "teamA" });
