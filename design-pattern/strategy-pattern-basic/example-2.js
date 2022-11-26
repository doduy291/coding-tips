/* Strategy parts */
function defaultPrice(price) {
  return price;
}

function dailyPrice(price) {
  return price * 0.6;
}
function blackFridayPrice(price) {
  return price <= 200 ? price * 0.8 : price - 50;
}

//! Not recommend //
// function getPrice(price, typePromotion) {
//     if(typePromotion === 'default') {
//         return defaultPrice(price)
//     }
//     if(typePromotion === 'daily') {
//         return dailyPrice(price)
//     }
// }

// * Use this * //
/* Strategy Manager */
const getPriceStrategies = {
  default: defaultPrice,
  daily: dailyPrice,
  blackFriday: blackFridayPrice,
};

function getPrice(price, typePromotion) {
  return getPriceStrategies[typePromotion](price);
}

/* Usage */
console.log(getPrice(200, "daily"));
