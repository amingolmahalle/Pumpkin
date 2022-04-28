# OrderPaymentCommand

## Business Actions
* **VALIDATIONS**
  * If order not exists, **ERROR**
  * If order state <> pending, **ERROR**
  * If successful and `TrackingCode` is null or empty, **ERROR**
  * If failed payment and `StatusCode` is null or empty, **ERROR**
* **CHANGES** 
  * If was successful
    * ON ORDER
      * `PaymentState` = Succeed, 
      * `PaymentAnnouncedAt` = NOW(), 
      * `PaymentTrackingCode` = incoming code
      * `PayedAt` = NOW()
    * ON ALL POLICIES
      * `IsPayed` = true
      * Check and Set `IsUsable` state
        * If [confirmed and payed and not expired and not total lost], make it `True`
  * If failed payment
    * ON ORDER
      * `PaymentState` = Failed,
      * `PaymentAnnouncedAt` = NOW(),
      * `PaymentFailReason` = incoming `StatusCode`
