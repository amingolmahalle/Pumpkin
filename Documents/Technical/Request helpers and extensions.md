#Request Helpers
There some helpers and extensions to use while you need data from incoming request or want to change it.

##Work with Headers
Get value of `authorization` header from request.
```c#
var token = Request.AuthToken();
```

Get value of `DeviceId` header.
```c#
var deviceId = Request.DeviceId();
```

Get `IP Address` of client, event it is behind NAT or Load Balancer.
```c#
var clientIp = Request.ClientIp();
```

Get value of a customer header by it's `key`.
```c#
var value = Request.GetHeader("ClientName");
```
