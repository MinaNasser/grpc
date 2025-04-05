// package: 
// file: src/app/protos/payment.proto

var src_app_protos_payment_pb = require("../../../src/app/protos/payment_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var Payment = (function () {
  function Payment() {}
  Payment.serviceName = "Payment";
  return Payment;
}());

Payment.ProcessPayment = {
  methodName: "ProcessPayment",
  service: Payment,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_payment_pb.PaymentRequest,
  responseType: src_app_protos_payment_pb.PaymentResponse
};

exports.Payment = Payment;

function PaymentClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

PaymentClient.prototype.processPayment = function processPayment(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Payment.ProcessPayment, {
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

exports.PaymentClient = PaymentClient;

