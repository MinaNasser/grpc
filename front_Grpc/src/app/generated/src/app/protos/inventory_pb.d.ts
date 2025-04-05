// package: 
// file: src/app/protos/inventory.proto

import * as jspb from "google-protobuf";

export class StockRequest extends jspb.Message {
  getProductId(): number;
  setProductId(value: number): void;

  getQuantity(): number;
  setQuantity(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): StockRequest.AsObject;
  static toObject(includeInstance: boolean, msg: StockRequest): StockRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: StockRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): StockRequest;
  static deserializeBinaryFromReader(message: StockRequest, reader: jspb.BinaryReader): StockRequest;
}

export namespace StockRequest {
  export type AsObject = {
    productId: number,
    quantity: number,
  }
}

export class StockResponse extends jspb.Message {
  getIsAvailable(): boolean;
  setIsAvailable(value: boolean): void;

  getAvailableQuantity(): number;
  setAvailableQuantity(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): StockResponse.AsObject;
  static toObject(includeInstance: boolean, msg: StockResponse): StockResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: StockResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): StockResponse;
  static deserializeBinaryFromReader(message: StockResponse, reader: jspb.BinaryReader): StockResponse;
}

export namespace StockResponse {
  export type AsObject = {
    isAvailable: boolean,
    availableQuantity: number,
  }
}

