#Event Storming Commands

## OrderPayedCommand
* **DATA**
  * `ApplicationId`: string
  * `BasketId`: string
  * `IsSuccess`: bool
  * `TrackingCode`: string
  * `PaymentState`: string
* **VALIDITY**
  * Client exists.
  * Client is active.
  * Basket exists.
  * Basket is in valid state.
  * If `success` ==  false, `PaymentState` should have value. 
  * If `success` ==  true, `TrackingCode` should have value.
* **CHANGES**
  * `CurrentState`: Pending
* **EVENT**
  * OrderPaymentStateChanged
    * `OrderId`: UUID
    * `IsSuccess`: bool
    * `TrackingCode`: string
    * `PaymentState`: string