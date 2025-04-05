// package: 
// file: src/app/protos/inventory.proto

var src_app_protos_inventory_pb = require("../../../src/app/protos/inventory_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var Inventory = (function () {
  function Inventory() {}
  Inventory.serviceName = "Inventory";
  return Inventory;
}());

Inventory.CheckStock = {
  methodName: "CheckStock",
  service: Inventory,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_inventory_pb.StockRequest,
  responseType: src_app_protos_inventory_pb.StockResponse
};

exports.Inventory = Inventory;

function InventoryClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

InventoryClient.prototype.checkStock = function checkStock(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Inventory.CheckStock, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.InventoryClient = InventoryClient;

