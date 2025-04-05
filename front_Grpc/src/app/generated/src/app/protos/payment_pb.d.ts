// package: 
// file: src/app/protos/payment.proto

import * as jspb from "google-protobuf";

export class PaymentRequest extends jspb.Message {
  getOrderid(): number;
  setOrderid(value: number): void;

  getAmount(): number;
  setAmount(value: number): void;

  getPaymentmethod(): string;
  setPaymentmethod(value: string): void;

  getCardnumber(): string;
  setCardnumber(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PaymentRequest.AsObject;
  static toObject(includeInstance: boolean, msg: PaymentRequest): PaymentRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PaymentRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PaymentRequest;
  static deserializeBinaryFromReader(message: PaymentRequest, reader: jspb.BinaryReader): PaymentRequest;
}

export namespace PaymentRequest {
  export type AsObject = {
    orderid: number,
    amount: number,
    paymentmethod: string,
    cardnumber: string,
  }
}

export class PaymentResponse extends jspb.Message {
  getSuccess(): boolean;
  setSuccess(value: boolean): void;

  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PaymentResponse.AsObject;
  static toObject(includeInstance: boolean, msg: PaymentResponse): PaymentResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PaymentResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PaymentResponse;
  static deserializeBinaryFromReader(message: PaymentResponse, reader: jspb.BinaryReader): PaymentResponse;
}

export namespace PaymentResponse {
  export type AsObject = {
    success: boolean,
    message: string,
  }
}

