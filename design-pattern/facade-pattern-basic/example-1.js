// # Example 1:
/* Complex parts */
const Discount = {
  calc: (value) => value * 0.9,
};

const Shipping = {
  calc: () => 5,
};

const Fees = {
  calc: (value) => value * 1.05,
};

/* Facade */
const ShopeeFacadePattern = {
  calc: (price) => {
    price = Discount.calc(price);
    price = Fees.calc(price);
    price += Shipping.calc();

    return price;
  },
};

// Now we can export this ShopeeFacadePattern funciton
// export default ShopeeFacadePattern

// Usage
const Buy = (price) => {
  console.log("Price::", ShopeeFacadePattern.calc(price));
};
Buy(120000);
