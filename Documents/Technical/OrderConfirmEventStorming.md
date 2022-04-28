# ConfirmPolicyCommand

## Business Actions
* If policy not exists, **ERROR**
* If already has confirmed, **ERROR**
* IF `UniqueNumber` is duplicated, **ERROR**
* Set `UniqueNumber`:
  * Set `IsConfirm` = true, `ConfirmedAt` =  NOW()
  * Check and Set `IsUsable` state
    * If [confirmed and payed and not expired and not total lost], make it `True`
* If all policies are `Confirmed` then make order item `IsConfirmed` = true
  * `ConfirmedAt` =  NOW()
* If all order items are `Confirmed` then make order `IsConfirmed` = true
  * `ConfirmedAt` =  NOW()
* If order is `Confirmed` and `Payed`
  * Change order state to `ConfirmedAndPayed`

## Technical Steps
* **DATA**
  * 
* **VALIDITY**
* **EVENT**
  * PolicyConfirmedEvent

##Event Policies
###Order finalizing policy
* **LISTENING**:
  * `OrderPaymentStateChanged`
  * `PolicyConfirmedEvent`
* **TASK**
  * Check order is **payed** and all items are **confirmed**?
    * No: do nothing
    * YES: change order state to 'PAYED+CONFIRMED'
* **EVENT**
  * OrderFinalizedEvent

###notify owner on order finalize policy
* **LISTENING**:
  * `OrderFinalizedEvent`
* **TASK**
  * send **SMS** and **EMAIL** to owner of the Order.
* **EVENT**
  * OrderOwnerNotifiedOnFinalizationEvent