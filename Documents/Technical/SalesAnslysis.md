#Create Order
* Command for create new order: `CreateOrderCommand`
* Event after success: `OrderCreatedEvent`
* Failure reason:
  * ================
* Entities witch change:
  * `User`, `UserProfile` : Create new user if not exists on insurance system by his/her `Mobile`.
  * `Order` : Order information, State = `New`.
  * `OrderItem` : Includes `Template` and `Product` of client and Calculates `Amount`.
  * `Policy` : At least one covenant for an order item, if `Quantity` is 1, Otherwise for each `OrderItem`, there will be `Quantity` count `covenants`.
    * For **single** add api : There may `BasketId` and `ProductId`+`PlanId` already exists.
      * `Quantity` of order item should be increased.
      * Add new `Policy` for the `OrderItem`.
  * All `Coverages` from sold `Template` will be copied to `PolicyUsage` table.
  * All `Balances` for all companies mentioned in `CutDefinition` will be set. It's state would be `Waiting`
* **RESULT:**
  * For single api `PolicyNumber`
  * For bulk api `List<PolicyNumber>`