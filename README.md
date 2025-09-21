# CSHARP Basic Saga Pattern with Orchestrator

## **Overview**
This project demonstrates a basic implementation of the **Saga pattern** using an orchestrator to manage distributed transactions across multiple services.

The goal is to enable **compensation mechanisms** for actions already performed in other services, in case the overall transaction fails. Each service exposes a **compensation endpoint** to reverse its operation when needed.

---

## **Key Concepts**
- **Saga Pattern**: Manages long-running transactions by chaining local operations with compensations.
- **Orchestrator**: Coordinates the flow and triggers compensations if any step fails. 
- **Compensation Services**: Each service provides an endpoint to undo its operation.

---

## **Project Structure**
- `Controllers/OrderController.cs`: Handles order creation and cancellation.
- `SagaOrchestrator.cs`: Coordinates the saga flow and manages compensations [link](https://github.com/wavila88/saga-pattern-c--orchestator/blob/master/SagaOrchestator/Controllers/OrderOrchestator.cs).
- `PaymentService/`: Simulated payment logic and refund endpoint.
- `Orders/`: Order creation logic.
- `appsettings.json`: Configuration file.

---

## **How It Works**
1. The orchestrator calls each service in sequence:
   - Create Order
   - Reserve Inventory
   - Process Payment
2. If any service fails, the orchestrator triggers compensation actions in reverse order.
3. Compensation endpoints are exposed via HTTP POST methods.

---

## **Example Flow**
```plaintext
Create Order → Reserve Inventory → Process Payment
        ↓              ↓                ↓
     Success        Success         Failure
        ↓              ↓                ↓
Cancel Order ← Release Inventory ← Refund Payment
