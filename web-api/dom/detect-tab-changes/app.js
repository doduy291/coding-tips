document.addEventListener("visibilitychange", (event) => {
  const datetime = new Date();
  if (document.visibilityState == "visible") {
    console.log(`Tab is active - ${datetime.toLocaleString("vi-VN")}`);
  } else {
    console.log(`Tab is inactive - ${datetime.toLocaleString("vi-VN")}`);
  }
});
