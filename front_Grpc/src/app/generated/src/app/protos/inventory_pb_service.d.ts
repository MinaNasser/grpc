// package: 
// file: src/app/protos/inventory.proto

import * as src_app_protos_inventory_pb from "../../../src/app/protos/inventory_pb";
import {grpc} from "@improbable-eng/grpc-web";

type InventoryCheckStock = {
  readonly methodName: string;
  readonly service: typeof Inventory;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_inventory_pb.StockRequest;
  readonly responseType: typeof src_app_protos_inventory_pb.StockResponse;
};

export class Inventory {
  static readonly serviceName: string;
  static readonly CheckStock: InventoryCheckStock;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class InventoryClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  checkStock(
    requestMessage: src_app_protos_inventory_pb.StockRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_inventory_pb.StockResponse|null) => void
  ): UnaryResponse;
  checkStock(
    requestMessage: src_app_protos_inventory_pb.StockRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_inventory_pb.StockResponse|null) => void
  ): UnaryResponse;
}

