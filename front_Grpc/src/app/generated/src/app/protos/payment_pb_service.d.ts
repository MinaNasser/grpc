// package: 
// file: src/app/protos/payment.proto

import * as src_app_protos_payment_pb from "../../../src/app/protos/payment_pb";
import {grpc} from "@improbable-eng/grpc-web";

type PaymentProcessPayment = {
  readonly methodName: string;
  readonly service: typeof Payment;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_payment_pb.PaymentRequest;
  readonly responseType: typeof src_app_protos_payment_pb.PaymentResponse;
};

export class Payment {
  static readonly serviceName: string;
  static readonly ProcessPayment: PaymentProcessPayment;
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

export class PaymentClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  processPayment(
    requestMessage: src_app_protos_payment_pb.PaymentRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_payment_pb.PaymentResponse|null) => void
  ): UnaryResponse;
  processPayment(
    requestMessage: src_app_protos_payment_pb.PaymentRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_payment_pb.PaymentResponse|null) => void
  ): UnaryResponse;
}

