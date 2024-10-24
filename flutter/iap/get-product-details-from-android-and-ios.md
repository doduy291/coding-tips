```js
List<GooglePlayProductDetails> _androidProductDetails = [];
List<AppStoreProduct2Details> _iOSProductDetails = [];

initStore() {
    final ProductDetailsResponse productDetailResponse = await iapConnection.queryProductDetails(_kProductIds.toSet());
    // Product details
    if (Platform.isAndroid) {
        final List<GooglePlayProductDetails> productDetails = productDetailResponse.productDetails as List<GooglePlayProductDetails>;
        _androidProductDetails = productDetails;
    }
    if (Platform.isIOS) {
    // TODO: sku for IOS
    }
}

String getProductPrice(String productId, [String? basePlanId]) {
    if (Platform.isAndroid) {
      List<GooglePlayProductDetails> productDetails = Shortdramaiap.instance.androidProductDetails;
      GooglePlayProductDetails? product = productDetails.firstWhereOrNull((data) => data.id == productId);
      ProductDetailsWrapper details = product!.productDetails;
      if (basePlanId == null && details.productType == ProductType.inapp) {
        return product.price;
      }
      if (basePlanId != null && details.productType == ProductType.subs) {
        List<SubscriptionOfferDetailsWrapper>? subsOfferDetails = details.subscriptionOfferDetails;
        List<PricingPhaseWrapper>? pricingPhases = subsOfferDetails?.firstWhereOrNull((data) => data.basePlanId == basePlanId)?.pricingPhases;
        return pricingPhases![0].formattedPrice;
      }
    }
    if (Platform.isIOS) {
      // TODO: product detail for iOS
      return '';
    }
    return '0';
}
```
