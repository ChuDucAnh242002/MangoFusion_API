import { API_BASE_URL } from "../../utility/constant";

function OrderTable({ orders, isLoading, error, onEdit }) {
  if (isLoading) {
    return (
      <div className="text-center py-4">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
        <p className="mt-2">Loading orders...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="alert alert-danger">
        <h5>Error Loading Orders</h5>
        <p>An error occurred while loading orders.</p>
      </div>
    );
  }

  if (!orders) {
    return (
      <div className="text-center py-5">
        <i className="bi bi-basket text-muted" style={{ fontSize: "3rem" }}></i>
        <h4 className="mt-3 text-muted">No Orders</h4>
        <p className="text-muted">Start by adding your first order.</p>
      </div>
    );
  }

  return (
    <>
      <div className="table-responsive">
        <table className="table table-hover">
          <thead className="table-dark">
            <tr>
              <th>Order #</th>
              <th>Date</th>
              <th>Customer</th>
              <th>Items</th>
              <th>Total</th>
              <th>Status</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {orders.map((order) => (
              <tr key={order.orderHeaderId}>
                <td className="fw-semibold">#{order.orderHeaderId}</td>
                <td>
                  <small className="text-muted">{order.orderDate}</small>
                </td>
                <td>
                  <div className="small">
                    <div
                      className="fw-semibold text-truncate"
                      style={{ maxWidth: "140px" }}
                    >
                      {order.pickUpName}
                    </div>
                    <div
                      className="fw-semibold text-truncate"
                      style={{ maxWidth: "140px" }}
                    >
                      {order.pickUpEmail}
                    </div>
                    <div
                      className="fw-semibold text-truncate"
                      style={{ maxWidth: "140px" }}
                    >
                      {order.pickUpPhoneNumber}
                    </div>
                  </div>
                </td>
                <td>
                  <strong>${order.totalItem}</strong>
                </td>
                <td>${parseFloat(order.orderTotal || 0).toFixed(2)}</td>
                <td>
                  <span className={`btn btn-sm disabled btn-primary`}>
                    {order.status}
                  </span>
                </td>
                <td>
                  <div className="btn-group" role="group">
                    <button
                      onClick={() => onEdit(order)}
                      className="btn btn-sm btn-outline-success"
                      title="Edit"
                    >
                      <i className="bi bi-pencil"></i>
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default OrderTable;
